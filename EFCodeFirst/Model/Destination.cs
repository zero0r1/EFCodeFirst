using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnotationModel
{

    //映射到可更新的视图
    //使用Fluent API的ToTable方法来映射到视图
    //[Table("my_detination_view")]
    public class Destination
    {
        //一个重要的数据库功能是可以使用计算属性
        //如果您将 Code First 类映射到包含计算列的表，则您可能不想让实体框架尝试更新这些列
        //。但是在插入或更新数据后，您的确需要 EF 从数据库中返回这些值。
        //您可以使用 DatabaseGenerated 注释与 Computed 枚举一起在您的类中标注这些属性。其他枚举为 None 和Identity。
        //如果并不想让 DestinationId 成为主键,因为它与类名加“Id”(不分大小写)匹配就会成为主键,此时使用 DatabaseGeneratedOption.Nope 就可以不当做主键
        [Column("LocationID"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DestinationId
        {
            get;
            set;
        }

        [Required]
        public string Name
        {
            get;
            set;
        }
        public string Country
        {
            get;
            set;
        }

        public decimal JustDecimal
        {
            get;
            set;
        }

        [MaxLength(500)]
        public string Description
        {
            get;
            set;
        }

        [Column(TypeName = "image")]
        public byte[] Photo
        {
            get;
            set;
        }

        /// <summary>
        /// 通过建立类型为List<Lodging>的Lodging属性Destination建立了联系
        /// Code First观察到您既定义了一个引用又有一个集合导航属性，
        /// 因此引用默认规则将其配置为一对多关系。
        /// 基于此，Code First可以确定Lodging（外键）-
        /// </summary>
        //单边导航功能就是用 [ForeignKey("LocationId")] 查找Lodging.LocationId字段
        //如使用FluentApi: modelBuilder.Entity <Destination>().HasMany(d =>d.Lodgings).WithRequired().HasForeignKey(l =>l.LocationId);
        //[ForeignKey("LocationId")]
        public List<Lodging> Lodgings
        {
            get;
            set;
        }

        public Address Address
        {
            get;
            set;
        }

        public PersonalInfo Info
        {
            get;
            set;
        }
    }
}
