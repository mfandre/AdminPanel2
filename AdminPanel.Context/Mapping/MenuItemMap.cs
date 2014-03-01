using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Models.Menu;

namespace AdminPanel.Context.Mapping
{
    class MenuItemMap : EntityTypeConfiguration<MenuItem>
    {
    
        public MenuItemMap()
        {
            // Table & Column Mappings
            ToTable("seg_funcao");

            Property(t => t.DataReg)
                .IsRequired()
                .HasColumnType("datetime2");

            HasMany(u => u.Children).WithOptional(u => u.Parent);
                
        }
    }
}
