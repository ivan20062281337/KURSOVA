using LIBRARY.MODEL;
using System.Windows;

namespace LIBRARY
{
    public partial class AddCategoryWindow : Window
    {
        private LibraryRepository _repo = new LibraryRepository();

        public AddCategoryWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Будь ласка, введіть назву категорії!");
                return;
            }
            try
            {
                _repo.AddCategory(txtFullName.Text);

                MessageBox.Show("Категорію успішно додано!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не вдалося додати категорію: " + ex.Message);
            }
        }
    }
}
