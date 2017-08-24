using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Lodging
    {
        public int LodgingId
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

        /// <summary>
        /// -与Destination（主键）具有依赖关系
        /// 此处Destination属性代表单个Destination实例
        /// </summary>
        [Required]
        public Destination Destination
        {
            get;
            set;
        }
    }
}
