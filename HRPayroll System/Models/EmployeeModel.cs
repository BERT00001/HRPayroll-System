namespace HRPayroll_System.Models
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public DateTime Birthday { get; set; }
        public string TIN { get; set; }
        public string EmployeeType { get; set; }
        public float TaxRate { get; set; }
        public int Absences { get; set; }
        public float RAbsences { get; set; }
        public float Ndays { get; set; }
        public float StandardSalary { get; set; }
        public float OverallTotal { get; set; }
    }
}
