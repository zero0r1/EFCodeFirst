using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace AnnotationModel
{
    public class Activity
    {
        public int ActivityId
        {
            get;
            set;
        }
        [Required, MaxLength(50)]
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