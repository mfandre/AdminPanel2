using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Context;
using Entity.Models;
using Repository;

namespace DataAccess.ControleDeAmbientes
{
    public class ClienteDAO : IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new MainContext());

        public object Create(Cliente cliente)
        {
            try
            {
                _unitOfWork.Repository<Cliente>().Insert(cliente);
                _unitOfWork.Save();
                return new { success = true, message = "Registro Inserido com Sucesso." };
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}\n", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                return new { success = false, message = sb.ToString() };
            }
            catch (Exception e)
            {
                return new { success = false, message = e.Message };
            }
        }

        public IQueryable<Cliente> ReadAll()
        {
            try
            {
                return _unitOfWork.Repository<Cliente>().Query().Filter(u => u.EstReg == "A").Get();
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<Cliente> GetServidoresSde(int id)
        {
            try
            {
                return _unitOfWork.Repository<Cliente>().Query().Filter(u => u.ClienteId == id).Include(u => u.ServidoresSde).Get();
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<Cliente> GetArcgisServers(int id)
        {
            try
            {
                return _unitOfWork.Repository<Cliente>().Query().Filter(u => u.ClienteId == id).Include(u => u.ArcgisServers).Get();
            }
            catch
            {
                throw;
            }
        }



        public Cliente ReadOne(int id)
        {
            try
            {
                return _unitOfWork.Repository<Cliente>().SingleOrDefault(s => s.ClienteId == id);
            }
            catch
            {
                throw;
            }
        }

        public object Update(Cliente cliente)
        {
            try
            {
                _unitOfWork.Repository<Cliente>().Update(cliente);
                _unitOfWork.Save();
                return new { success = true, message = "Registro Atualizado com Sucesso." };
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}\n", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                return new { success = false, message = sb.ToString() };
            }
            catch (Exception e)
            {
                return new { success = false, message = e.Message };
            }
        }

        public object Delete(int id)
        {
            try
            {
                Cliente c = this.ReadOne(id);
                c.EstReg = "H";
                this.Update(c);
                return new { success = true, message = "Registro Deletado com Sucesso." };
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}\n", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                return new { success = false, message = sb.ToString() };
            }
            catch (Exception e)
            {
                return new { success = false, message = e.Message };
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
