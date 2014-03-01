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
    public class SdeDAO
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new MainContext());

        public object Create(Sde servidor)
        {
            try
            {
                _unitOfWork.Repository<Sde>().Insert(servidor);
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

        public IQueryable<Sde> ReadAll()
        {
            try
            {
                return _unitOfWork.Repository<Sde>().Query().Filter(u => u.EstReg == "A").Get();
            }
            catch
            {
                throw;
            }
        }

        public Sde ReadOne(int id)
        {
            try
            {
                return _unitOfWork.Repository<Sde>().SingleOrDefault(s => s.SdeId == id);
            }
            catch
            {
                throw;
            }
        }

        public object Update(Sde servidor)
        {
            try
            {
                _unitOfWork.Repository<Sde>().Update(servidor);
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

        public object Delete()
        {
            return null;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
