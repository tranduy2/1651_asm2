using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM
{
    public class Library
    {
        public List<LibraryItem> Items { get; } = new List<LibraryItem>();
        public List<Borrower> Borrowers { get; } = new List<Borrower>();
        public List<LibraryItem> AddedItems { get; } = new List<LibraryItem>();
        public List<Borrower> AddedBorrowers { get; } = new List<Borrower>();

        public void AddItem(LibraryItem item)
        {
            Items.Add(item);
        }


        public bool RemoveItem(string title)
        {
            LibraryItem itemToRemove = Items.FirstOrDefault(i => i.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisplayItems()
        {
            Console.WriteLine("Items in the Library:");
            foreach (var item in Items)
            {
                Console.WriteLine($"Title: {item.Title}, Author: {item.Author}, " +
                                  $"Availability: {(item is Book book && book.IsAvailable ? "Available" : "Not Available")}");
            }
        }

        public bool AddBorrower(string name, string id)
        {
            // Check if the ID is already in use.
            if (Borrowers.Any(b => b.ID == id))
            {
                return false; // Return false to indicate that the borrower was not added.
            }
            Borrower borrower = new Borrower(name, id);
            Borrowers.Add(borrower);
            AddedBorrowers.Add(borrower); // Add the borrower to the list of added borrowers
            return true; // Return true to indicate a successful addition.
        }

        public bool RemoveBorrower(string name)
        {
            Borrower borrowerToRemove = Borrowers.FirstOrDefault(b => b.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (borrowerToRemove != null)
            {
                Borrowers.Remove(borrowerToRemove);
                return true; // Returns true if the borrower is successfully deleted
            }
            else
            {
                return false; // Returns false if the borrower does not exist in the system
            }
        }

        public bool BorrowItem(string borrowerName, string itemTitle)
        {
            Book book = (Book)Items.FirstOrDefault(b => b.Title.Equals(itemTitle, StringComparison.OrdinalIgnoreCase) && b.IsAvailable);
            Borrower borrower = Borrowers.FirstOrDefault(b => b.Name.Equals(borrowerName, StringComparison.OrdinalIgnoreCase));

            if (book != null && borrower != null)
            {
                book.IsAvailable = false;
                borrower.BorrowedItems.Add(book);
                return true; // Return true to indicate a successful book borrowing.
            }
            else
            {
                Console.WriteLine("Unable to borrow the book. Please check the borrower's name and book availability.");
                return false; // Return false to indicate an unsuccessful book borrowing.
            }
        }

        public void ReturnItem(string borrowerName, string itemTitle)
        {
            Book book = (Book)Items.FirstOrDefault(b => b.Title.Equals(itemTitle, StringComparison.OrdinalIgnoreCase));
            Borrower borrower = Borrowers.FirstOrDefault(b => b.Name.Equals(borrowerName, StringComparison.OrdinalIgnoreCase));

            if (book != null && borrower != null && borrower.BorrowedItems.Contains(book))
            {
                book.IsAvailable = true;
                borrower.BorrowedItems.Remove(book);
            }
            else
            {
                Console.WriteLine("Unable to return the book. Please check the borrower's name and book availability.");
            }
        }
        public void DisplayBorrowers()
        {
            Console.WriteLine("Registered Borrowers:");
            foreach (var borrower in Borrowers)
            {
                Console.WriteLine($"Name: {borrower.Name}, ID: {borrower.ID}");
            }
        }

        public void DisplayBorrowerInfo(string borrowerName)
        {
            Borrower borrower = Borrowers.FirstOrDefault(b => b.Name.Equals(borrowerName, StringComparison.OrdinalIgnoreCase));
            if (borrower != null)
            {
                Console.WriteLine($"Borrower Name: {borrower.Name}, ID: {borrower.ID}");
                Console.WriteLine("Borrowed Books:");
                foreach (var book in borrower.BorrowedItems)
                {
                    Console.WriteLine($"Title: {book.Title}, Author: {book.Author}");
                }
            }
            else
            {
                Console.WriteLine("Borrower not found.");
            }
        }
    }
}
