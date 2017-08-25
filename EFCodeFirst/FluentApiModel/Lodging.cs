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
    }
}
