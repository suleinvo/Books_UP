using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Books.DAL
{
    public static class DataManipulator
    {
        public static void DeleteData(DataGridView data)
        {
            var books = (List<Book>)data.DataSource;
            foreach (DataGridViewRow p in data.SelectedRows)
            {
                books[p.Index].Remove();
            }
            data.DataSource = Book.Db.Books.ToList();
        }

        public static void UpdateData(DataGridView data)
        {
            var books = (List<Book>)data.DataSource;
            foreach (DataGridViewRow p in data.SelectedRows)
            {
                books[p.Index].Update();
            }
        }
    }
}
