using HRPayroll_System.Models;
using Microsoft.EntityFrameworkCore;

namespace HRPayroll_System.Data
{
    public class HRPayrollDb : DbContext
    {
        public HRPayrollDb(DbContextOptions options) : base(options)
        {


        }

        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
