using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnotationModel
{

    [ComplexType]
    //使用Data Annotations 配置表和构架名
    [Table("Address")]
    public class Address
    {
        //复杂类型不需要指定ID
        //public int AddressId
        //{
        //    get;
        //    set;
        //}

        //对复杂类型的列名施加影响
        [MaxLength(150), Column("StreetAddress")]
        public string StreetAddress
        {
            get;
            set;
        }

        //对复杂类型的列名施加影响
        [Column("City")]
        public string City
        {
            get;
            set;
        }

        //对复杂类型的列名施加影响
        [Column("State")]
        public string State
        {
            get;
            set;
        }

        //对复杂类型的列名施加影响
        [Column("ZipCode")]
        public string ZipCode
        {
            get;
            set;
        }
    }
}
