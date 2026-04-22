using LIBRARY.MODEL;
using MySql.Data.MySqlClient;
using System.Windows;
using static LIBRARY.MODEL.DbItems;

namespace LIBRARY
{
    /// <summary>
    /// Interaction logic for BookEditWindow.xaml
    /// </summary>
    public partial class BookEditWindow : Window
    {
        private Book _currentBook;
        private LibraryRepository _repo = new LibraryRepository();

        // ДОДАВАННЯ (book == null) АБО РЕДАГУВАННЯ
        public BookEditWindow(Book book = null)
        {
            InitializeComponent();
            _currentBook = book;
            LoadLists();

            if (_currentBook != null)
            {
                lblHeader.Text = "Редагування книги";
                btnSave.Content = "Оновити дані";
                FillFields();
            }
        }

        private void LoadLists()
        {
            cbAuthors.ItemsSource = _repo.GetAuthorsShort();
            cbCategories.ItemsSource = _repo.GetCategoriesShort();
        }

        private void FillFields()
        {
            txtTitle.Text = _currentBook.title;
            txtIsbn.Text = _currentBook.isbn;
            txtQty.Text = _currentBook.quantity.ToString();
            cbAuthors.SelectedValue = _currentBook.author_id;
            cbCategories.SelectedValue = _currentBook.category_id;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || cbAuthors.SelectedValue == null || cbCategories.SelectedValue == null)
            {
                MessageBox.Show("Будь ласка, заповніть усі обов'язкові поля");
                return;
            }

            try
            {
                int authorId = (int)cbAuthors.SelectedValue;
                int categoryId = (int)cbCategories.SelectedValue;
                int quantity = int.Parse(txtQty.Text);
                string isbn = txtIsbn.Text;
                string title = txtTitle.Text;
                string adminName = "operator";

                if (_currentBook == null)
                {
                    _repo.AddBook(title, authorId, categoryId, isbn, quantity, adminName);
                }
                else
                {
                    _repo.UpdateBook(_currentBook.book_id, title, authorId, categoryId, isbn, quantity);
                }

                this.DialogResult = true;
                this.Close();
            }
            catch (MySqlException ex)
            {
                //ЯКШО НЕМА ДОСТУПУ ВИВОДИМО ЦЕ НА ЕКРАН
                MessageBox.Show("Помилка доступу до бази даних: " + ex.Message, "Відмовлено в доступі", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }


        private void LoadComboBoxes()
        {
            var authors = _repo.GetAuthorsShort();
            cbAuthors.ItemsSource = authors;
            var categories = _repo.GetCategoriesShort();
            cbCategories.ItemsSource = categories;
        }
    }
}
