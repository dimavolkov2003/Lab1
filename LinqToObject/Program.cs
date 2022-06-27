using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace LinqToObject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            List<Book> books = new List<Book>
            {
                new Book("Замок", "Франц", "Кафка", 350, new DateTime(2015,7,20,18,00,00), "K1"),
                new Book("Отелло", "Вільям", "Шекспір", 200, new DateTime(2016,6,24,2,30,00), "K1"),
                new Book("Король Лір", "Вільям", "Шекспір", 600, new DateTime(2016,1,2,22,15,00), "L1"),
                new Book("Гамлет", "Вільям", "Шекспір", 400, new DateTime(2016,11,3,9,30,00), "L1"),
                new Book("Сон", "Тарас", "Шевченко", 1000, new DateTime(2017,7,31,00,45,00), "H1")
            };

            List<Author> authors = new List<Author>
            {
                new Author("Франц","Кафка", 1883),
                new Author("Вільям","Шекспір", 1564),
                new Author("Тарас","Шевченко", 1814),
                new Author("Ліна","Костенко", 1930)
            };

            List<Edition> editions = new List<Edition>
            {
                new Edition("L1","Каменяр","Lviv"),
                new Edition("H1","Фоліо", "Kharkiv"),
                new Edition("K1","Основи", "Kyiv")
            };

            Console.WriteLine("РЕЗУЛЬТАТИ ЗАПИТІВ");
            Console.WriteLine("Отримати назву книг дешевші, ніж 650 грн");
            q1(books);
            Console.WriteLine("------------------");
            q2(books, authors);
            Console.WriteLine("------------------");
            q3(books);
            Console.WriteLine("------------------");
            q4(books);
            Console.WriteLine("------------------");
            q5(books, authors);
            Console.WriteLine("------------------");
            q6(books, editions);
            Console.WriteLine("------------------");
            q7(books);
            Console.WriteLine("------------------");
            q8(books);
            Console.WriteLine("------------------");
            q9(books);
            Console.WriteLine("------------------");
            q10(books, editions);
            Console.WriteLine("------------------");
            q11(books);
            Console.WriteLine("------------------");
            q12(books, "Сон");


        }
        //Отримати назву книг дешевші, ніж 650 грн
        static void q1(List<Book> books)
        {
            var cheapBooks = from book in books
                             where book.Price < 650
                             select book.Title;

            foreach(var cheapBook in cheapBooks)
            {
                Console.WriteLine(cheapBook);
            }
            Console.WriteLine();

            var cheapBooks2 = books.Where(b => b.Price < 650).Select(b => b.Title);

            foreach (var cheapBook in cheapBooks2)
            {
                Console.WriteLine(cheapBook);
            }
            Console.WriteLine();
        }
        //Знайти для кожної книги свого автора
        static void q2(List<Book> books, List<Author> authors)
        {
            var booksOfAuthors = from book in books
                                 join author in authors on book.SurnameOfAuthor equals author.Surname
                                 orderby author.Name, author.Surname
                                 select new { author.Name, author.Surname, book.Title };

            foreach (var booksOfAuthor in booksOfAuthors)
            {
                Console.WriteLine(booksOfAuthor);
            }
            Console.WriteLine();
        }
        //Знайти суму всіх цін i к-сть усіх книг, які менші за 500грн
        static void q3(List<Book> books)
        {
            int countBooks = books.Count(p => p.Price <= 500);
            decimal sumPrice = books.Where(p => p.Price <= 500).Sum(p => p.Price);

            Console.WriteLine("К-сть книг та їх сума: {0} - {1}", countBooks, sumPrice);
        }
        //Найдешевша книга та її назва
        static void q4(List<Book> books)
        {
            decimal cheapBook = books.Min(p => p.Price);
            var nameСheapBooks = from book in books
                                   where book.Price == cheapBook
                                   select book.Title;

            foreach (var nameСheapBook in nameСheapBooks)
            {
                Console.WriteLine("{0} - {1}", nameСheapBook, cheapBook);
            }
            Console.WriteLine();
        }
        //Якого автора книг немає в бібліотеці
        static void q5(List<Book> books, List<Author> authors)
        {
            var firstListName = from book in books
                                select book.NameOfAuthor;

            var secondListName = from author in authors
                                 select author.Name ;

            var names = secondListName.Except(firstListName);

            foreach(var name in names)
            {
                Console.WriteLine(name);
            }
            
        }
        //Брати книги до тих пір, коли не зустрінемо києвську редакцію
        static void q6(List<Book> books, List<Edition> editions)
        {
            var booksAndEditions = from book in books
                                   join edition in editions on book.EditionID equals edition.ID
                                   select new { Title = book.Title, Edition = edition.Location };

            var BooksBeforeCity = booksAndEditions.TakeWhile(l => l.Edition != "Kharkiv");

            foreach (var BookBeforeCity in BooksBeforeCity)
            {
                Console.WriteLine("{0} - {1}", BookBeforeCity.Title, BookBeforeCity.Edition);
            }
            Console.WriteLine();
        }
        // Книги одного виробництва
        static void q7(List<Book> books)
        {         
            var editionsGroup = from book in books
                             group book by book.EditionID;

            foreach (var editionGroup in editionsGroup)
            {
                Console.WriteLine(editionGroup.Key);

                foreach (var book in editionGroup)
                {
                    Console.WriteLine(book.Title);
                }
                Console.WriteLine(); 
            }
        }
        // К-сть книг одного виробництва
        static void q8(List<Book> books)
        {
            var editionsGroup = from book in books
                                group book by book.EditionID into g
                                select new { Edition = g.Key, Count = g.Count() };

            foreach (var editionGroup in editionsGroup)
            {
                Console.WriteLine("{0} - {1}", editionGroup.Edition, editionGroup.Count);
            }
            Console.WriteLine();
        }
        //q7 + q8
        static void q9(List<Book> books)
        {
            var editionsGroup = from book in books
                                group book by book.EditionID into g
                                select new { 
                                    Edition = g.Key,
                                    Count = g.Count(),
                                    Title = from b in g select b.Title
                                };


            foreach (var editionGroup in editionsGroup)
            {
                Console.WriteLine($"{editionGroup.Edition} : {editionGroup.Count}");
                foreach (var book in editionGroup.Title)
                {
                    Console.WriteLine(book);
                }
                Console.WriteLine(); // для разделения компаний
            }
        }
        //Згрупуємо усі книги по назвам видавництв
        static void q10(List<Book> books, List<Edition> editions)
        {
            var editionsGroup = editions.GroupJoin(books,
                edition => edition.ID,
                book => book.EditionID,
                (edition, book) => new
                {
                    Edition = edition.Name,
                    Books = book
                });

            foreach (var edition in editionsGroup)
            {
                Console.WriteLine(edition.Edition);
                foreach (var book in edition.Books)
                {
                    Console.WriteLine(book.Title);
                }
                Console.WriteLine();
            }

            var editionsGroup2 = from edition in editions
                                 join book in books on edition.ID equals book.EditionID into g
                                 select new
                                 {
                                     Name = edition.Name,
                                     Books = g
                                 };

            foreach (var edition in editionsGroup2)
            {
                Console.WriteLine(edition.Name);
                foreach (var book in edition.Books)
                {
                    Console.WriteLine(book.Title);
                }
                Console.WriteLine();
            }
        }
        //Перша книга, яка коштує більше 500грн
        static void q11(List<Book> books)
        {
            var firstElement = books.FirstOrDefault(b => b.Price >= 500);
            Console.WriteLine("Назва - {0} \nЦіна - {1}", firstElement.Title, firstElement.Price);
        }
        //Чи є така книга
        static void q12(List<Book> books, string name)
        {
            var firstElement = books.Any(b => b.Title == name);
            Console.WriteLine("{0} - {1} ", name, firstElement == false ? "No" : "Yes");
        }
    }
}
