#define FluentApi
//#define Annotation
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnnotationModel;
using FluentApiModel;
using DataAccess;
using System.Data.Entity;
using System.Data.SqlClient;

namespace BreakAwayConsole
{
    class Program
    {
        //设置数据库名称,或者设置数据库字段连接,格式: name=[数据库连接名称]
        //该数据库连接查找的位置是, App.config -> connectionStrings -> name="BreakAwayContext" 的数据库连接字符串
        static string nameOrConnectionString = "name=BreakAwayContext";

        static void Main(string[] args)
        {

            /***
              *
             ***
            数据库初始化策略：
            1.CreateDatabaseIfNotExists：这是默认的策略。如果数据库不存在，那么就创建数据库。但是如果数据库存在了，而且实体发生了变化，就会出现异常。
            2.DropCreateDatabaseIfModelChanges：此策略表明，如果模型变化了，数据库就会被重新创建，原来的数据库被删除掉了。
            3.DropCreateDatabaseAlways：此策略表示，每次运行程序都会重新创建数据库，这在开发和调试的时候非常有用。
            4.自定制数据库策略：可以自己实现IDatabaseInitializer来创建自己的策略。或者从已有的实现了IDatabaseInitializer接口的类派生。
            */

            /***
              *
             ***
            如果不想使用策略，就可以关闭策略，特别是默认策略
            Database.SetInitializer<UserManContext>(null);
            < addkey = "DatabaseInitializerForTypeEFCodeFirstSample.UserManContext, EFCodeFirstSample" value = "Disabled" />
            */

            //当然这一方法,也并不是 100% 能够删除数据库的你需要关闭所有的数据库引用,包括任何 SqlServer Management
            //<code>

            //当发现数据库结构发生改变,自动删除数据库并重新创建新结构
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BreakAwayContext>());

            //当发现数据库结构发生改变,自动删除数据库并重新创建新结构
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<FluentApiBreakAwayContext>());

            //永远是删除数据库的, 并且创建一个新的空的数据库
            //Database.SetInitializer(new DropCreateDatabaseAlways<BreakAwayContext>());
            Database.SetInitializer(new DropCreateDatabaseAlways<FluentApiBreakAwayContext>());

            //在数据库删除之后通过 override void Seed 方法添加种子数据
            Database.SetInitializer(new DropCreateFluentApiBreakAwayWithSeedData());

            //一个自定义的数据库初始化器,如果模型发生改变,就会提示是否删除数据库并且重新生成.
            //Database.SetInitializer(new PromptForDropCreateDatabaseWhenModelChages<BreakAwayContext>());
            //Database.SetInitializer(new PromptForDropCreateDatabaseWhenModelChages<FluentApiBreakAwayContext>());

            //</code>


#if Annotation
UpdatePersonDestination();
DeleteDestinationInMemoryAndDbCascade();
//DeleteDestinationInMemoryAndDbCascade2();
SelectTripWithActivities();
#endif

#if FluentApi


#if false
FluentApiReuseDbConnection();
#endif

