using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApiModel
{
    public class PersonalInfo
    {
        public Measurement Weight
        {
            get;
            set;
        }
        public Measurement Height
        {
            get;
            set;
        }
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
