using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;

namespace Infrastructure.Data
{
    public class BikesDbContext:DbContext
    {
        public BikesDbContext(DbContextOptions<BikesDbContext> options):base(options)
        {

        }
        public DbSet<Bikes> tblBikes{set;get;}
    }
}