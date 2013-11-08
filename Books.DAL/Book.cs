using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

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

    public class Book 
    {
        [NotMapped]
        public static Database db = new Database();

        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Publisher { get; set; }

        public static IEnumerable<Book> Select(Func<Book, bool> predicate=null)
        {
            var result = (predicate == null) ? db.Books.ToList() : db.Books.Where(predicate).ToList();
            return result;
        }

        public void Save()
        {
            db.Books.Add(this);
            db.SaveChanges();
        }

        public void Remove()
        {
            db.Books.Remove(this);
            db.SaveChanges();
        }

        public void Update()
        {
            db.SaveChanges();
        }
    }
}