using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_.net7.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Character> Characters => Set<Character>();
    }
}