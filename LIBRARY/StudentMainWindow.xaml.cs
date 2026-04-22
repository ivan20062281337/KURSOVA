using LIBRARY.MODEL;
using System.Windows;
using static LIBRARY.MODEL.DbItems;

namespace LIBRARY
{
    public partial class StudentMainWindow : Window
    {
        private Student _currentStudent;
        private LibraryRepository _repo = new LibraryRepository();

        public StudentMainWindow(Student student)
        {
            InitializeComponent();
            _currentStudent = student;
            this.Title = $"Вітаємо, {_currentStudent.full_name}";

            LoadData();
        }

        private void LoadData()
        {
            dgMyLoans.ItemsSource = _repo.GetStudentActiveLoans(_currentStudent.student_id);
            dgCatalog.ItemsSource = _repo.GetBooks();
        }
    }
}
