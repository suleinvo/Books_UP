using Books.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Books.UI
{
    public partial class MainForm : Form
    {
        BindingSource source = new BindingSource();
 
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = Book.db.Books.ToList();
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            MessageBox.Show("Error happened " + anError.Context.ToString());
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                var book = new Book()
                {
                    Author = authorText.Text,
                    Date = DateTime.Parse(dateText.Text),
                    Name = nameText.Text,
                    Publisher = publisherText.Text
                };
                book.Save();
                this.dataGridView1.DataSource = Book.db.Books.ToList();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }         
        }

        private void DeleteSelected_Click(object sender, EventArgs e)
        {
            var books = (List<Book>)this.dataGridView1.DataSource;
            foreach (DataGridViewRow p in this.dataGridView1.SelectedRows)
            {
                books[p.Index].Remove();
            }
            this.dataGridView1.DataSource = Book.db.Books.ToList();
        }

        private void UpdateSelected_Click(object sender, EventArgs e)
        {
            var books = (List<Book>)this.dataGridView1.DataSource;
            foreach (DataGridViewRow p in this.dataGridView1.SelectedRows)
            {
                books[p.Index].Update();
            }
        }
    }
}
