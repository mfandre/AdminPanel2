using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Context;
using Entity.Models.Menu;
using Repository;

namespace DataAccess.Menu
{
    public class MenuDAO : IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new MainContext());

        public IQueryable<MenuItem> GetMainMenu()
        {
            try
            {
                return _unitOfWork.Repository<MenuItem>().Query()
                    .Include(u => u.Children)
                    .Include(u => u.Parent)
                    .Get();
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<MenuItem> ReadAll()
        {
            try
            {
                return _unitOfWork.Repository<MenuItem>().Query().Filter(i => i.EstReg == "A").Get();
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
