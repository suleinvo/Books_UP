using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Books.DAL
{
    public class Book 
    {
        private DateTime _date;

        [NotMapped]
        public static Database Db = new Database();

        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime Date 
        {
            get
            {
                return _date;
            }
            set
            {
                try
                {
                    OnDateChanged(new DateEventArgs(value, _date));
                    _date = value;
                }
                catch (BadDataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



        public Book()
        {
            DateChanged += HandleDateChanged;
        }

        public event EventHandler<DateEventArgs> DateChanged;

        public static IEnumerable<Book> Select(Func<Book, bool> predicate=null)
        {
            var result = (predicate == null) ? Db.Books.ToList() : Db.Books.Where(predicate).ToList();
            return result;
        }

        public void Save()
        {
            Db.Books.Add(this);
            Db.SaveChanges();
        }

        public void Remove()
        {
            Db.Books.Remove(this);
            Db.SaveChanges();
        }

        public void Update()
        {
            Db.SaveChanges();
        }

        protected virtual void OnDateChanged(DateEventArgs e)
        {
            EventHandler<DateEventArgs> handler = DateChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        private void HandleDateChanged(object sender, DateEventArgs e)
        {
            if (e.NewDate < new DateTime(2000, 1, 1))
            {
                throw new BadDataException(e.NewDate, e.OldDate, "Недопустимая дата, указываемая дата должна быть позднее 1го января 2000г");
            }
        }
    }

    public class DateEventArgs : EventArgs
    {
        public DateTime NewDate { get; set; }
        public DateTime OldDate { get; set; }

        public DateEventArgs(DateTime newDate, DateTime oldDate)
        {
            NewDate = newDate;
            OldDate = oldDate;
        }
    }
}