using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM
{
    public class LibraryItem
    {
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length <= 80)
                {
                    title = value;
                }
                else
                {
                    Console.WriteLine("Invalid title. Please submit a title of no more than 80 words.");
                }
            }
        }

        private string author;
        public string Author
        {
            get { return author; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && !value.Any(char.IsDigit) && value.Length <= 40)
                {
                    author = value;
                }
                else
                {
                    Console.WriteLine("Invalid author name. Please submit an author name with no numbers and a maximum of 40 characters.");
                }
            }
        }

        public bool IsAvailable { get; set; } = true;

        public LibraryItem(string title, string author)
        {
            Title = title;
            Author = author;
        }
    }

}
