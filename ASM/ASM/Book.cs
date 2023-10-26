using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM
{
    public class Book : LibraryItem
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && !value.Any(char.IsDigit))
                {
                    name = value;
                }
                else
                {
                    Console.WriteLine("Invalid name. Please enter a valid name without numbers.");
                }
            }
        }

        public new bool IsAvailable { get; set; }

        public Book(string title, string author) : base(title, author)
        {
            IsAvailable = true;
        }
    }
}
