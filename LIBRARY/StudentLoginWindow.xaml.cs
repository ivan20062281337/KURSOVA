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

namespace LIBRARY
{
    /// <summary>
    /// Interaction logic for StudentLoginWindow.xaml
    /// </summary>
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
