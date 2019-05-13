using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GisysConsultBonusSystem.Models
{
    public class ConsultContext: DbContext
    {
        public ConsultContext(DbContextOptions<ConsultContext> options):base(options)
        {
                
        }
        public DbSet<Consult> Consults { get; set; }
        public DbSet<Bonus> Bonus { get; set; }
    }
}
