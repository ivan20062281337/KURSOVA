using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.MODEL
{
    public class DbItems
    {
        public class Book
        {
            public int book_id { get; set; }
            public string title { get; set; }
            public string isbn { get; set; }
            public string category_name { get; set; } // category join category_name
            public string author_name { get; set; }   // authors join full_name
            public int quantity { get; set; }



            public int author_id { get; set; }
            public int category_id { get; set; }
        }

        public class Student
        {
            public int student_id { get; set; }
            public string full_name { get; set; }
            public string ticket_number { get; set; }
            public string email { get; set; }
        }


        public class AuthorShort
        {
            public int author_id { get; set; }
            public string name { get; set; }
        }

        public class CategoryShort
        {
            public int category_id { get; set; }
            public string name { get; set; }
        }

        public class Loan
        {
            public int loan_id { get; set; }
            public int book_id { get; set; }
            public int student_id { get; set; }

            public string book_title { get; set; }

            public DateTime loan_date { get; set; }
            public DateTime return_date { get; set; }

            public bool is_returned { get; set; }
        }
    }
}
