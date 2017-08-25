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
    public class Address
    {
        //public int AddressId
        //{
        //    get;
        //    set;
        //}
        [MaxLength(150)]
        public string StreetAddress
        {
            get;
            set;
        }
        public string City
        {
            get;
            set;
        }
        public string State
        {
            get;
            set;
        }
        public string ZipCode
        {
            get;
            set;
        }
    }
}
