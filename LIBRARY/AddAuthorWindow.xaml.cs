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
    /// Interaction logic for AddAuthorWindow.xaml
    /// </summary>
    public partial class AddAuthorWindow : Window
    {
        private LibraryRepository _repo = new LibraryRepository();

        public AddAuthorWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Мінімальна валідація
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Будь ласка, введіть ім'я автора!");
                return;
            }

            try
            {
                // Викликаємо метод, який ми раніше обговорювали
                _repo.AddAuthor(txtFullName.Text, txtCountry.Text);

                MessageBox.Show("Автора успішно додано!");
                this.DialogResult = true; // Це закриє вікно і поверне сигнал "успіх"
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не вдалося додати автора: " + ex.Message);
            }
        }
    }
}
