using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Linq.Dynamic;
using Repository;

namespace AdminPanel.Helpers.Tables
{
    /// <summary>
    /// Parses the request values from a query from the DataTables jQuery pluggin
    /// </summary>
    /// <typeparam name="T">List data type</typeparam>
    public class DataTableParser<T>
    {
        /*
         * int: iDisplayStart - Display start point
        * int: iDisplayLength - Number of records to display
        * string: string: sSearch - Global search field
        * boolean: bEscapeRegex - Global search is regex or not
        * int: iColumns - Number of columns being displayed (useful for getting individual column search info)
        * string: sSortable_(int) - Indicator for if a column is flagged as sortable or not on the client-side
        * string: sSearchable_(int) - Indicator for if a column is flagged as searchable or not on the client-side
        * string: sSearch_(int) - Individual column filter
        * boolean: bEscapeRegex_(int) - Individual column filter is regex or not
        * int: iSortingCols - Number of columns to sort on
        * int: iSortCol_(int) - Column being sorted on (you will need to decode this number for your database)
        * string: sSortDir_(int) - Direction to be sorted - "desc" or "asc". Note that the prefix for this variable is wrong in 1.5.x, but left for backward compatibility)
        * string: sEcho - Information for DataTables to use for rendering
         */

        private const string INDIVIDUAL_SEARCH_KEY_PREFIX = "sSearch_";
        private const string INDIVIDUAL_SORT_KEY_PREFIX = "iSortCol_";
        private const string INDIVIDUAL_SORT_DIRECTION_KEY_PREFIX = "sSortDir_";
        private const string DISPLAY_START = "iDisplayStart";
        private const string DISPLAY_LENGTH = "iDisplayLength";
        private const string ECHO = "sEcho";
        private const string ASCENDING_SORT = "asc";

        private IQueryable<T> _queriable;
        private readonly HttpRequestBase _httpRequest;
        private readonly Type _type;
        private readonly PropertyInfo[] _properties;

        public DataTableParser(HttpRequestBase httpRequest, IQueryable<T> queriable)
        {
            _queriable = queriable;
            _httpRequest = httpRequest;
            _type = typeof(T);
            _properties = _type.GetProperties();
        }

        public DataTableParser(HttpRequest httpRequest, IQueryable<T> queriable)
            : this(new HttpRequestWrapper(httpRequest), queriable)
        { }

        /// <summary>
        /// Parses the <see cref="HttpRequestBase"/> parameter values for the accepted 
        /// DataTable request values
        /// </summary>
        /// <returns>Formated output for DataTables, which should be serialized to JSON</returns>
        /// <example>
        ///     In an ASP.NET MVC from a controller, you can call the Json method and return this result.
        ///     
        ///     public ActionResult List()
        ///     {
        ///         // change the following line per your data configuration
        ///         IQueriable<User> users = datastore.Linq();
        ///         
        ///         if (Request["sEcho"] != null) // always test to see if the request is from DataTables
        ///         {
        ///             var parser = new DataTableParser<User>(Request, users);
        ///             return Json(parser.Parse());
        ///         }
        ///         return Json(_itemController.CachedValue);
        ///     }
        ///     
        ///     If you're not using MVC, you can create a web service and write the JSON output as such:
        ///     
        ///     using System.Web.Script.Serialization;
        ///     public class MyWebservice : System.Web.Services.WebService
        ///     {
        ///         public string MyMethod()
        ///         {
        ///             // change the following line per your data configuration
        ///             IQueriable<User> users = datastore.Linq();
        ///             
        ///             response.ContentType = "application/json";
        ///             
        ///             JavaScriptSerializer serializer = new JavaScriptSerializer();
        ///             var parser = new DataTableParser<User>(Request, users);
        ///             return new JavaScriptSerializer().Serialize(parser.Parse());
        ///         }
        ///     }
        /// </example>
        public FormatedList<T> Parse()
        {
            var list = new FormatedList<T>();

            // import property names
            //list.Import(_properties.Select(x => x.Name).ToArray());
            list.Import(_properties);

            // parse the echo property (must be returned as int to prevent XSS-attack)
            list.sEcho = int.Parse(_httpRequest[ECHO]);

            if (string.IsNullOrEmpty(_httpRequest["sSearch"]))
                // count the record BEFORE filtering
                list.iTotalRecords = _queriable.Count();

            // apply the sort, if there is one
            ApplySort();

            // parse the paging values
            int skip = 0, take = 10;
            int.TryParse(_httpRequest[DISPLAY_START], out skip);
            int.TryParse(_httpRequest[DISPLAY_LENGTH], out take);

            //.Where("City == @0 and Orders.Count >= @1", "London", 10).
            //    OrderBy("CompanyName").
            //    Select("New(CompanyName as Name, Phone)")

            // setup the data with individual property search, all fields search,
            // paging, and property list selection
            list.aaData = _queriable.Where(ApplyGenericSearch, _httpRequest["sSearch"].ToString())
                //.Where(ApplyGenericSearch)
                //.Where(IndividualPropertySearch)
                                    .Skip(skip)
                                    .Take(take)
                                    //.Select("New(" + _columns + ")") as List<T>;
                //.Select(SelectProperties)
                                    .ToList<T>();

            if (!string.IsNullOrEmpty(_httpRequest["sSearch"]))
                list.iTotalRecords = list.aaData.Count;

            // total records that are displayed
            list.iTotalDisplayRecords = list.iTotalRecords;//list.aaData.Count;

            return list;
        }

