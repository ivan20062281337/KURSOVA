using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using static LIBRARY.MODEL.DbItems;

namespace LIBRARY.MODEL
{
    internal class LibraryRepository
    {
        private string _connStr = DbConnector.GetConnectionString();

        //СТВОРЮЄМ СТУДЕНТА
        public int RegisterStudent(string name, string email)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                string sql = "INSERT INTO students (name, email) VALUES (@name, @email); SELECT LAST_INSERT_ID();";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // ДІСТАЄМ КНИГИ
        public List<Book> GetBooks()
        {
            var books = new List<Book>();
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                string sql = @"SELECT b.book_id, b.title, b.isbn, b.quantity, 
                               a.full_name as author_name, a.author_id, 
                               c.category_name as category_name, c.category_id
                        FROM books b
                        JOIN authors a ON b.author_id = a.author_id
                        JOIN categories c ON b.category_id = c.category_id";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            book_id = reader.GetInt32("book_id"),
                            title = reader.GetString("title"),
                            isbn = reader.GetString("isbn"),
                            quantity = reader.GetInt32("quantity"),
                            author_name = reader.GetString("author_name"),
                            author_id = reader.GetInt32("author_id"),
                            category_name = reader.GetString("category_name"),
                            category_id = reader.GetInt32("category_id")
                        });
                    }
                }
            }
            return books;
        }

        // ВИДАЧА КНИГИ
        public void IssueBook(int bookId, int studentId, DateTime returnDate, string adminName)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("issue_book", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_book_id", bookId);
                    cmd.Parameters.AddWithValue("@p_student_id", studentId);
                    cmd.Parameters.AddWithValue("@p_return_date", returnDate);
                    cmd.Parameters.AddWithValue("@p_admin_name", adminName);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ДІСТАЄМ СТУДЕНТІВ ВСІ
        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                string sql = "SELECT student_id, full_name FROM students";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string encryptedName = reader.GetString("full_name");
                            string decryptedName = DbEncrypter.Decrypt(encryptedName);

                            students.Add(new Student
                            {
                                student_id = reader.GetInt32("student_id"),
                                full_name = decryptedName,
                                email = "********", // ЗАГЛУШКА(EMAIL НАМ НЕ ПОТРІБЕН)
                                ticket_number = "********" // ЗАГЛУШКА(TICKET НАМ НЕ ПОТРІБЕН)
                            });
                        }
                    }
                }
            }
            return students;
        }


        // ОНОВЛЕННЯ КНИГИ
        public void UpdateBook(int id, string title, int authorId, int catId, string isbn, int qty)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                string sql = @"UPDATE books SET title=@t, author_id=@a, category_id=@c, 
                       isbn=@i, quantity=@q WHERE book_id=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@t", title);
                    cmd.Parameters.AddWithValue("@a", authorId);
                    cmd.Parameters.AddWithValue("@c", catId);
                    cmd.Parameters.AddWithValue("@i", isbn);
                    cmd.Parameters.AddWithValue("@q", qty);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // АВТОРИ ДЛЯ КБОКСІВ
        public List<AuthorShort> GetAuthorsShort()
        {
            var authors = new List<AuthorShort>();
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                string sql = "SELECT author_id, full_name FROM authors ORDER BY full_name";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        authors.Add(new AuthorShort
                        {
                            author_id = reader.GetInt32("author_id"),
                            name = reader.GetString("full_name")
                        });
                    }
                }
            }
            return authors;
        }

        // КАТЕГОРІЇ ДЛЯ КБОКСІВ
        public List<CategoryShort> GetCategoriesShort()
        {
            var categories = new List<CategoryShort>();
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                string sql = "SELECT category_id, category_name FROM categories ORDER BY category_name";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new CategoryShort
                        {
                            category_id = reader.GetInt32("category_id"),
                            name = reader.GetString("category_name")
                        });
                    }
                }
            }
            return categories;
        }

        //ДОДАТИ КНИГУ
        public void AddBook(string title, int authorId, int catId, string isbn, int qty, string adminName)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("add_new_book", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Додаємо параметри відповідно до твоєї процедури
                    cmd.Parameters.AddWithValue("@p_title", title);
                    cmd.Parameters.AddWithValue("@p_author_id", authorId);
                    cmd.Parameters.AddWithValue("@p_cat_id", catId);
                    cmd.Parameters.AddWithValue("@p_isbn", isbn);
                    cmd.Parameters.AddWithValue("@p_qty", qty);
                    cmd.Parameters.AddWithValue("@p_admin_name", adminName);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // АВТОР + КАТЕГОРІЯ

        public void AddAuthor(string name, string country)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                string sql = "INSERT INTO authors (full_name, country) VALUES (@full_name, @country)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@full_name", name);
                    cmd.Parameters.AddWithValue("@country", country);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddCategory(string name)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                string sql = "INSERT INTO categories (category_name) VALUES (@category_name)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@category_name", name);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<Loan> GetStudentActiveLoans(int studentId)
        {
            var loans = new List<Loan>();
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("get_active_loans_student", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@s_id", studentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loans.Add(new Loan
                            {
                                loan_id = reader.GetInt32("loan_id"),
                                book_title = reader.GetString("book_title"),
                                loan_date = reader.GetDateTime("loan_date"),
                                return_date = reader.GetDateTime("return_date")
                            });
                        }
                    }
                }
            }
            return loans;
        }

        public void ReturnBook(int loanId, string adminName)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("return_book", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_loan_id", loanId);
                    cmd.Parameters.AddWithValue("@p_admin_name", adminName);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Student VerifyStudent(string email, string rawTicket)
        {
            string encryptedTicket = DbEncrypter.Encrypt(rawTicket);
            string encryptedEmail = DbEncrypter.Encrypt(email);

            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("verify_student", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_email", encryptedEmail);
                    cmd.Parameters.AddWithValue("@p_ticket", encryptedTicket);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                student_id = reader.GetInt32("student_id"),
                                full_name = DbEncrypter.Decrypt(reader.GetString("full_name")),
                                email = reader.GetString("email")
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void AddStudent(string name, string email, string rawTicket, string adminName)
        {
            string encryptedTicket = DbEncrypter.Encrypt(rawTicket);
            string encryptedEmail = DbEncrypter.Encrypt(email);
            string encryptedName = DbEncrypter.Encrypt(name);

            using (var conn = new MySqlConnection(_connStr))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("add_new_student", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_name", encryptedName);
                    cmd.Parameters.AddWithValue("@p_email", encryptedEmail);
                    cmd.Parameters.AddWithValue("@p_ticket", encryptedTicket);
                    cmd.Parameters.AddWithValue("@p_admin_name", adminName);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
