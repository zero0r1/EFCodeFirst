using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnnotationModel
{
    [ComplexType]
    public class PersonalInfo
    {
        public Measurement Width
        {
            get;
            set;
        }

        public Measurement Height
        {
            get;
            set;
        }

        //对复杂类型的列名施加影响
        [Column("DietryRestrictions")]
        public string DietryRestrictions
        {
            get;
            set;
        }
    }

    public class Measurement
    {

        public decimal Reading
        {
            get;
            set;
        }

        public string Units
        {
            get;
            set;
        }
    }
}
