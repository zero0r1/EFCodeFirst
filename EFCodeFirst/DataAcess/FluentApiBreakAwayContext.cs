using FluentApiModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FluentApiBreakAwayContext : DbContext
    {
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DestinationConfiguration());
            modelBuilder.Configurations.Add(new LodgingConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.ComplexType<Address>();
            modelBuilder.ComplexType<PersonalInfo>();
        }

        public class DestinationConfiguration : EntityTypeConfiguration<Destination>
        {
            public DestinationConfiguration()
            {
                //Fluent Api 首先需要定义主键，否则会出现缺少主键的错误
                HasKey(d => d.DestinationId);
                //Fluent Api 再定义主键类型
                Property(d => d.DestinationId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                Property(d => d.Name).IsRequired();
                Property(d => d.Description).HasMaxLength(500);
                Property(d => d.Photo).HasColumnType("image");
            }
        }

        public class LodgingConfiguration : EntityTypeConfiguration<Lodging>
        {
            public LodgingConfiguration()
            {
                //Fluent Api 首先需要定义主键，否则会出现缺少主键的错误
                HasKey(l => l.LodgingId);
                //Fluent Api 再定义主键类型
                Property(l => l.LodgingId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                Property(l => l.Name).IsRequired().HasMaxLength(200);
            }
        }

        public class PersonConfiguration : EntityTypeConfiguration<Person>
        {
            public PersonConfiguration()
            {
                HasKey(l => l.SocialSecurityNumber);
                Property(p => p.SocialSecurityNumber).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            }
        }
    }
}
