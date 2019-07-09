using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetDataDrivenHomeworkNo2
{
    class Program
    {
        private static string _connectionString = @"Server=ASUS\SQLEXPRESS;Database=AdoNetDB;Trusted_Connection=True;";
    
    //1. Use the database from the prevous homework
    //2. Create simple app that will add/edit Authors and Books using Dapper
    //3. Use the stored procedure for add authors
    //4. Create method to get all authors with books using Query strongly typed one to one
        static void Main(string[] args)
        {
            Console.WriteLine("Insert number of the method to execute");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("1.Insert Author----------------------");
            Console.WriteLine("2.Edit Author------------------------");
            Console.WriteLine("3.Insert Book------------------------");
            Console.WriteLine("4.Edit Book--------------------------");
            Console.WriteLine("5.Add Author using Stored procedure--");
            Console.WriteLine("6.Get all authors with books---------");
            Console.WriteLine("-------------------------------------");

            var response = Console.ReadLine();

            switch (response)
            {
                case "1":
                    {
                        insertAuthor();
                break;
            };
                case "2":
                    {
                        editAuthor();
                break;
            }
                case "3":
                    {
                        insertBook();
                break;
            };
                case "4":
                    {
                        editBook();
                        break;
                    };
                case "5":
                    {
                        addAuthorStoredProcedure();
                        break;
                    };
                case "6":
                    {
                        getAllAuthorsAndBooks();
                        break;
                    };
                default:
                    {
                        Console.WriteLine("Invalid input");
                        break;
                    };
            }

        }

        private static void getAllAuthorsAndBooks()
        {
            string sqlCom = "SELECT * FROM Books AS b INNER JOIN dbo.Authors AS a ON A.ID = B.AuthorID";
            
            using (var connection = new SqlConnection(_connectionString))
            {
                var authBooks = connection.Query<Books,Authors,Books>(sqlCom,
                    (books,authors) =>
                    {
                        books.Authors = authors;
                        return books;
                    },
                    splitOn:"ID").Distinct().ToList();

                foreach (var item in authBooks)
                {
                    Console.WriteLine($"Author first name: {item.Authors.FirstName}, Author last name {item.Authors.FirstName}, Book title: {item.Title}, Book genre: {item.Genre}");
                }
            }
        }

        private static void addAuthorStoredProcedure()
        {
            Console.WriteLine("Insert Author first name:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Insert Author last name:");
            var lastName = Console.ReadLine();

            using (var connection = new SqlConnection(_connectionString))
            {
                var res = connection.Execute("AddAuthorStoredProc",
                    new { FirstName = firstName, LastName = lastName },
                    commandType: CommandType.StoredProcedure);
            }






            //            CREATE OR ALTER PROCEDURE AddAuthorsStoredProcedure
            //    @ID INT,
            //	@FirstName NVARCHAR(100),
            //	@LastName NVARCHAR(100)
            //AS
            //BEGIN

            //    SET NOCOUNT ON;

            //            update dbo.Authors
            //            set FirstName = FirstName,LastName = LastName
            //END
        }

        private static void editAuthor()
        {

            Console.WriteLine("Enter Author ID:");
            var ID = Console.ReadLine();
            Console.WriteLine("Enter new First Name for the Author");
            var FirstName = Console.ReadLine();
            Console.WriteLine("Enter new Last Name for the Author");
            var LastName = Console.ReadLine();

            string insertQuery = $"UPDATE Authors SET FirstName = '{FirstName}', LastName = '{LastName}' WHERE ID = '{ID}'";

            using (var connection = new SqlConnection(_connectionString))
            {
                var affectedRows = connection.Execute(insertQuery);
            }
        }

        private static void editBook()
        {
        Console.WriteLine("Enter Book ID:");
        var bookID = Console.ReadLine();
        Console.WriteLine("Enter new Title for the book");
        var Title = Console.ReadLine();
        Console.WriteLine("Enter new Genre for the book");
        var Genre = Console.ReadLine();


            string insertQuery = $"UPDATE Books SET Title = '{Title}', Genre = '{Genre}' WHERE ID = '{bookID}'";
            
            using (var connection = new SqlConnection(_connectionString))
            {
                var affectedRows = connection.Execute(insertQuery);
            }
        }

        private static void insertBook()
        {
            Console.WriteLine("Insert Book title:");
            var Title = Console.ReadLine();
            Console.WriteLine("Insert Book genre:");
            var Genre = Console.ReadLine();
            Console.WriteLine("Insert AuthorID:");
            var AuthorID = Console.ReadLine();
            const string insertQuery = @"INSERT INTO dbo.Books (Title, Genre, AuthorID) VALUES (@Title, @Genre, @AuthorID)";

            using (var connection = new SqlConnection(_connectionString))
            {
                var affectedRows = connection.Execute(insertQuery, new { Title = Title, Genre = Genre, AuthorID = AuthorID });
            }
            Console.WriteLine($"The new book {Title} {Genre} was inserted into the database");
            Console.ReadLine();
        }

        private static void insertAuthor()
        {
            Console.WriteLine("Insert Author first name:");
            var FirstName = Console.ReadLine();
            Console.WriteLine("Insert Author last name:");
            var LastName = Console.ReadLine();

            const string insertQuery = @"INSERT INTO dbo.Authors (FirstName, LastName) VALUES (@FirstName, @LastName)";

            using (var connection = new SqlConnection(_connectionString))
            {
                var affectedRows = connection.Execute(insertQuery, new { FirstName = FirstName, LastName = LastName });
            }
            Console.WriteLine($"The new author {FirstName} {LastName} was inserted into the database");
            Console.ReadLine();
        }
    }
}
