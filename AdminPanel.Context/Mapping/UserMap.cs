using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Models;

namespace AdminPanel.Context.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            HasKey(t => t.UserId);

            // Properties
            Property(t => t.UserId)
                .IsRequired();

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(32);

            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(128);

            Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(64);

            Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(64);

            Property(t => t.EstReg)
                .IsRequired()
                .HasMaxLength(1);

            Property(t => t.DataReg)
                .IsRequired()
                .HasColumnType("datetime2");

            // Table & Column Mappings
            ToTable("System_Users");
            Property(t => t.UserId).HasColumnName("ID do Usuário");
            Property(t => t.DataReg).HasColumnName("Data de Registro");
            Property(t => t.EstReg).HasColumnName("Ativo?");
            Property(t => t.Name).HasColumnName("Nome");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.Username).HasColumnName("Login");
            Property(t => t.Password).HasColumnName("Senha");
        }
    }
}
