namespace HRPayroll_System.Models
{
    public class CCalculatorModel
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public DateTime Birthday { get; set; }
        public string TIN { get; set; }
        public string EmployeeType { get; set; }
        public float Ndays { get; set; }
        public float OverallTotal { get; set; }
    }
}
