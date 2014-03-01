using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Models;

namespace AdminPanel.Context.Mapping
{
    public class ClienteMap : EntityTypeConfiguration<Cliente>
    {
        public ClienteMap()
        {
            // Table & Column Mappings
            ToTable("cfg_cliente");

            Property(t => t.DataReg)
                .IsRequired()
                .HasColumnType("datetime2");
        }
    }
}
