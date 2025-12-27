// entry point for the program

// uncomment the below code to run the QuickMartTraders Problem

using System;

namespace QuickMartTraders
{
    class Program
    {
        static void Main()
        {
            TransactionService service = new TransactionService();
            bool running = true;

            // Simple loop: display menu, read choice, invoke service
            while (running)
            {
                Console.WriteLine("\n================== QuickMart Traders ==================");
                Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Create and save a new transaction
                        service.CreateTransaction();
                        break;

                    case "2":
                        // Display the last saved transaction (if any)
                        service.ViewTransaction();
                        break;

                    case "3":
                        // Recompute and print profit/loss for the last transaction
                        service.Recalculate();
                        break;

                    case "4":
                        // Exit the interactive loop and end the program
                        Console.WriteLine("Thank you. Application closed.");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please choose between 1 and 4.");
                        break;
                }
            }
        }
    }
}







// Uncomment the below given code to run the MediSure Clinic Problem


// using System;

// namespace MediSureClinicBilling
// {
//     class Program
//     {
//         static void Main()
//         {
//             BillingService service = new BillingService();
//             bool running = true;

//             // Simple loop: display menu, read choice, invoke service
//             while (running)
//             {
//                 Console.WriteLine("\n================== MediSure Clinic Billing ==================");
//                 Console.WriteLine("1. Create New Bill (Enter Patient Details)");
//                 Console.WriteLine("2. View Last Bill");
//                 Console.WriteLine("3. Clear Last Bill");
//                 Console.WriteLine("4. Exit");
//                 Console.Write("Enter your option: ");

//                 string option = Console.ReadLine();

//                 switch (option)
//                 {
//                     case "1":
//                         // Create and save a new bill
//                         service.CreateBill();
//                         break;

//                     case "2":
//                         // Display the last saved bill (if any)
//                         service.ViewBill();
//                         break;

//                     case "3":
//                         // Clear the stored last bill
//                         service.ClearBill();
//                         break;

//                     case "4":
//                         // Exit the interactive loop and end the program
//                         Console.WriteLine("Thank you. Application closed.");
//                         running = false;
//                         break;

//                     default:
//                         // Handle invalid selections
//                         Console.WriteLine("Invalid option. Please select between 1 and 4.");
//                         break;
//                 }
//             }
//         }
//     }
// }
