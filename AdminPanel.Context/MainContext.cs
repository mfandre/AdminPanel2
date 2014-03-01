#region

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using AdminPanel.Context.Mapping;
using Repository;
using Repository.Providers.EntityFramework;

#endregion

namespace AdminPanel.Context
{
    public partial class MainContext : DbContextBase
    {
        public MainContext() :
            base("MainContext")
        {
            Database.SetInitializer<MainContext>(null);
            Configuration.ProxyCreationEnabled = false;
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new SdeMap());
            modelBuilder.Configurations.Add(new ClienteMap());
            modelBuilder.Configurations.Add(new ArcgisServerMap());
            modelBuilder.Configurations.Add(new ClientSideJavascriptErrorMap());
            modelBuilder.Configurations.Add(new MenuItemMap());
        }
    }
}