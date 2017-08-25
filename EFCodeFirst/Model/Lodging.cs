using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnotationModel
{
    public class Lodging
    {
        public Guid LodgingId
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Owner
        {
            get;
            set;
        }
        public bool IsResort
        {
            get;
            set;
        }

        public decimal MilesFromNearestAirport
        {
            get;
            set;
        }

        // -与Destination（主键）具有依赖关系
        // 此处Destination属性代表单个Destination实例
        //你可以使用 Data Annotations 的配置外键特性ForeignKey来声明外键属性。
        //在AccommodtaionId上添加ForeignKey特性告知Code First哪个导航属性是外键，来修复这个问题。
        [Required]
        [ForeignKey("DestinationId")]
        public Destination Destination
        {
            get;
            set;
        }

        //你也可以将ForeignKey特性放在导航属性上来通知哪个属性是关系的外键。

        public Guid DestinationId
        {
            get;
            set;
        }

        public List<InternetSpecial> InternetSpecials
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


        //Configuring multiple bidirectional relationships from Lodging to Person
        [InverseProperty("PrimaryContactFor")]
        public Person PrimaryContact
        {
            get;
            set;
        }

        //Configuring multiple bidirectional relationships from Lodging to Person
        [InverseProperty("SecondaryContactFor")]
        public Person SecondaryContact
        {
            get;
            set;
        }

        #endregion
    }
}
