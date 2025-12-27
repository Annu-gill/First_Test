
namespace QuickMartTraders
{
    /// <summary>
    /// This Service responsible for creating, viewing and recalculating sale transactions.
    /// </summary>
    public class TransactionService
    {
        public static SaleTransaction LastTransaction;

        /// <summary>
        /// first the firsttransaction value is set as false.
        /// </summary>
        public static bool HasLastTransaction = false;
        public void CreateTransaction()
        {
            // Creating a new transaction object to collect the input
            SaleTransaction tx = new SaleTransaction();

            // Read and validate invoice number (Example: INV1001)
            Console.Write("Enter Invoice No: ");
            tx.InvoiceNo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(tx.InvoiceNo))
            {
                Console.WriteLine("Invoice No cannot be empty.");
                return;
            }

            // Customer and item name are given
            Console.Write("Enter Customer Name: ");
            tx.CustomerName = Console.ReadLine();

            Console.Write("Enter Item Name: ");
            tx.ItemName = Console.ReadLine();

            // Quantity must be a positive integer
            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Quantity must be greater than 0.");
                return;
            }
            tx.Quantity = qty;

            // Purchase amount must be positive
            Console.Write("Enter Purchase Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal purchase) || purchase <= 0)
            {
                Console.WriteLine("Purchase amount must be greater than 0.");
                return;
            }
            tx.PurchaseAmount = purchase;

            // Selling amount can be zero or more
            Console.Write("Enter Selling Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal selling) || selling < 0)
            {
                Console.WriteLine("Selling amount must be 0 or more.");
                return;
            }
            tx.SellingAmount = selling;

            // Computing profit/loss
            Calculate(tx);

            // Store the transaction as last transaction
            LastTransaction = tx;
            HasLastTransaction = true;

            Console.WriteLine("\nTransaction saved successfully.");
            PrintCalculation(tx);
        }

        /// <summary>
        /// Prints the details of the last saved transaction to the console.
        /// If no transaction exists, a message to create new transaction is displayed.
        /// </summary>
        public void ViewTransaction()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            SaleTransaction tx = LastTransaction;

            Console.WriteLine("\n-------------- Last Transaction --------------");
            Console.WriteLine($"InvoiceNo: {tx.InvoiceNo}");
            Console.WriteLine($"Customer: {tx.CustomerName}");
            Console.WriteLine($"Item: {tx.ItemName}");
            Console.WriteLine($"Quantity: {tx.Quantity}");
            Console.WriteLine($"Purchase Amount: {tx.PurchaseAmount:F2}");
            Console.WriteLine($"Selling Amount: {tx.SellingAmount:F2}");
            Console.WriteLine($"Status: {tx.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {tx.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {tx.ProfitMarginPercent:F2}");
            Console.WriteLine("--------------------------------------------");
        }

        /// <summary>
        /// Recalculates profit/loss for the last saved transaction and prints it
        /// </summary>
        public void Recalculate()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            Calculate(LastTransaction);
            PrintCalculation(LastTransaction);
        }

        /// <summary>
        /// Calculates profit or loss status, absolute amount and margin percent
        /// and stores the results
        /// -if sellingAmount is less... less
        /// -if sellingamount is more... profit
        /// -if equal... break-even condition
        /// </summary>
        private void Calculate(SaleTransaction tx)
        {
            // Determine status and absolute profit/loss amount
            if (tx.SellingAmount > tx.PurchaseAmount)
            {
                tx.ProfitOrLossStatus = "PROFIT";
                tx.ProfitOrLossAmount = tx.SellingAmount - tx.PurchaseAmount;
            }
            else if (tx.SellingAmount < tx.PurchaseAmount)
            {
                tx.ProfitOrLossStatus = "LOSS";
                tx.ProfitOrLossAmount = tx.PurchaseAmount - tx.SellingAmount;
            }
            else
            {
                tx.ProfitOrLossStatus = "BREAK-EVEN";
                tx.ProfitOrLossAmount = 0;
            }

            // Calculate margin percent, guarding against zero purchase amount
            tx.ProfitMarginPercent =
                (tx.PurchaseAmount > 0)
                ? (tx.ProfitOrLossAmount / tx.PurchaseAmount) * 100
                : 0;
        }

        /// <summary>
        /// Helper to print profit/loss calculation summary for a transaction.
        /// </summary>
        private void PrintCalculation(SaleTransaction tx)
        {
            Console.WriteLine($"Status: {tx.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {tx.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {tx.ProfitMarginPercent:F2}");
            Console.WriteLine("------------------------------------------------------");
        }
    }
}
