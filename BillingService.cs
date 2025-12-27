using System;

namespace MediSureClinicBilling
{
    /// <summary>
    /// Simple billing service for creating, viewing and clearing the bill
    /// </summary>
    public class BillingService
    {
        public static PatientBill LastBill;
        public static bool HasLastBill = false;

        /// <summary>
        /// it creates a new bill by reading values from the console.
        /// Performs the validation and computes the final payable amount.
        /// </summary>
        public void CreateBill()
        {
            PatientBill bill = new PatientBill();

            Console.Write("Enter Bill Id: ");
            bill.BillId = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(bill.BillId))
            {
                Console.WriteLine("Bill Id cannot be empty.");
                return;
            }

            // enter patient name
            Console.Write("Enter Patient Name: ");
            bill.PatientName = Console.ReadLine();

            // Parse the insurance flag (Y/N)
            Console.Write("Is the patient insured? (Y/N): ");
            string insuranceInput = Console.ReadLine();
            bill.HasInsurance = insuranceInput.Equals("Y", StringComparison.OrdinalIgnoreCase);

            // Consultation fee must be positive
            Console.Write("Enter Consultation Fee: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal consultation) || consultation <= 0)
            {
                Console.WriteLine("Consultation fee must be greater than 0.");
                return;
            }
            bill.ConsultationFee = consultation;

            // Lab charges must be positive
            Console.Write("Enter Lab Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal lab) || lab < 0)
            {
                Console.WriteLine("Lab charges must be 0 or more.");
                return;
            }
            bill.LabCharges = lab;

            // Medicine charges must be greater than 0 
            Console.Write("Enter Medicine Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal medicine) || medicine < 0)
            {
                Console.WriteLine("Medicine charges must be 0 or more.");
                return;
            }
            bill.MedicineCharges = medicine;

            // Compute amounts 
            CalculateBill(bill);

            LastBill = bill;
            HasLastBill = true;

            Console.WriteLine("\nBill created successfully.");
            PrintSummary(bill);
        }

        /// <summary>
        /// Prints the last created bill to the console. If no bill exists a message
        /// prompts the user to create a new bill first.
        /// </summary>
        public void ViewBill()
        {
            if (!HasLastBill)
            {
                Console.WriteLine("No bill available. Please create a new bill first.");
                return;
            }

            PatientBill bill = LastBill;

            Console.WriteLine("\n----------- Last Bill -----------");
            Console.WriteLine($"BillId: {bill.BillId}");
            Console.WriteLine($"Patient: {bill.PatientName}");
            Console.WriteLine($"Insured: {(bill.HasInsurance ? "Yes" : "No")}");
            Console.WriteLine($"Consultation Fee: {bill.ConsultationFee:F2}");
            Console.WriteLine($"Lab Charges: {bill.LabCharges:F2}");
            Console.WriteLine($"Medicine Charges: {bill.MedicineCharges:F2}");
            Console.WriteLine($"Gross Amount: {bill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {bill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {bill.FinalPayable:F2}");
            Console.WriteLine("--------------------------------");
        }

        /// <summary>
        /// Clears the stored bill.
        /// </summary>
        public void ClearBill()
        {
            LastBill = null;
            HasLastBill = false;
            Console.WriteLine("Last bill cleared.");
        }

        /// <summary>
        /// Calculates gross amount, applies a fixed insurance discount (10%)
        /// when the patient is insured, and computes the final payable amount.
        /// </summary>
        private void CalculateBill(PatientBill bill)
        {
            bill.GrossAmount =
                bill.ConsultationFee +
                bill.LabCharges +
                bill.MedicineCharges;

            // If insured, apply 10% discount to the gross amount
            if (bill.HasInsurance)
            {
                bill.DiscountAmount = bill.GrossAmount * 0.10m;
            }
            else
            {
                bill.DiscountAmount = 0;
            }

            bill.FinalPayable = bill.GrossAmount - bill.DiscountAmount;
        }

        private void PrintSummary(PatientBill bill)
        {
            Console.WriteLine($"Gross Amount: {bill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {bill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {bill.FinalPayable:F2}");
            Console.WriteLine("------------------------------------------------------------");
        }
    }
}
