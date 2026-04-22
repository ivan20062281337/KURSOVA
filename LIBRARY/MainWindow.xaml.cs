using LIBRARY.MODEL;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static LIBRARY.MODEL.DbItems;

namespace LIBRARY
{
    public partial class MainWindow : Window
    {
        LibraryRepository _repo = new LibraryRepository();
        public MainWindow()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            try
            {
                dgBooks.ItemsSource = _repo.GetBooks();
                dgStudents.ItemsSource = _repo.GetAllStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження даних: " + ex.Message);
            }
        }

        private void ManageStudent_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var student = (Student)button.DataContext;

            int studentId = student.student_id;
            string full_name = student.full_name;

            StudentInteractionWindow issueWin = new StudentInteractionWindow(studentId, full_name);
            issueWin.ShowDialog();
            RefreshData();
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            BookEditWindow addWindow = new BookEditWindow();

            addWindow.Owner = this;

            if (addWindow.ShowDialog() == true)
            {
                RefreshData();
                MessageBox.Show("Нову книгу успішно додано до каталогу!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddAuthor_Click(object sender, RoutedEventArgs e)
        {
            AddAuthorWindow authorWin = new AddAuthorWindow();
            authorWin.Owner = this;
            authorWin.ShowDialog();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryWindow categoryWin = new AddCategoryWindow();
            categoryWin.Owner = this;
            categoryWin.ShowDialog();
        }

        private void OpenInteraction_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var student = btn.DataContext as Student;

            if (student != null)
            {
                var interactionWin = new StudentInteractionWindow(student.student_id, student.full_name);

                interactionWin.Owner = this;

                if (interactionWin.ShowDialog() == true)
                {
                    RefreshData();
                }
            }
        }



        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            var addWin = new AddStudentWindow("operator_ivan"); // Передай ім'я поточного адміна
            if (addWin.ShowDialog() == true)
            {
                RefreshData();
            }
        }



        private void Refresh_Click(object sender, RoutedEventArgs e) => RefreshData();

    }
}