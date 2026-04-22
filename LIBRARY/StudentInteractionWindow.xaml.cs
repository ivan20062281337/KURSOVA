using LIBRARY.MODEL;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;

namespace LIBRARY
{

    public partial class StudentInteractionWindow : Window
    {
        private int _studentId;
        private LibraryRepository _repo = new LibraryRepository();

        public StudentInteractionWindow(int studentId, string studentName)
        {
            InitializeComponent();
            _studentId = studentId;
            lblStudentName.Text = $"Студент: {studentName}";
            RefreshAll();
        }

        private void RefreshAll()
        {
            dgActiveLoans.ItemsSource = _repo.GetStudentActiveLoans(_studentId);
            dgAvailableBooks.ItemsSource = _repo.GetBooks();
        }

        private void Issue_Click(object sender, RoutedEventArgs e)
        {
            int bookId = (int)((Button)sender).Tag;

            DateTime expectedReturnDate = DateTime.Now.AddDays(14); //ВИДАЄМ НА 2 ТИЖНІ
            string currentAdmin = "operator";

            try
            {
                _repo.IssueBook(bookId, _studentId, expectedReturnDate, currentAdmin);
                RefreshAll();
                MessageBox.Show("Книгу успішно видано!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            int loanId = (int)((Button)sender).Tag;
            string currentAdmin = "operator";

            try
            {
                _repo.ReturnBook(loanId, currentAdmin);
                RefreshAll();
                MessageBox.Show("Книгу повернено та занесено в лог.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }
    }
}
