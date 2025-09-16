using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace mssa_11._1
{
    public partial class Form1 : Form
    {
        private readonly BooksContext _dbContext;

        public Form1()
        {
            InitializeComponent();
            // Get DbContext from DI
            var serviceProvider = ((IServiceProvider)AppDomain.CurrentDomain.GetData("ServiceProvider"));
            _dbContext = serviceProvider?.GetService(typeof(BooksContext)) as BooksContext;
            LoadBooks();
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            var book = new Book
            {
                ISBN = txtISBN.Text.Trim(),
                Name = txtName.Text.Trim(),
                AuthorName = txtAuthorName.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };
            if (string.IsNullOrWhiteSpace(book.ISBN) || string.IsNullOrWhiteSpace(book.Name) || string.IsNullOrWhiteSpace(book.AuthorName))
            {
                MessageBox.Show("ISBN, Name, and Author Name are required.");
                return;
            }
            if (_dbContext.Books.Any(b => b.ISBN == book.ISBN))
            {
                MessageBox.Show("A book with this ISBN already exists.");
                return;
            }
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            LoadBooks();
            txtISBN.Clear();
            txtName.Clear();
            txtAuthorName.Clear();
            txtDescription.Clear();
        }

        private void LoadBooks()
        {
            dgvBooks.DataSource = _dbContext.Books.ToList();
        }
    }
}
