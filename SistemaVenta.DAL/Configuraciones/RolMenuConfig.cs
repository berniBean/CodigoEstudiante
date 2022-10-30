using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVenta.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.Entity.Configuraciones
{
    public class RolMenuConfig : IEntityTypeConfiguration<RolMenu>
    {
        public void Configure(EntityTypeBuilder<RolMenu> builder)
        {
            
            builder.HasKey(prop => new { prop.RolId, prop.MenuId });
        }
    }
}
