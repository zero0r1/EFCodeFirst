using FluentApiModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FluentApiBreakAwayContext : DbContext
    {

        //如果使用默认初始化器, 数据库名称将是 [nameSpace]DataAccess.[className]FluentApiBreakAwayContext
        public FluentApiBreakAwayContext() { }

        /// <summary>
        /// 可以重定义数据库名称,也可以传递数据库连接字符串
        /// 如果传递数据库连接字符串格式: name=BreakAwayContext
        /// </summary>
        /// <param name="databaseName">数据库名称</param>
        public FluentApiBreakAwayContext(string databaseName) : base(databaseName) { }

        /// <summary>
        /// 重用数据库连接,DbContext另一个构造器，允许您提供一个DbConnection的实例
        /// </summary>
        /// <param name="connection"></param>
        public FluentApiBreakAwayContext(DbConnection connection) : base(connection, contextOwnsConnection: false) { }

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

        public DbSet<ViewDestination> ViewDestinations
        {
            get;
            set;
        }


        //</code>


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //注册配置
            modelBuilder.Configurations.Add(new DestinationConfiguration());
            modelBuilder.Configurations.Add(new LodgingConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new InternetSpecialConfiguration());
            modelBuilder.Configurations.Add(new ReservationConfiguration());
            modelBuilder.Configurations.Add(new ActivityConfiguration());
            modelBuilder.Configurations.Add(new ViewDestinationConfiguration());



            //注册Address类为复杂类型
            //modelBuilder.ComplexType<Address>();

            //对复杂类型的列名施加影响
            //设置Address字段名称,使用FluentApi使其受到影响
            //在ComplexType下设置ColumnName必须使用一下表达式,不能单独设置,否则出错.
            modelBuilder.ComplexType<Address>().Property(a => a.StreetAddress).HasColumnName("StreetAddress");
            modelBuilder.ComplexType<Address>().Property(a => a.City).HasColumnName("City");
            modelBuilder.ComplexType<Address>().Property(a => a.State).HasColumnName("State");
            modelBuilder.ComplexType<Address>().Property(a => a.ZipCode).HasColumnName("ZipCode");


            //注册PersonalInfo类为复杂类型
            modelBuilder.ComplexType<PersonalInfo>();


            //使用Fluent API切分表
            //将Person切分为Person,所有的导航属性将不创建字段,将重新建立为 People 的表
            modelBuilder.Entity<Person>().ToTable("Person");


            //映射到可更新的视图
            //使用Fluent API的ToTable方法来映射到视图
            //modelBuilder.Entity<Destination>().ToTable("TopTenDestinations");
        }

        public class DestinationConfiguration : EntityTypeConfiguration<Destination>
        {
            public DestinationConfiguration()
            {
                //使用Fluent API修改默认列名 HasColumnName
                //Fluent Api 首先需要定义主键，否则会出现缺少主键的错误,再定义主键类型
                HasKey(d => d.DestinationId);
                Property(d => d.DestinationId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                Property(d => d.Name).HasColumnName("LocationID");
                Property(d => d.Name).IsRequired().HasColumnName("LocationName");
                Property(d => d.Description).HasMaxLength(500);
                Property(d => d.Photo).HasColumnType("image");

                //使用Fluent API切分表
                //映射一个单独的实体到多个表
                // ToTable("Locations", "baga"); 
                //<code>
                //Map(m =>
                //{
                //    m.Properties(d => new
                //    {
                //        d.Name,
                //        d.Country,
                //        d.Description
                //    });
                //    m.ToTable("Locations");
                //});
                //Map(m =>
                //{
                //    m.Properties(d => new
                //    {
                //        d.Photo
                //    });
                //    m.ToTable("LocationPhotos");
                //});
                //<code>

                //在Data Annotation无法做到位数精确
                //对Decimal固定有效位数和小数位数的影响,Convention默认规则: Decimals are 18, 2
                //使用Fluent Api可以
                Property(d => d.JustDecimal).HasPrecision(20, 3);


                #region 使用Fluent Api配置数据库关系
                /* Has方法包括如下几个：
                        • HasOptional
                        • HasRequired
                        • HasMany
                 * 在多数情况还需要在Has方法后面跟随如下With方法之一：
                        • WithOptional
                        • WithRequired
                        • WithMany
                        */

                //使用现有的Destination和Lodging之间的一对多关系的实例。
                //这一配置并非真的做任何事，
                //因为这会被Code First通过默认规则同样进行配置(WithOptional)
                //HasMany(d => d.Lodgings).WithOptional(l => l.Destination);

                //使Code First知晓你想建立一个必须的（Required）一对多关系
                HasMany(d => d.Lodgings).WithRequired(l => l.Destination);
                #endregion


            }
        }

        public class LodgingConfiguration : EntityTypeConfiguration<Lodging>
        {
            public LodgingConfiguration()
            {
                //Fluent Api 首先需要定义主键，否则会出现缺少主键的错误,再定义主键类型
                HasKey(l => l.LodgingId).Property(l => l.LodgingId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                Property(l => l.Name).IsRequired().HasMaxLength(200);

                //使用Has/With语句来指定关系的两端
                //第一个配置的一端为Lodging.PrimaryContact，另一端为Person.PrimaryContactFor.
                //第二个配置是针对SecondaryContact和SecondaryContactFor两者关系建立的
                HasOptional(l => l.PrimaryContact).WithMany(p => p.PrimaryContactFor).HasForeignKey(p => p.PrimaryContactId);
                HasOptional(l => l.SecondaryContact).WithMany(p => p.SecondaryContactFor).HasForeignKey(p => p.SecondaryContactId);

                //映射到继承层次结构
                //<code>
                //PART 1
                //Map可以将实体分割成为多个表也可以将默认的 Discriminator 列改变成为 LodgingType
                //将 Lodgings -> 更变为 Standard
                //将 Resort   -> 更变为 Resort
                Map(m =>
                {
                    //控制使用实体切分生成的外键
                    //拆分表必须将所有字段切分,不然会报错
                    //运行一下实例必须解除注释
                    //<code>
                    //m.Properties(d => new
                    //{
                    //    d.Name,
                    //    d.LodgingId,
                    //    d.MilesFromNearestAirport,
                    //});
                    //</code>

                    m.ToTable("Lodgings");
                    m.Requires("LodgingType").HasValue("Standard");

                }).Map<Resort>(m =>
                {
                    m.Requires("LodgingType").HasValue("Resort");
                });

                //控制使用实体切分生成的外键
                //拆分表必须将所有字段切分,不然会报错
                //运行一下实例必须解除注释
                //<code>
                //Map(m =>
                // {
                //     m.Properties(d => new
                //     {
                //         d.Owner,
                //     });
                //     m.ToTable("LodgingInfo");
                // });
                //</code>

                //PART 2
                //可以将默认列 Discriminator 转换成为 bool 类型 名称变更为 IsResort 类型为 bit NOT NULL
                //Map(m =>
                //{
                //    m.ToTable("Lodging");
                //    m.Requires("IsResort").HasValue(false);
                //})
                //.Map<Resort>(m =>
                //{
                //    m.Requires("IsResort").HasValue(true);
                //});
                //</code>


                //控制由Code First生产的外键
                //通过 FluentApi 创建外键
                //变更外键列名只能通过 Fluent API.你可以使用Map方法来控制的映射,也可使用Map方法来控制关系的映射.
                HasRequired(l => l.Destination).WithMany(d => d.Lodgings).Map(c => c.MapKey("DestinationId"));

                //控制使用实体切分生成的外键
                //运行一下实例必须解除注释
                //<code>
                //HasRequired(l => l.Destination).WithMany(d => d.Lodgings).Map(c => c.MapKey("DestinationId").ToTable("LodgingInfo"));
                //</code>
            }
        }

        public class PersonConfiguration : EntityTypeConfiguration<Person>
        {
            public PersonConfiguration()
            {
                //SocialSecurityNumber 属性设置为 System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.None，
                //以指示该值不由数据库生成
                Property(p => p.SocialSecurityNumber).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                /***
                  *
                 *** 
                调用SaveChanges 时，因为BloggerName 字段上具有ConcurrencyCheck 注释，
                所以在更新中将使用该属性的初始值。该命令将尝试通过同时依据键值和 BloggerName 的初始值进行筛选来查找正确的行。
                下面是发送到数据库的 UPDATE 命令的关键部分，
                在其中您可以看到该命令将更新 PrimaryTrackingKey 为 1 且BloggerName 为“Julie”（这是从数据库中检索到该博客时的初始值）的行。
                where (([PrimaryTrackingKey]= @4) and([BloggerName] = @5)) @4=1,@5=N'Julie'
                如果在此期间有人更改了该博客的博主姓名，则此更新将失败，并引发 DbUpdateConcurrencyException 并且需要处理该异常。
                */
                //使用IsConcurrencyToken方法配置并发，并应用于属性
                Property(p => p.SocialSecurityNumber).IsConcurrencyToken();

                //设置主键
                HasKey(p => p.PersonId).Property(p => p.PersonId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            }
        }

        public class InternetSpecialConfiguration : EntityTypeConfiguration<InternetSpecial>
        {
            public InternetSpecialConfiguration()
            {
                //使用Fluent API来修改外键
                //Fluent API并没有提供配置属性作为外键的简单方法。你要使用专门的关系API来配置正确的外键。
                //而且你不能简单地配置关系的片断，你需要首先指定你想配置的关系类型（前面已经提到）然后才能应用修改。
                //为了指定关系，需要从IneternetSpecial实体开始，我们直接在modelBuilder中进行配置
                //定义AccommodationId为外键 Foreign key
                //在默认情况下，我们先要设置关系而不打破Code First建立的默认关系
                //再添加了HasForeignKey方法来为关系指定外键
                HasRequired(s => s.Accommodation).WithMany(l => l.InternetSpecials).HasForeignKey(s => s.AccommodationId);
            }
        }

        //通过 EntityTypeConfiguration 的配置情况下即使不添加DbSet,数据库一样可以建立数据表
        public class ReservationConfiguration : EntityTypeConfiguration<Reservation>
        {
            public ReservationConfiguration()
            {

            }
        }

        //通过 EntityTypeConfiguration 的配置情况下即使不添加DbSet,数据库一样可以建立数据表
        public class ViewDestinationConfiguration : EntityTypeConfiguration<ViewDestination>
        {
            public ViewDestinationConfiguration()
            {
                HasKey(v => v.DestinationId).Property(v => v.DestinationId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                Ignore(v => v.Country);
                Ignore(v => v.Description);
                Ignore(v => v.Photo);
            }
        }

        public class ActivityConfiguration : EntityTypeConfiguration<Trip>
        {
            public ActivityConfiguration()
            {
                //控制多对多关系中的内联表
                //前面根据默认的方式已经在Acitvity和Trip之间引入了多对多关系，最终在数据库中生成 ActivityTrips 内联表
                //但是通过Map方法配置这个表名, 可以使这个表名更加具有意义
                //HasMany(t => t.Activities).WithMany(a => a.Trips).Map(c => c.ToTable("TripActivities"));


                HasMany(t => t.Activities).WithMany(a => a.Trips).Map(c =>
                {
                    c.ToTable("TripActivities");
                    //hasMany is Left Table
                    c.MapLeftKey("TripIdentifier");
                    //withMany is Right Table
                    c.MapRightKey("ActivityId");
                });
            }
        }
    }
}
