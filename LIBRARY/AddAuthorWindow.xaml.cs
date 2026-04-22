using LIBRARY.MODEL;
using System.Windows;

namespace LIBRARY
{
    public partial class AddAuthorWindow : Window
    {
        private LibraryRepository _repo = new LibraryRepository();

        public AddAuthorWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Будь ласка, введіть ім'я автора!");
                return;
            }

            try
            {
                _repo.AddAuthor(txtFullName.Text, txtCountry.Text);

                MessageBox.Show("Автора успішно додано!");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не вдалося додати автора: " + ex.Message);
            }
        }
    }
}
