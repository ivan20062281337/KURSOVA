using LIBRARY.MODEL;using System.Windows;

namespace LIBRARY
{
    public partial class StudentLoginWindow : Window
    {
        public StudentLoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string ticket = txtTicket.Text;

            try
            {
                DbConnector.Initialize("reader", "reader");

                LibraryRepository repo = new LibraryRepository();
                var student = repo.VerifyStudent(email, ticket);

                if (student != null)
                {
                    StudentMainWindow studentWindow = new StudentMainWindow(student);
                    studentWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Невірний Email або номер квитка.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка підключення: " + ex.Message);
            }
        }
    }
}
