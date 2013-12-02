using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DAL
{
    public class Database : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public void ToContainer(System.Windows.Forms.BindingSource container)
        {
            container.Clear();
            foreach (var p in Books)
            {
                container.Add(p);
            }
        }
    }
}
