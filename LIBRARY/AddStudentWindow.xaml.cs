using LIBRARY.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace LIBRARY
{
    /// <summary>
    /// Interaction logic for AddStudentWindow.xaml
    /// </summary>
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

            // 1. Генеруємо випадковий 6-значний номер квитка
            Random rnd = new Random();
            int ticketNumber = rnd.Next(100000, 999999);

            try
            {
                // 2. Викликаємо метод репозиторію (який все зашифрує сам всередині)
                _repo.AddStudent(txtName.Text, txtEmail.Text, ticketNumber.ToString(), _currentAdmin);

                // 3. Повідомляємо номер квитка
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
