using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApiModel
{
    //属性的可访问性，Getters和Setters
    //1.public属性将会被Code First自动映射。
    //2.Set访问器可以用更严格的访问规则界定，但get访问器必须保持public才能被自动映射；
    //3.非公开的属性必须使用Fluent API配置才能被Code First所映射；
    //对于非公开属性，这意味只有执行配置的位置才能访问该属性。
    //例如，如果Person类有一个internal Name的属性，使用与PersonContext类相同的程序集，你可以在Person context的OnModelCreating方法中调用modelBuilder.Entity<Person>().Property(p => p.Name)。这就会使用该属性包含进你的方法。但是如果Person和PersonContext定义在单独的程序集中，你就需要添加一个PersonConfiguration类（EntityConfiguration<Person>)到Person的同一程序集中，以执行配置类内的配置。这要求包含有域类的程序集必须添加对EntityFramework.dll的引用。PersonCOnfig配置类可以在PersonContext的OnModelCreating方法中被注册。
    //类似的方法可以用于受保护的和私有的属性。但是，配置类必须嵌套在类内部，成为模型的一部分，这样才能访问私有或受保护的属性。下面是一个这样的例子，使用private隐藏了Name属性，但是允许外部代码使用CreatePerson方法对Name属性进行设置。嵌套的PersonConfig类可以访问本地复制的Name属性。

    //当我们配置类为为嵌套类时，可以使用下列代码：
    //modelBuilder.Configurations.Add(new Person.PersonConfig());

    //一个常见的场景是为了避免开发者在代码中修改某个特定的属性（如PersonId)，使用set访问器来将属性设置为private或internal.
    //这种场景的实现归功于上述所列的第二个规则：Set访问器可以用更严格的访问规则界定，但get访问器必须保持public才能被自动映射；
    //EF框架必须使用反射才能访问非公开的set访问器，但当运行于中等信任的模式时这并不提供支持。除了中等信任的情况以外，
    //这意味着当真实对象作为查询或插入的结果时，上下文将能够填充受限的属性。
    //上下文也能够以查询或插入的数据为属性设置值--即使上下文和域类处于单独的程序集或名称空间里。
    //这即可以工作在有键值的情况，也可以工作在没有键值的情况下。

    //定义类
    //public class Person
    //{
    //    public int PersonId
    //    {
    //        get;
    //        set;
    //    }
    //私有字段
    //    private string Name
    //    {
    //        get;
    //        set;
    //    }
    //通过配置的方式添加到数据库
    //    public class PersonConfig : EntityTypeConfiguration<Person>
    //    {
    //        public PersonConfig()
    //        {
    //            Property(b = > b.Name);
    //        }
    //    }
    //    public string GetName()
    //    {
    //        return this.Name;
    //    }
    //    public static Person CreatePerson(string name)
    //    {
    //        return new Person
    //        {
    //            Name = name
    //        };
    //    }
    //}

    public class Person
    {
        public Guid PersonId
        {
            get;
            set;
        }

        public Guid SocialSecurityNumber
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }

        #region 使用逆导航属性
        /*
         * Ref: http://www.aizhengli.com/entity-framework-code-first/78/entity-framework-code-first-relation.html
        Code First 到目前为止一直能够解析我们定义的两个导航属性，虽然它们处于不同端，实际上是同一关系。
        它之所以能做到这一点，因为两端至少有一个可能的匹配关系。例如， Lodging 只包含一个单一的属性，
        指向目的地（ Lodging.Destination ） ; 同样地，目的地 Destination 只包含一个属性引用住所（ Destination.Lodgings ）。
        虽然并不十分普遍，您可能会遇到这样一种情况：实体之间存在多个关系。在这种情况下，
        Code First 将不能够与相关导航属性相匹配。您将需要提供一些额外的配置。
        例如，如果你想跟踪每个住所的两个联系人怎么办？这就需要在Lodging类中有一个PromaryContact和一个SecondaryContact属性。
        */
        public List<Lodging> PrimaryContactFor
        {
            get;
            set;
        }
        public List<Lodging> SecondaryContactFor
        {
            get;
            set;
        }
        #endregion


        //回顾: 
        //根据默认规则，由于Person是模型中的类，而Person找到了Reservationod ,Code First就会将Reservation类放进模型中。
        //现在来看看第三种默认规则：通过提供一个配置将类包括在模型中。
        //首先我们将Person类中的Reservation属性注释掉：
        //// public List<Reservation> Reservations { get; set; }
        //使用EntityTypeConfiguration 建立关系
        public List<Reservation> Reservations
        {
            get;
            set;
        }
    }
}