            FluentApiQueryDestinationView();
            FluentApiInsertDestination();
            FluentApiUpdatePersonDestination();
            FluentApiDeleteDestinationInMemoryAndDbCascade();
            FluentApiInsertLodging();
            FluentApiInsertResort();
            FluentApiGreatBarrierReefTest();

#endif


        }

        #region Fluent Api
        static void FluentApiInsertDestination()
        {
            var destination = new FluentApiModel.Destination
            {
                Country = "Indonesia",
                Description = "EcoTourism at its best in exquisite Bali",
                Name = "Bali",
                Address = new FluentApiModel.Address
                {
                    City = "shanghai",
                    State = "huayi",
                    ZipCode = "000000",
                    StreetAddress = "yishanlu"
                },
                Info = new FluentApiModel.PersonalInfo
                {
                    DietryRestrictions = "DietryRestrictions",
                    Height = new FluentApiModel.Measurement
                    {
                        Reading = 100,
                        Units = "200"
                    },
                    Width = new FluentApiModel.Measurement
                    {
                        Reading = 200,
                        Units = "300"
                    },
                },
                Lodgings = new List<FluentApiModel.Lodging>() {
        new FluentApiModel.Lodging
       {
           Name = "lodging Name",
           Owner = "lodging Owner",
           //IsResort = true,
           MilesFromNearestAirport = 1.1M
       },
        new FluentApiModel.Lodging
        {
            Name = "lodging Name2",
           Owner = "lodging Owner2",
           //IsResort = true,
           MilesFromNearestAirport = 2.2M
        }
   }
            };


            using (var context = new FluentApiBreakAwayContext(nameOrConnectionString))
            {
                context.Destinations.Add(destination);
                context.SaveChanges();
            }

            using (var context = new FluentApiBreakAwayContext(nameOrConnectionString))
            {
                var destinationsArray = context.Destinations.ToList();
                var destinationFirst = destinationsArray[0];
                destinationFirst.Description += "Trust us, you'll love it!";
                context.SaveChanges();
                //context.Destinations.Remove(destinationFirst);
                context.SaveChanges();
            }
        }

        static void FluentApiUpdatePersonDestination()
        {
            using (var context = new FluentApiBreakAwayContext(nameOrConnectionString))
            {
                var destination = context.Destinations.FirstOrDefault();
                destination.Country = "Rowena";
                context.SaveChanges();
            }
        }

        static void FluentApiUpdatePerson()
        {
            using (var context = new FluentApiBreakAwayContext(nameOrConnectionString))
            {
                var person = context.Persons.FirstOrDefault();
                person.FirstName = "Rowena";
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 这个功能演示联级删除 FluentApi
        /// </summary>
        static void FluentApiDeleteDestinationInMemoryAndDbCascade()
        {
            Guid destinationId;
            using (var context = new FluentApiBreakAwayContext(nameOrConnectionString))
            {
                var destination = new FluentApiModel.Destination
                {
                    Name = "Sample Destination",
                    Address = new FluentApiModel.Address
                    {
                        City = "City",
                        StreetAddress = "StreetAddress",
                        State = "State",
                        ZipCode = "ZipCode"
                    },
                    Info = new FluentApiModel.PersonalInfo
                    {
                        DietryRestrictions = "DietryRestrictions",
                        Height = new FluentApiModel.Measurement
                        {
                            Reading = 0.1M,
                            Units = "0.2"
                        },
                        Width = new FluentApiModel.Measurement
                        {
                            Reading = 1.1M,
                            Units = "1.2"
                        }
                    },
                    Lodgings = new List<FluentApiModel.Lodging>
       {
           new FluentApiModel.Lodging
           {
               Name = "Lodging One"
           },
           new FluentApiModel.Lodging
           {
               Name = "Lodging Two"
           }
       }
                };

                context.Destinations.Add(destination);
                context.SaveChanges();
                destinationId = destination.DestinationId;
            }
            using (var context = new FluentApiBreakAwayContext(nameOrConnectionString))
            {
                var destination = context.Destinations.Include("Lodgings").Single(d => d.DestinationId == destinationId);
                var aLodging = destination.Lodgings.FirstOrDefault();
                context.Destinations.Remove(destination);
                Console.WriteLine("State of one Lodging: {0}", context.Entry(aLodging).State.ToString());
                context.SaveChanges();
            }
        }
        //static void FluentApiInsertPerson()
        //{
        //    var person = new FluentApiModel.Person
        //    {
        //        FirstName = "Rowan",
        //        LastName = "Miller",
        //        SocialSecurityNumber = 12345678
        //    };
        //    using (var context = new FluentApiBreakAwayContext())
        //    {
        //        context.Persons.Add(person);
        //        context.SaveChanges();
        //    }
        //}

        /// <summary>
        /// 映射到继承层次结构
        /// 由EF框架生成的INSERT语句会将字符串 "Lodging" 放进新加入行的Discriminator列中
        /// </summary>
        static void FluentApiInsertLodging()
        {
            var lodging = new FluentApiModel.Lodging
            {
                Name = "Rainy Day Motel",
                Destination = new FluentApiModel.Destination
                {
                    Name = "Seattle, Washington",
                    Country = "USA",
                    Address = new FluentApiModel.Address
                    {
                        City = "City",
                    },
                    Info = new FluentApiModel.PersonalInfo
                    {
                        DietryRestrictions = "DietryRestrictions",
                        Width = new FluentApiModel.Measurement
                        {
                            Reading = 1M,
                            Units = "Units"
                        },
                        Height = new FluentApiModel.Measurement
                        {
                            Reading = 2M,
                            Units = "Units2"
                        }
                    }
                }
            };
            using (var context = new FluentApiBreakAwayContext(nameOrConnectionString))
            {
                context.Lodgings.Add(lodging);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 由EF框架生成的INSERT语句会将字符串 "Resort" 放进新加入行的Discriminator列中
        /// </summary>
        static void FluentApiInsertResort()
        {
            //创建Resort类型的实例
            var resort = new FluentApiModel.Resort
            {
                Name = "Top Notch Resort and Spa",
                MilesFromNearestAirport = 30,
                Activities = "Spa, Hiking, Skiing, Ballooning",
                Destination = new FluentApiModel.Destination
                {
                    Name = "Stowe, Vermont",
                    Country = "USA",
                    Address = new FluentApiModel.Address
                    {
                        City = "City",
                    },
                    Info = new FluentApiModel.PersonalInfo
                    {
                        DietryRestrictions = "DietryRestrictions",
                        Width = new FluentApiModel.Measurement
                        {
                            Reading = 1M,
                            Units = "Units"
                        },
                        Height = new FluentApiModel.Measurement
                        {
                            Reading = 2M,
                            Units = "Units2"
                        }
                    }
                }
            };
            using (var context = new FluentApiBreakAwayContext(nameOrConnectionString))
            {
                //并且要保存到 Lodgings 它的基类中
                //EF框架将会在Discriminator列中插入字符串 "Resort".
                context.Lodgings.Add(resort);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 数据库数据库初始化器添加种子数据
        /// </summary>
        static void FluentApiGreatBarrierReefTest()
        {
            //一个依赖于包含一些已知数据的数据库的测试
            //种子数据可以用另一种情况是运行集成测试
            //验证“Great Barrier Reef”是否为数据库中的Destination条目
            using (var context = new FluentApiBreakAwayContext(nameOrConnectionString))
            {
                var reef = from destination in context.Destinations
                           where destination.Name == "Great Barrier Reef"
                           select destination;

                if (reef.Count() == 1)
                {
                    Console.WriteLine("Test Passed: 1 'Great Barrier Reef' destination found");
                }
                else
                {
                    Console.WriteLine("Test Failed: {0} 'Great Barrier Reef' destinations found", context.Destinations.Count());
                }
            }
        }

        static void FluentApiQueryDestinationView()
        {
            using (var context = new FluentApiBreakAwayContext(nameOrConnectionString))
            {
                //使用视图填充对象
                //var destinations = context.Destinations.SqlQuery("SELECT * FROM Destinations");


                //SqlQuery函数的方法依赖于在查询结果集的列名和对象属性名的精确匹配。
                //由于目标类包含DestinationId，Name和其他属性，视图必须返回与其相同名称的列。
                //如果视图没有与类属性相同的列名，需要在SELECT语句中的为列设置别名。 
                //例如，TopTenDestinations视图使用Id而不是DestinationId作为主键的名称。

                //    var destinations2 = context.ViewDestinations.SqlQuery(@"
                //SELECT
                //    DestinationId,
                //    JustDecimal,
                //    Name
                //  FROM TopTenDestinations");

                var destinations3 = context.Database.SqlQuery<ViewDestination>(@"
   SELECT
       DestinationId,
       JustDecimal,
       Name
     FROM TopTenDestinations");

                var reef = from destination in destinations3
                           select destination;

                foreach (var item in reef)
                {
                    Console.WriteLine(item.Name + item.JustDecimal.ToString());
                }
            }
        }
        #endregion

        #region DataAnnotation
        static void SelectTripWithActivities()
        {
            //多对多关系
            var trips = new AnnotationModel.Trip
            {
                Activities = new List<AnnotationModel.Activity>()
    {
        new AnnotationModel.Activity
        {
             Name="Name",
        }
    }
            };

            using (var context = new BreakAwayContext())
            {
                context.Trips.Add(trips);
                context.SaveChanges();
                var tripWithActivities = context.Trips.Include("Activities").FirstOrDefault();
            }
        }

        static void InsertDestination()
        {
            var destination = new AnnotationModel.Destination
            {
                Country = "Indonesia",
                Description = "EcoTourism at its best in exquisite Bali",
                Name = "Bali",
                Address = new AnnotationModel.Address
                {
                    City = "shanghai",
                    State = "huayi",
                    ZipCode = "000000",
                    StreetAddress = "yishanlu"
                },
                Info = new AnnotationModel.PersonalInfo
                {
                    DietryRestrictions = "DietryRestrictions",
                    Height = new AnnotationModel.Measurement
                    {
                        Reading = 0.1M,
                        Units = "0.2"
                    },
                    Width = new AnnotationModel.Measurement
                    {
                        Reading = 1.1M,
                        Units = "1.2"
                    }
                }
            };

            using (var context = new BreakAwayContext())
            {
                context.Destinations.Add(destination);
                context.SaveChanges();
            }
        }
        static void UpdatePersonDestination()
        {
            using (var context = new BreakAwayContext())
            {
                var destination = context.Destinations.FirstOrDefault();
                destination.Country = "Rowena";
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 这个功能演示联级删除 Annotation
        /// </summary>
        static void DeleteDestinationInMemoryAndDbCascade()
        {
            Guid destinationId;
            using (var context = new BreakAwayContext())
            {
                var destination = new AnnotationModel.Destination
                {
                    Name = "Sample Destination",
                    Address = new AnnotationModel.Address
                    {
                        City = "City",
                        StreetAddress = "StreetAddress",
                        State = "State",
                        ZipCode = "ZipCode"
                    },
                    Info = new AnnotationModel.PersonalInfo
                    {
                        DietryRestrictions = "DietryRestrictions",
                        Height = new AnnotationModel.Measurement
                        {
                            Reading = 0.1M,
                            Units = "0.2"
                        },
                        Width = new AnnotationModel.Measurement
                        {
                            Reading = 1.1M,
                            Units = "1.2"
                        }
                    },
                    Lodgings = new List<AnnotationModel.Lodging>
       {
           new AnnotationModel.Lodging
           {
               Name = "Lodging One"
           },
           new AnnotationModel.Lodging
           {
               Name = "Lodging Two"
           }
       }
                };

                context.Destinations.Add(destination);
                context.SaveChanges();
                destinationId = destination.DestinationId;
            }
            using (var context = new BreakAwayContext())
            {
                var destination = context.Destinations.Include("Lodgings").Single(d => d.DestinationId == destinationId);
                var aLodging = destination.Lodgings.FirstOrDefault();
                context.Destinations.Remove(destination);
                Console.WriteLine("State of one Lodging: {0}", context.Entry(aLodging).State.ToString());
                context.SaveChanges();
            }
        }


        static void DeleteDestinationInMemoryAndDbCascade2()
        {
            //NOTE:
            //随同Destination一起删除以前存入的Loading数据。我们删除与Lodging提到的所有相关代码。
            //由于内存中无Lodging，就不会有客户端的级联删除，而数据库却清除了任何孤立的Lodgings数据，
            //这是因为在数据库中定义了级联删除。

            Guid destinationId;
            using (var context = new BreakAwayContext())
            {
                var destination = new AnnotationModel.Destination
                {
                    Name = "Sample Destination",
                    Address = new AnnotationModel.Address
                    {
                        City = "City"
                    },
                    Lodgings = new List<AnnotationModel.Lodging>
                   {
                       new AnnotationModel.Lodging
                       {
                           Name = "Lodging One"
                       },
                       new AnnotationModel.Lodging
                       {
                           Name = "Lodging Two"
                       }
                   },
                    Info = new AnnotationModel.PersonalInfo
                    {
                        DietryRestrictions = "DietryRestrictions",
                        Width = new AnnotationModel.Measurement
                        {
                            Reading = 2.1M,
                            Units = "Units"
                        },
                        Height = new AnnotationModel.Measurement
                        {
                            Reading = 3.1M,
                            Units = "Units2"
                        }
                    }
                };
                context.Destinations.Add(destination);
                context.SaveChanges();
                destinationId = destination.DestinationId;
            }
            using (var context = new BreakAwayContext())
            {
                var destination = context.Destinations.Single(d => d.DestinationId == destinationId);
                context.Destinations.Remove(destination);
                context.SaveChanges();
            }
            using (var context = new BreakAwayContext())
            {
                var lodgings = context.Lodgings.Where(l => l.DestinationId == destinationId).ToList();
                Console.WriteLine("Lodgings: {0}", lodgings.Count);
            }
        }
        #endregion


        /// <summary>
        /// 自定义链接字符串,创建一个自定义的DbConnection
        /// </summary>

#if false
static void FluentApiReuseDbConnection()
{
var cstr = @"Server=.\SQLEXPRESS;
Database=BreakAwayContext;
Trusted_Connection=true";

using (var connection = new SqlConnection(cstr))
{
   using (var context = new FluentApiBreakAwayContext(connection))
   {
       foreach (var destination in context.Destinations)
       {
           Console.WriteLine(destination.Name);
       }
   }
}
} 
#endif
    }
}
