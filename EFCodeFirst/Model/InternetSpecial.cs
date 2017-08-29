using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnotationModel
{
    /// <summary>
    /// 这个类被演示做: 指定非规则命名的外键 部分的演示
    /// </summary>
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
        //NOTE: Code First根据Accommodation导航属性，检测到了一个对应对Lodging的关系然后使用默认规则创建了Accommodation_LodgingId字段。
        //默认规则无法将AccommodationId推断为外键，因为Code First检查了默认规则对外键属性名称的三个要求没有在类中找到匹配项，就创建了自己的外键。
        //可以使用 Data Annotations 的配置外键特性ForeignKey来声明外键属性。
        //在AccommodtaionId上添加ForeignKey特性告知Code First哪个导航属性是外键，来修复这个问题。
        //外键属性（AccommodationId）
        public Guid AccommodationId
        {
            get;
            set;
        }


        [ForeignKey("AccommodationId")]
        //你也可以将ForeignKey特性放在导航属性上来通知哪个属性是关系的外键。
        //导航属性（Accommodation）
        public Lodging Accommodation
        {
            get;
            set;
        }
    }
}
