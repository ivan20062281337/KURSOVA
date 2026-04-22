using LIBRARY.MODEL;
using MySql.Data.MySqlClient;
using System.Windows;

namespace LIBRARY
{
    /// <summary>
    /// Interaction logic for StaffLoginWindow.xaml
    /// </summary>
    public partial class StaffLoginWindow : Window
    {
        public StaffLoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUser.Text;
            string pass = txtPass.Password;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Введіть логін та пароль!");
                return;
            }

            try
            {
                //ПІДКЛЮЧАЄМСЯ
                DbConnector.Initialize(user, pass);

                using (var conn = new MySqlConnection(DbConnector.GetConnectionString()))
                {
                    conn.Open();
                }

                MainWindow mainWin = new MainWindow();
                mainWin.Show();
                this.Close();
            }
            catch (MySqlException ex)
            {
                // 1045 - НЕПРАВИЛЬНІ ДАНІ
                if (ex.Number == 1045)
                    MessageBox.Show("Доступ заборонено: невірне ім'я користувача або пароль.");
                else
                    MessageBox.Show("Помилка бази даних: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
