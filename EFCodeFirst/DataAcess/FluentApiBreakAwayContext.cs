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
        //</code>


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //注册配置
            modelBuilder.Configurations.Add(new DestinationConfiguration());
            modelBuilder.Configurations.Add(new LodgingConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new InternetSpecialConfiguration());

            //注册Address类为复杂类型
            modelBuilder.ComplexType<Address>();

            //注册PersonalInfo类为复杂类型
            modelBuilder.ComplexType<PersonalInfo>();
        }

        public class DestinationConfiguration : EntityTypeConfiguration<Destination>
        {
            public DestinationConfiguration()
            {
                //Fluent Api 首先需要定义主键，否则会出现缺少主键的错误,再定义主键类型
                HasKey(d => d.DestinationId).Property(d => d.DestinationId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


                Property(d => d.Name).IsRequired();
                Property(d => d.Description).HasMaxLength(500);
                Property(d => d.Photo).HasColumnType("image");

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
                //HasMany(d => d.Lodgings).WithRequired(l => l.Destination);
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

                HasOptional(l => l.PrimaryContact).WithMany(p => p.PrimaryContactFor);
                HasOptional(l => l.SecondaryContact).WithMany(p => p.SecondaryContactFor);
            }
        }

        public class PersonConfiguration : EntityTypeConfiguration<Person>
        {
            public PersonConfiguration()
            {
                //SocialSecurityNumber 属性设置为System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.None，
                //以指示该值不由数据库生成
                Property(p => p.SocialSecurityNumber).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

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
                //定义AccommodationId为外键 Foreign key
                HasRequired(s => s.Accommodation).WithMany(l => l.InternetSpecials).HasForeignKey(s => s.AccommodationId);
            }
        }
    }
}