        private void ApplySort()
        {
            int sortcolumn = int.Parse(_httpRequest["iSortCol_0"]);
            if (sortcolumn < 0 || sortcolumn >= _properties.Length)
                return;

            string sortDir = _httpRequest["sSortDir_0"].ToString();
            string columnOrder = _httpRequest["mDataProp_" + sortcolumn].ToString();
            _queriable = _queriable.OrderBy(columnOrder + " " + sortDir);
        }

        /// <summary>
        /// Expression that returns a list of string values, which correspond to the values
        /// of each property in the list type
        /// </summary>
        /// <remarks>This implementation does not allow indexers</remarks>
        private Expression<Func<T, List<string>>> SelectProperties
        {
            get
            {
                // 
                return value => _properties.Select
                                            (
                    // empty string is the default property value
                                                prop => (prop.GetValue(value, new object[0]) ?? string.Empty).ToString()
                                            )
                                           .ToList();
            }
        }

        /// <summary>
        /// Compound predicate expression with the individual search predicates that will filter the results
        /// per an individual column
        /// </summary>
        private Expression<Func<T, bool>> IndividualPropertySearch
        {
            get
            {
                var paramExpr = Expression.Parameter(typeof(T), "val");
                Expression whereExpr = Expression.Constant(true); // default is val => True

                foreach (string key in _httpRequest.Params.AllKeys.Where(x => x.StartsWith(INDIVIDUAL_SEARCH_KEY_PREFIX)))
                {
                    // parse the property number
                    int property = -1;
                    if (!int.TryParse(_httpRequest[key].Replace(INDIVIDUAL_SEARCH_KEY_PREFIX, string.Empty), out property)
                        || property >= _properties.Length || string.IsNullOrEmpty(_httpRequest[key]))
                        break; // ignore if the option is invalid

                    string query = _httpRequest[key].ToLower();

                    // val.{PropertyName}.ToString().ToLower().Contains({query})
                    var toStringCall = Expression.Call(
                                        Expression.Call(
                                            Expression.Property(paramExpr, _properties[property]), "ToString", new Type[0]),
                                        typeof(string).GetMethod("ToLower", new Type[0]));

                    // reset where expression to also require the current contraint
                    whereExpr = Expression.And(whereExpr,
                                               Expression.Call(toStringCall,
                                                               typeof(string).GetMethod("Contains"),
                                                               Expression.Constant(query)));

                }

                return Expression.Lambda<Func<T, bool>>(whereExpr, paramExpr);
            }
        }

        /// <summary>
        /// Expression for an all column search, which will filter the result based on this criterion
        /// </summary>
        //private Expression<Func<T, bool>> ApplyGenericSearch
        //{
        //    get
        //    {
        //        string search = _httpRequest["sSearch"];

