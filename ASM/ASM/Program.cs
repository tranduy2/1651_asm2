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
                Console.WriteLine("7. Borrow Book");
                Console.WriteLine("8. Return Book");
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
                        string borrowerIDToAdd;
                        bool isBorrowerAdded = false;

                        do
                        {
                            // Prompt the user to enter the two-digit ID.
                            Console.Write("Enter borrower ID: ");
                            borrowerIDToAdd = Console.ReadLine();

                            isBorrowerAdded = library.AddBorrower(borrowerNameToAdd, borrowerIDToAdd);

                            if (!isBorrowerAdded)
                            {
                                Console.WriteLine("Borrower with the same ID already exists. Please choose a unique ID.");
                            }
                        } while (!isBorrowerAdded);

                        Console.WriteLine("Borrower added to the library.");
                        break;

                    case 5:
                        Console.Write("Enter the name of the borrower to remove: ");
                        string borrowerToRemove = Console.ReadLine();
                        bool isBorrowerRemoved = library.RemoveBorrower(borrowerToRemove);
                        if (isBorrowerRemoved)
                        {
                            Console.WriteLine("Borrower removed from the library.");
                        }
                        else
                        {
                            Console.WriteLine("Borrower not found in the library. Unable to remove.");
                        }
                        break;


                    case 6:
                        library.DisplayBorrowers();
                        break;

                    case 7:
                        Console.Write("Enter borrower name: ");
                        string borrowerNameToBorrow;
                        bool isBorrowed = false;
                        do
                        {
                            borrowerNameToBorrow = Console.ReadLine();
                            if (!library.Borrowers.Any(b => b.Name.Equals(borrowerNameToBorrow, StringComparison.OrdinalIgnoreCase)))
                            {
                                Console.WriteLine("Borrower not found. Please enter a valid borrower name.");
                                Console.Write("Enter borrower name: ");
                            }
                            else
                            {
                                Console.Write("Enter book title to borrow: ");
                                string bookTitleToBorrow = Console.ReadLine();
                                isBorrowed = library.BorrowItem(borrowerNameToBorrow, bookTitleToBorrow);
                                if (!isBorrowed)
                                {
                                    Console.WriteLine("Book already borrowed. Please enter a different book title.");
                                    Console.Write("Enter book title to borrow: ");
                                }
                            }
                        } while (!isBorrowed);
                        Console.WriteLine("Book borrowed successfully.");
                        break;

                    case 8:
                        Console.Write("Enter borrower name: ");
                        string borrowerNameToReturn = Console.ReadLine();
                        Console.Write("Enter book title to return: ");
                        string bookTitleToReturn = Console.ReadLine();
                        library.ReturnItem(borrowerNameToReturn, bookTitleToReturn);
                        Console.WriteLine("Book returned successfully.");
                        break;


                    case 9:
                        Console.Write("Enter borrower name to view info: ");
                        string borrowerNameToDisplayInfo = Console.ReadLine();
                        library.DisplayBorrowerInfo(borrowerNameToDisplayInfo);
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
    }
}
