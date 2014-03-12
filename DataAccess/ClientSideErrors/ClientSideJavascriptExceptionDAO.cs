using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Context;
using Entity.Models.ClientSideErrors;
using Repository;

namespace DataAccess.ClientSideErrors
{
    public class ClientSideJavascriptExceptionDAO : IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new MainContext());

        public void Create(ClientSideJavaScriptException except)
        {
            try
            {
                _unitOfWork.Repository<ClientSideJavaScriptException>().Insert(except);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
            }
        }

        public IQueryable<ClientSideJavaScriptException> ReadAll()
        {
            try
            {
                return _unitOfWork.Repository<ClientSideJavaScriptException>().Query().Get();
            }
            catch
            {
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public ClientSideJavaScriptException ReadOne(int id)
        {
            try
            {
                return _unitOfWork.Repository<ClientSideJavaScriptException>().SingleOrDefault(s => s.ClientSideJavaScriptExceptionId == id);
            }
            catch
            {
                throw;
            }
        }
    }
}
