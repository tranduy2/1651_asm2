    using ASM;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace ASM
    {
        public class Borrower : LibraryItem
        {
            private string id;
            public string ID
            {
                get { return id; }
                set
                {
                    if (value.Length == 2 && int.TryParse(value, out _))
                    {
                        id = value;
                    }
                    else
                    {
                        Console.WriteLine("Invalid borrower ID. Please use two digits for the ID.");
                    }
                }
            }
            public List<LibraryItem> BorrowedItems { get; } = new List<LibraryItem>();

            public Borrower(string name, string id) : base(name, "N/A") // Provide a default author value
            {
                Name= name;
                ID = id;
            }
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

    }
}
