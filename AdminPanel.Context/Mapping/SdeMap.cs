using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Models;

namespace AdminPanel.Context.Mapping
{
    public class SdeMap : EntityTypeConfiguration<Sde>
    {
        public SdeMap()
        {
            // Primary Key
            HasKey(t => t.SdeId);

            // Properties
            Property(t => t.Instance)
                .IsRequired();

            Property(t => t.Login)
                .IsRequired();

            Property(t => t.Name)
                .IsRequired();

            Property(t => t.Password)
                .IsRequired();

            Property(t => t.Server)
                .IsRequired();

            Property(t => t.EstReg)
                .IsRequired()
                .HasMaxLength(1);

            Property(t => t.DataReg)
                .IsRequired()
                .HasColumnType("datetime2");

            // Table & Column Mappings
            ToTable("cfg_servidor_sde");
        }
    }
}
