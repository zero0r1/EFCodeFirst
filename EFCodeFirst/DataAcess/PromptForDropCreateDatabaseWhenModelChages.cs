using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PromptForDropCreateDatabaseWhenModelChages<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        /// <summary>
        /// 创建一个定制的数据库初始化器
        /// 在数据库删除并重新创建之前给一个提示
        /// </summary>
        /// <param name="context"></param>
        public void InitializeDatabase(TContext context)
        {
            //除了写定制的初始化器,也可以引用别人创建的.
            //有一个例子EFCodeFirst.CreateTablesOnly NuGet 包.
            //这个初始化器允许你在已经存在的数据库进行删除和创建操作,而不是删除和创建数据库实体本身.
            //当你将数据库指向一个宿主数据库而又没有权限删除和创建整个数据库时特别有用.

            // If the database exists and matches the model
            // there is nothing to do 
            var exists = context.Database.Exists();
            if (exists && context.Database.CompatibleWithModel(true))
            {
                return;
            }
            // If the database exists and doesn't match the model 
            // then prompt for input 
            if (exists)
            {
                Console.WriteLine("Existing database doesn't match the model!");
                Console.Write("Do you want to drop and create the database? (Y/N): ");
                var res = Console.ReadKey();
                Console.WriteLine();
                if (!String.Equals("Y", res.KeyChar.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
                context.Database.Delete();
            }
            // Database either didn't exist or it didn't match 
            // the model and the user chose to delete it 
            context.Database.Create();
        }
    }
}
