using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApiModel
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

        //Code First 自动检测 DestinationId 是一个外键
        //，对应于 Lodging 与 Destination 的关系，
        //不再产生 Destination_DestinationId 外键
        //在设置外键同时可以注释掉
        public Destination Destination
        {
            get;
            set;
        }

        //外键
        //设置外键的同时,他的类型必须与 (Guid)Destination.DestinationId 类型一致否则无法自动创建外键.
        //Code First默认约定就是 根据类中外键属性的可空性，来确定是否关系是Required还是Optional的。
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


        public Person PrimaryContact
        {
            get;
            set;
        }

        public Person SecondaryContact
        {
            get;
            set;
        }

        #endregion
    }
}