        //        // default value
        //        if (string.IsNullOrEmpty(search) || _properties.Length == 0)
        //            return x => true;

        //        // invariant expressions
        //        var searchExpression = Expression.Constant(search.ToLower());
        //        var paramExpression = Expression.Parameter(typeof(T), "val");

        //        // query all properties and returns a Contains call expression 
        //        // from the ToString().ToLower()
        //        try
        //        {
        //            var propertyQuery = (from property in _properties
        //                                 let tostringcall = Expression.Call(
        //                                                            Expression.Call(
        //                                                                Expression.AndAlso(
        //                                                                    Expression.AndAlso(
        //                                                                        Expression.Call(Expression.Call(typeof(Type).GetMethod("GetType"), Expression.Property(paramExpression, property)),"Equals", new Type[1] {typeof(string)}),
        //                                                                        Expression.Call(typeof(string).GetMethod("IsNullOrEmpty"), Expression.Property(paramExpression, property))
        //                                                                    ),
        //                                     //Expression.Call(Expression.Property(paramExpression, property), typeof(string).GetMethod("IsNullOrEmpty")),
        //                                                                    Expression.Call(Expression.Property(paramExpression, property), "ToString", new Type[0])
        //                                                                ), "ToLower", new Type[0]

        //                                                                //Expression.Property(paramExpression, property), "ToString", new Type[0]
        //                                                            ),
        //                                                         typeof(string).GetMethod("ToLower", new Type[0]))
        //                                 select Expression.Call(tostringcall, typeof(string).GetMethod("Contains"), searchExpression)).ToArray();

        //            // we now need to compound the expression by starting with the first
        //            // expression and build through the iterator
        //            Expression compoundExpression = propertyQuery[0];


        //            // add the other expressions
        //            for (int i = 1; i < propertyQuery.Length; i++)
        //            {
        //                var nullExpression = Expression.Constant(null, propertyQuery[i].Object.Type);
        //                var equalityCheck = Expression.Equal(propertyQuery[i].Object, nullExpression);

        //                compoundExpression = Expression.Condition(equalityCheck, Expression.Empty(), compoundExpression);

        //                compoundExpression = Expression.Or(compoundExpression, propertyQuery[i]);
        //                //compoundExpression = Expression.Or(Expression.AndAlso(Expression.Not(Expression.Call(typeof(string), "IsNullOrEmpty", null, propertyQuery[i])), compoundExpression), propertyQuery[i]);
        //                //compoundExpression = Expression.AndAlso(Expression.Call(typeof(string), "IsNullOrEmpty", null, propertyQuery[i]), Expression.Or(compoundExpression, propertyQuery[i]));
        //            }


        //            // compile the expression into a lambda 
        //            return Expression.Lambda<Func<T, bool>>(compoundExpression, paramExpression);
        //        }
        //        catch (Exception e)
        //        {
        //            throw e;
        //        }


        //    }
        //}

        private String ApplyGenericSearch
        {
            get
            {
                string search = _httpRequest["sSearch"];

                // default value
                if (string.IsNullOrEmpty(search))
                    return "1=1";

                string clausule = "";
                foreach (PropertyInfo pi in this._properties)
                {
                    if(pi.PropertyType == typeof(String))
                        clausule += pi.Name + ".Contains(@0) or ";
                    //else if(pi.PropertyType == typeof(int))
                    //    clausule += pi.Name + ".Equals(@0) or ";
                    //else if(pi.PropertyType == typeof(DateTime))
                    //    clausule += pi.Name + ".ToString(\"dd/MM/YYYY\").Contains(@0) or ";
                    else if (pi.PropertyType == typeof(ObjectState))
                        clausule += "";
                }
                clausule = clausule.Remove(clausule.Length - 3, 3);

                return clausule;
            }
        }
    }

    public class FormatedList<T>
    {

        public int sEcho { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public List<T> aaData { get; set; }
        public string sColumns { get; set; }

        public void Import(PropertyInfo[] properties)
        {
            sColumns = string.Empty;
            for (int i = 0; i < properties.Length; i++)
            {
                sColumns += properties[i].Name;
                if (i < properties.Length - 1)
                    sColumns += ",";
            }
        }
    }
}
