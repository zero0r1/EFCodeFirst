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

namespace BreakAwayConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //当发现数据库结构发生改变,自动删除数据库并重新创建新结构
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BreakAwayContext>());

            //当发现数据库结构发生改变,自动删除数据库并重新创建新结构
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<FluentApiBreakAwayContext>());


#if Annotation

            InsertDestination();
            UpdatePersonDestination();
            DeleteDestinationInMemoryAndDbCascade();
            //DeleteDestinationInMemoryAndDbCascade2();
            SelectTripWithActivities();
#endif
#if FluentApi

            FluentApiInsertDestination();
            FluentApiUpdatePersonDestination();
            FluentApiDeleteDestinationInMemoryAndDbCascade();
            FluentApiInsertLodging();
            FluentApiInsertResort();
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


            using (var context = new FluentApiBreakAwayContext())
            {
                context.Destinations.Add(destination);
                context.SaveChanges();
            }

            using (var context = new FluentApiBreakAwayContext())
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
            using (var context = new FluentApiBreakAwayContext())
            {
                var destination = context.Destinations.FirstOrDefault();
                destination.Country = "Rowena";
                context.SaveChanges();
            }
        }

        static void FluentApiUpdatePerson()
        {
            using (var context = new FluentApiBreakAwayContext())
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
            using (var context = new FluentApiBreakAwayContext())
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
            using (var context = new FluentApiBreakAwayContext())
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
            using (var context = new FluentApiBreakAwayContext())
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
            using (var context = new FluentApiBreakAwayContext())
            {
                //并且要保存到 Lodgings 它的基类中
                //EF框架将会在Discriminator列中插入字符串 "Resort".
                context.Lodgings.Add(resort);
                context.SaveChanges();
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
    }
}
