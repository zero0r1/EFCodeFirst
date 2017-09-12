using FluentApiModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DropCreateFluentApiBreakAwayWithSeedData : DropCreateDatabaseAlways<FluentApiBreakAwayContext>
    {
        protected override void Seed(FluentApiBreakAwayContext context)
        {
            /*
            ***
             *
            ***
            */
            //NOTE: 在这里没有调用SaveChanges方法.Seed的基方法会在定制方法之后调用.
            //如果你让VS的编辑器自动实现覆写方法,就会包括一个对base.Seed(context)的调用.
            //你可以不去管他,但是记住要将这行代码放在方法的最后一行.

            /*
            ***
             *
            ***
            */
            //使用数据库初始化进一步影响数据库构架
            //除了使用Code First在数据库中创建种子数据以外,你也可不使用配置或种子数据达到相同目的.
            //例如,你可以想创建Lodgings表中Name字段的索引以加快使用name查询的速度.
            //context.Database.ExecuteSqlCommand("CREATE INDEX IX_Lodgings_Name ON Lodgings (Name)");

            //创建视图
            string sqlScript = @"CREATE VIEW [dbo].[TopTenDestinations]
AS
SELECT  DestinationId, Name, JustDecimal
FROM    dbo.ViewDestinations
GO
";
            context.Database.ExecuteSqlCommand(sqlScript);

            context.Destinations.Add(new Destination
            {
                Country = "Indonesia",
                Description = "EcoTourism at its best in exquisite Bali",
                Name = "Great Barrier Reef",
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
            });
            context.Destinations.Add(new Destination
            {
                Country = "Indonesia",
                Description = "EcoTourism at its best in exquisite Bali",
                Name = "Grand Canyon",
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
            });
        }
    }
}
