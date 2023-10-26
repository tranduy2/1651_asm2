using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASM
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Library library = new Library();
            library.AddItem(new Book("Doraemon", "Fujiko F. Fujio"));
            library.AddItem(new Book("Totto Chan", "Tetsuko Kuroyanagi"));
            library.AddItem(new Book("Harry Potter", "J.K. Rowling"));
            library.AddItem(new Book("The Witcher", "Andrzej Sapkowski"));
            library.AddItem(new Book("Doraemon: Nobita's Dinosaur", "Fujiko F. Fujio"));
            library.AddItem(new Book("Doraemon: Nobita to mugen sankenshi", "Fujiko F. Fujio"));

            while (true)
            {
                Console.WriteLine("=========Library Management=========");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Remove Book");
                Console.WriteLine("3. Display Books");
                Console.WriteLine("4. Add Borrower");
                Console.WriteLine("5. Remove Borrower");
                Console.WriteLine("6. Display Borrowers");
                Console.WriteLine("7. Borrow Item");
                Console.WriteLine("8. Return Item");
                Console.WriteLine("9. Display Borrower Info");
                Console.WriteLine("0. Exit");
                Console.WriteLine("====================================");
                Console.WriteLine("Enter Your Choice: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter item title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter item author: ");
                        string author = Console.ReadLine();
                        library.AddItem(new Book(title, author));
                        Console.WriteLine("Item added to the library.");
                        break;

                    case 2:
                        Console.Write("Enter the title of the book to remove: ");
                        string titleToRemove = Console.ReadLine();
                        bool isRemoved = library.RemoveItem(titleToRemove);

                        if (isRemoved)
                        {
                            Console.WriteLine("Book removed from the library.");
                        }
                        else
                        {
                            Console.WriteLine("Book not found in the library. Unable to remove.");
                        }
                        break;

                    case 3:
                        library.DisplayItems();
                        break;

                    case 4:
                        Console.Write("Enter borrower name: ");
                        string borrowerNameToAdd = Console.ReadLine();
                        Console.Write("Enter borrower ID: ");
                        string borrowerIDToAdd = Console.ReadLine();
                        bool isBorrowerAdded = library.AddBorrower(borrowerNameToAdd, borrowerIDToAdd);

                        if (!ContainsNumber(borrowerNameToAdd)) 
                        {
                            if (isBorrowerAdded)
                            {
                                Console.WriteLine("Borrower added to the library.");
                            }
                            else
                            {
                                Console.WriteLine("Borrower with the same ID already exists or invalid ID. Please choose a unique two-digit ID.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Borrower name cannot contain numbers.");
                        }
                        break;

                    case 5:
                        Console.Write("Enter borrower name to remove: ");
                        string borrowerNameToRemove = Console.ReadLine();
                        Console.Write("Enter borrower ID: ");
                        string borrowerIDToRemove = Console.ReadLine();
                        bool isBorrowerRemoved = library.RemoveBorrower(borrowerNameToRemove, borrowerIDToRemove);
                        if (isBorrowerRemoved)
                        {
                            Console.WriteLine("Borrower removed from the library.");
                        }
                        else
                        {
                            Console.WriteLine("Borrower not found in the library or the ID does not match. Unable to remove.");
                        }
                        break;

                    case 6:
                        library.DisplayBorrowers();
                        break;

                    case 7:
                        Console.Write("Enter borrower name: ");
                        string borrowerNameToBorrow = Console.ReadLine();
                        Console.Write("Enter borrower ID: ");
                        string borrowerIDToBorrow = Console.ReadLine();

                        if (!ContainsNumber(borrowerNameToBorrow))
                        {
                            Console.Write("Enter item title to borrow: ");
                            string itemTitleToBorrow = Console.ReadLine();
                            library.BorrowItem(borrowerNameToBorrow, itemTitleToBorrow, borrowerIDToBorrow);
                        }
                        else
                        {
                            Console.WriteLine("Borrower name cannot contain numbers.");
                        }
                        break;


                    case 8:
                        Console.Write("Enter borrower name: ");
                        string borrowerNameToReturn = Console.ReadLine();
                        Console.Write("Enter borrower ID: ");
                        string borrowerIDToReturn = Console.ReadLine();

                        if (!ContainsNumber(borrowerNameToReturn))
                        {
                            Console.Write("Enter item title to return: ");
                            string itemTitleToReturn = Console.ReadLine();
                            library.ReturnItem(borrowerNameToReturn, itemTitleToReturn, borrowerIDToReturn);
                        }
                        else
                        {
                            Console.WriteLine("Borrower name cannot contain numbers.");
                        }
                        break;


                    case 9:
                        Console.Write("Enter borrower name to view info: ");
                        string borrowerNameToDisplayInfo = Console.ReadLine();
                        Console.Write("Enter borrower ID: ");
                        string borrowerIDToDisplayInfo = Console.ReadLine();

                        if (!ContainsNumber(borrowerNameToDisplayInfo))
                        {
                            library.DisplayBorrowerInfo(borrowerNameToDisplayInfo, borrowerIDToDisplayInfo);
                        }
                        else
                        {
                            Console.WriteLine("Borrower name cannot contain numbers.");
                        }
                        break;

                    case 0:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }
        private static bool ContainsNumber(string borrowerNameToAdd)
        {
            throw new NotImplementedException();
        }
    }
}