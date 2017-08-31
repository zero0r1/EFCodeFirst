using System.Collections.Generic;
namespace FluentApiModel
{
    public class Activity
    {
        public int ActivityId
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }

        //建立对Trip的多对多关系
        public List<Trip> Trips
        {
            get;
            set;
        }
    }
}