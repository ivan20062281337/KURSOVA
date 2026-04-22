using LIBRARY.MODEL;
using System.Windows;

namespace LIBRARY
{
    public partial class AddStudentWindow : Window
    {
        private LibraryRepository _repo = new LibraryRepository();
        private string _currentAdmin;

        public AddStudentWindow(string adminName)
        {
            InitializeComponent();
            _currentAdmin = adminName;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Заповніть всі поля!");
                return;
            }

            Random rnd = new Random();
            int ticketNumber = rnd.Next(100000, 999999);

            try
            {
                _repo.AddStudent(txtName.Text, txtEmail.Text, ticketNumber.ToString(), _currentAdmin);
                MessageBox.Show($"Студента успішно зареєстровано!\nНомер квитка: {ticketNumber}",
                                "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при реєстрації: " + ex.Message);
            }
        }
    }
}
