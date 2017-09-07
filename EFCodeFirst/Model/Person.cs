using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnotationModel
{
    [Table("Person")]
    public class Person
    {
        public Guid PersonId
        {
            get;
            set;
        }

        /***
          *
         ***
        场景:
        调用SaveChanges 时，因为BloggerName 字段上具有ConcurrencyCheck 注释，
        所以在更新中将使用该属性的初始值。该命令将尝试通过同时依据键值和 BloggerName 的初始值进行筛选来查找正确的行。
        下面是发送到数据库的 UPDATE 命令的关键部分，
        在其中您可以看到该命令将更新 PrimaryTrackingKey 为 1 且BloggerName 为“Julie”（这是从数据库中检索到该博客时的初始值）的行。
        where (([PrimaryTrackingKey]= @4) and([BloggerName] = @5)) @4=1,@5=N'Julie'
        如果在此期间有人更改了该博客的博主姓名，则此更新将失败，并引发 DbUpdateConcurrencyException 并且需要处理该异常。
        */
        //使用IsConcurrencyToken方法配置并发，并应用于属性
        [ConcurrencyCheck]
        public int SocialSecurityNumber
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

        //对应Lodging.PrimaryContact
        public List<Lodging> PrimaryContactFor
        {
            get;
            set;
        }

        //对应Lodging.SecondaryContact
        public List<Lodging> SecondaryContactFor
        {
            get;
            set;
        }
        #endregion
    }
}
