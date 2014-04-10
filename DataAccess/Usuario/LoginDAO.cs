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
    public class LoginDAO : IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new MainContext());

        /// <summary>
        /// Caso o login seja efetuado com sucesso, o objeto do usuario 'e substituido por um que possui
        /// todas as informacoes.
        /// </summary>
        /// <returns></returns>
        public virtual User CheckLogin(User usuario)
        {
            try
            {
                User queryUser = _unitOfWork.Repository<User>().SingleOrDefault(user => user.Username == usuario.Username && user.Password == usuario.Password);

                return queryUser;
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
