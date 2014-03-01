using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Context;
using Entity.Models;
using Repository;

namespace DataAccess.Usuario
{
    public class UsuarioDAO : IDisposable
    {   
        private IUnitOfWork _unitOfWork = new UnitOfWork(new MainContext());

        public List<User> GetAllIEnumerable()
        {
            try
            {
                return _unitOfWork.Repository<User>().Query().Filter(u => u.EstReg == "A").Get().ToList<User>();
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<User> GetAllIQueryable()
        {
            try
            {
                return _unitOfWork.Repository<User>().Query().Filter(u => u.EstReg == "A").Get();
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
    }
}
