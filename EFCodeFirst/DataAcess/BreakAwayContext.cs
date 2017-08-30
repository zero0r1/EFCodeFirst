using AnnotationModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BreakAwayContext : DbContext
    {
        //定义外部可访问数据
        //<code>
        public DbSet<Destination> Destinations
        {
            get;
            set;
        }
        public DbSet<Lodging> Lodgings
        {
            get;
            set;
        }

        public DbSet<Person> Persons
        {
            get;
            set;
        }

        public DbSet<Trip> Trips
        {
            get;
            set;
        }
        //</code>
    }
}
