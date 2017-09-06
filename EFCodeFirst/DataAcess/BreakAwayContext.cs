using AnnotationModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BreakAwayContext : DbContext
    {
        public BreakAwayContext() { }

        /// <summary>
        /// 可以重定义数据库名称,也可以传递数据库连接字符串
        /// 如果传递数据库连接字符串格式: name=BreakAwayContext
        /// </summary>
        /// <param name="databaseName">数据库名称</param>
        public BreakAwayContext(string databaseName) : base(databaseName) { }

        /// <summary>
        /// 重用数据库连接,DbContext另一个构造器，允许您提供一个DbConnection的实例
        /// </summary>
        /// <param name="connection"></param>
        public BreakAwayContext(DbConnection connection) : base(connection, contextOwnsConnection: false) { }

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
