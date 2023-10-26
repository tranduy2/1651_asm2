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
                Console.WriteLine("Item removed from the library.");
                return true;
            }
            else
            {
                Console.WriteLine("Item not found in the library. Unable to remove.");
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

            // Check if the name contains any numbers.
            if (ContainsNumber(name))
            {
                Console.WriteLine("Borrower name cannot contain numbers.");
                return false; // Return false to indicate that the borrower was not added.
            }

            // If the name doesn't contain numbers and the ID is unique, add the borrower.
            Borrower borrower = new Borrower(name, id);
            Borrowers.Add(borrower);
            AddedBorrowers.Add(borrower); // Add the borrower to the list of added borrowers
            return true; // Return true to indicate a successful addition.
        }


        public bool RemoveBorrower(string borrowerName, string borrowerID)
        {
            Borrower borrowerToRemove = Borrowers.FirstOrDefault(b => b.Name.Equals(borrowerName, StringComparison.OrdinalIgnoreCase) && b.ID == borrowerID);
            if (borrowerToRemove != null)
            {
                Borrowers.Remove(borrowerToRemove);
                Console.WriteLine("Borrower removed from the library.");
                return true;
            }
            else
            {
                Console.WriteLine("Borrower not found in the library or the ID does not match. Unable to remove.");
                return false;
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

        public void DisplayBorrowerInfo(string borrowerName, string borrowerID)
        {
            Borrower borrower = Borrowers.FirstOrDefault(b => b.Name.Equals(borrowerName, StringComparison.OrdinalIgnoreCase));
            if (borrower.Name.Equals(borrowerName, StringComparison.OrdinalIgnoreCase) && borrower.ID == borrowerID)
            {
                Console.WriteLine($"Borrower Name: {borrower.Name}, ID: {borrower.ID}");
                Console.WriteLine("Borrowed Items:");
                foreach (var item in borrower.BorrowedItems)
                {
                    Console.WriteLine($"Title: {item.Title}, Author: {item.Author}");
                }
            }
            else
            {
                Console.WriteLine("Invalid borrower name or ID. Please check the borrower's name and ID.");
            }
        }

        public bool BorrowItem(string borrowerName, string itemTitle, string borrowerID)
        {
            Borrower borrower = Borrowers.FirstOrDefault(b => b.Name.Equals(borrowerName, StringComparison.OrdinalIgnoreCase) && b.ID == borrowerID);
            LibraryItem item = Items.FirstOrDefault(i => i.Title.Equals(itemTitle, StringComparison.OrdinalIgnoreCase));

            if (borrower != null && item != null)
            {
                if (!ContainsNumber(borrower.Name))
                {
                    if (item.IsAvailable)
                    {
                        item.IsAvailable = false;
                        borrower.BorrowedItems.Add(item);
                        Console.WriteLine("Item borrowed successfully.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Item already borrowed. Please enter a different item title.");
                    }
                }
                else
                {
                    Console.WriteLine("Borrower name cannot contain numbers.");
                }
            }
            else
            {
                Console.WriteLine("Invalid borrower name, ID, or item title. Please check the borrower's name and ID, and item availability.");
            }

            return false;
        }


        public void ReturnItem(string borrowerName, string itemTitle, string borrowerID)
        {
            Borrower borrower = Borrowers.FirstOrDefault(b => b.Name.Equals(borrowerName, StringComparison.OrdinalIgnoreCase) && b.ID == borrowerID);
            LibraryItem item = Items.FirstOrDefault(i => i.Title.Equals(itemTitle, StringComparison.OrdinalIgnoreCase));

            if (borrower != null && item != null)
            {
                if (!ContainsNumber(borrower.Name) && !item.IsAvailable)
                {
                    item.IsAvailable = true;
                    borrower.BorrowedItems.Remove(item);
                    Console.WriteLine("Item returned successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid borrower name, ID, or item title, or the item is not currently borrowed.");
                }
            }
            else
            {
                Console.WriteLine("Invalid borrower name, ID, or item title. Please check the borrower's name and ID, and item availability.");
            }
        }

        private bool ContainsNumber(string name)
        {
            foreach (char c in name)
            {
                if (char.IsDigit(c))
                {
                    return true; // Trả về true nếu tìm thấy số trong tên người mượn.
                }
            }
            return false; // Trả về false nếu không tìm thấy số trong tên người mượn.
        }
    }
}
