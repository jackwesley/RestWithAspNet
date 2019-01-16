using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet.Model.Context
{
    public class MySqlContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }


        public MySqlContext()
        {

        }

        public MySqlContext(DbContextOptions<MySqlContext> options)
            : base(options)
        {

        }

    }
}
