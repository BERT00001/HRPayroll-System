namespace HRPayroll_System.Models
{
    public class CAddModel
    {
        public Guid Id { get; set; }
        public String Fullname { get; set; }
        public DateTime Birthday { get; set; }
        public string TIN { get; set; }
        public string EmployeeType { get; set; }

    }
}
