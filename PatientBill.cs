namespace MediSureClinicBilling
{
    public class PatientBill
    {
        public string BillId { get; set; }
        public string PatientName { get; set; }
        public bool HasInsurance { get; set; }

        public decimal ConsultationFee { get; set; }
        public decimal LabCharges { get; set; }
        public decimal MedicineCharges { get; set; }

        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalPayable { get; set; }
    }
}


