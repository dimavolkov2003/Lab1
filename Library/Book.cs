using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book
    {
        public string Title { get; private set; }
        public string NameOfAuthor { get; private set; }
        public string SurnameOfAuthor { get; private set; }

        public decimal Price { get; private set; }
        public DateTime DateOfPublication { get; private set; }
        public string EditionID { get; private set; }

        public Book(string title, string nameOfAuthor, string surnameOfAuthor, decimal price,
            DateTime dateOfPublication, string editionID)
        {
            Title = title;
            NameOfAuthor = nameOfAuthor;
            SurnameOfAuthor = surnameOfAuthor;
            Price = price;
            DateOfPublication = dateOfPublication;
            EditionID = editionID;
        }
   
    }
}
