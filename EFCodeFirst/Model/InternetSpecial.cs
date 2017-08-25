using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnotationModel
{
    public class InternetSpecial
    {
        public int InternetSpecialId
        {
            get;
            set;
        }
        public int Nights
        {
            get;
            set;
        }
        public decimal CostUSD
        {
            get;
            set;
        }
        public DateTime FromDate
        {
            get;
            set;
        }
        public DateTime ToDate
        {
            get;
            set;
        }

        [ForeignKey("Accommodation")]
        //你可以使用 Data Annotations 的配置外键特性ForeignKey来声明外键属性。
        //在AccommodtaionId上添加ForeignKey特性告知Code First哪个导航属性是外键，来修复这个问题。
        public Guid AccommodationId
        {
            get;
            set;
        }


        [ForeignKey("AccommodationId")]
        //你也可以将ForeignKey特性放在导航属性上来通知哪个属性是关系的外键。
        public Lodging Accommodation
        {
            get;
            set;
        }
    }
}
