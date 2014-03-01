using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Models.ClientSideErrors;

namespace AdminPanel.Context.Mapping
{
    public class ClientSideJavascriptErrorMap : EntityTypeConfiguration<ClientSideJavaScriptException>
    {
        public ClientSideJavascriptErrorMap()
        {
            // Table & Column Mappings
            ToTable("log_javascript_error");

            Property(t => t.DataReg)
                .IsRequired()
                .HasColumnType("datetime2");
        }
    }
}
