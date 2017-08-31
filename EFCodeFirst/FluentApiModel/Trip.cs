using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApiModel
{
    public class Trip
    {
        public int TripId
        {
            get;
            set;
        }

        //建立对Activity的多对多关系
        public List<Activity> Activities
        {
            get;
            set;
        }
    }
}
