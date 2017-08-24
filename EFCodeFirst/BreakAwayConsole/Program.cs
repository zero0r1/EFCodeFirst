using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using FluentApiModel;
using DataAccess;
using System.Data.Entity;

namespace BreakAwayConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BreakAwayContext>());
            //InsertDestination();
            //UpdatePersonDestination();

            FluentApiInsertDestination();
            FluentApiUpdatePersonDestination();
            //FluentApiInsertPerson();
            FluentApiUpdatePerson();
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
                    Height = new Measurement
                    {
                        Reading = 100,
                        Units = "200"
                    },
                    Weight = new Measurement
                    {
                        Reading = 200,
                        Units = "300"
                    },
                }
            };

            using (var context = new FluentApiBreakAwayContext())
            {
                context.Destinations.Add(destination);
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
        #endregion

        #region DataAnnotation
        static void InsertDestination()
        {
            var destination = new Model.Destination
            {
                Country = "Indonesia",
                Description = "EcoTourism at its best in exquisite Bali",
                Name = "Bali",
                Address = new Model.Address
                {
                    City = "shanghai",
                    State = "huayi",
                    ZipCode = "000000",
                    StreetAddress = "yishanlu"
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
        #endregion
    }
}
