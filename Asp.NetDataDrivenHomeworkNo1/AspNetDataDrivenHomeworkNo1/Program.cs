using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetDataDrivenHomeworkNo1
{
    class Program
    {
        private static string _connectionString = @"Server=ASUS\SQLEXPRESS;Database=AdoNetDB;Trusted_Connection=True;";


        static void Main(string[] args)
        {
            //addAuthor();
            //AddBook();
            //GetNumberOffAuthors();
            //GetNumberOffBooks();
            //EditAuthor();
            EditBook();

            Console.ReadLine();
        }
        #region Edit Book
        private static void EditBook()
        {
            Console.WriteLine("Enter Book ID:");
            var bookID = Console.ReadLine();
            Console.WriteLine("Enter new Title for the book");
            var newTitle = Console.ReadLine();
            Console.WriteLine("Enter new Genre for the book");
            var newGenre = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = $"UPDATE Books SET Title='{newTitle}', Genre='{newGenre}' WHERE ID={bookID}";
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Book edited succesfully!");

        }
        #endregion Edit Book


        #region Add author
        private static void addAuthor()
        {
            Console.WriteLine("Author First Name: ");
            var FirstName = Console.ReadLine();
            Console.WriteLine("Author Last Name");
            var LastName = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format("Insert into dbo.Authors (FirstName, LastName) values ('{0}','{1}')", FirstName,LastName);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Author inserted");
        }
        #endregion Add author

        #region Add Book
        private static void AddBook()
        {
            Console.WriteLine("Insert Book Title:");
            var Title = Console.ReadLine();
            Console.WriteLine("Insert Genre");
            var Genre = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format("Insert into dbo.Books (Title, Genre) values ('{0}','{1}')",Title, Genre);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Book inserted!");
        }
        #endregion Add Book

        #region Check Author number
        private static void GetNumberOffAuthors()
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            Console.WriteLine(connection.State.ToString());

            //your code goes here
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "Select Count(*) From Authors";

            connection.Open();
            var count = (int)cmd.ExecuteScalar();

            Console.WriteLine("There are in total " + count + " authors in database");

            connection.Close();
            Console.WriteLine(connection.State.ToString());
        }
        #endregion Check Author number

        #region Check Book number
        private static void GetNumberOffBooks()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            Console.WriteLine(connection.State.ToString());

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "Select Count(*) From Books";

            connection.Open();
            var count = (int)cmd.ExecuteScalar();

            Console.WriteLine("There are in total " + count + " books in database");

            connection.Close();
            Console.WriteLine(connection.State.ToString());
        }
        #endregion Check Book number

        #region Edit Author
        private static void EditAuthor()
        {
            Console.WriteLine("Enter Author ID:");
            var authorID = Console.ReadLine();
            Console.WriteLine("Enter new First Name for the author");
            var newFirstName = Console.ReadLine();
            Console.WriteLine("Enter new Last Name for the author");
            var newLastName = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = $"UPDATE Authors SET FirstName='{newFirstName}', LastName='{newLastName}' WHERE ID={authorID}";
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Author edited succesfully!");
        }
        #endregion Edit Author
    }
}
