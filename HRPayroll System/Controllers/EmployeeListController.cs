using HRPayroll_System.Data;
using HRPayroll_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace HRPayroll_System.Controllers
{
    public class EmployeeListController : Controller
    {
        private readonly HRPayrollDb hRDatabase;

        public EmployeeListController(HRPayrollDb HRDatabase)
        {
            hRDatabase = HRDatabase;
        }

        public async Task<IActionResult> List()
        {
            var Display = await hRDatabase.Employees.ToListAsync();
            return View(Display);
        }

        //Get Request from Add Employee
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //Post : Add to the database new Employee
        [HttpPost]
        public async Task <IActionResult> Add(AddModel NewEmployee)
        {
            var Employee = new EmployeeModel()
            {

                Id = Guid.NewGuid(),
                Fullname = NewEmployee.Fullname,
                Birthday = NewEmployee.Birthday,
                TIN = NewEmployee.TIN,
                EmployeeType = NewEmployee.EmployeeType,

            };
            //Add Data To Database
            await hRDatabase.Employees.AddAsync(Employee);
            //Save Changes from the Database
            await hRDatabase.SaveChangesAsync();
            //Back to the Employee List Page
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)


        {
            var EditEmployee = await hRDatabase.Employees.FirstOrDefaultAsync(x => x.Id == Id);

            if (EditEmployee != null)
            { 
            var Employee = new EditModel()
            {

                Id = EditEmployee.Id,
                Fullname = EditEmployee.Fullname,
                Birthday = EditEmployee.Birthday,
                TIN = EditEmployee.TIN,
                OverallTotal = EditEmployee.OverallTotal,
                EmployeeType = EditEmployee.EmployeeType,

                Absences = EditEmployee.Absences,

            };
                //Return to the Editmodel
                return View(Employee);
            }
               //Else Return to Employee List Table Page
                return RedirectToAction("List"); 
        }

        [HttpPost]

        public async Task<IActionResult> Edit(EditModel CreateEmp)
        {
            var checking = await hRDatabase.Employees.FindAsync(CreateEmp.Id);

            if(checking != null) 
            {
                checking.Fullname = CreateEmp.Fullname;
                checking.Birthday = CreateEmp.Birthday;
                checking.TIN = CreateEmp.TIN;
                checking.OverallTotal = CreateEmp.OverallTotal;

                await hRDatabase.SaveChangesAsync();

                return RedirectToAction("List");
            }
            return View("Edit");
        }

        //For Regular : Calculator 
        [HttpGet]
        public async Task<IActionResult> Calculator(Guid Id)
        {

            var RegularCalcu = hRDatabase.Employees.FirstOrDefault(i => i.Id == Id);
            
            if(RegularCalcu != null)
                
            {

                var Calcu = new RegularModel()
                {
                    Id = RegularCalcu.Id,
                    Fullname = RegularCalcu.Fullname,
                    Birthday = RegularCalcu.Birthday,
                    TIN = RegularCalcu.TIN,
                    EmployeeType = RegularCalcu.EmployeeType,
                    TaxRate = RegularCalcu.TaxRate,
                    Absences = RegularCalcu.Absences,
                };
                return View(Calcu); 
            }
                return RedirectToAction("List");
        }


        //List of Contractual Employee
        public async Task <IActionResult> CEmployees()
        {
            var CList = await hRDatabase.Employees.ToListAsync();
            return View(CList);
        }
    
        //Calculator for Emloyee
        public IActionResult CCalculator(Guid Id)
        {

            var ContractualCalcu = hRDatabase.Employees.FirstOrDefault(i => i.Id == Id);

            if (ContractualCalcu != null)

            {

                var CCalcu = new RegularModel()
                {
                    Id = ContractualCalcu.Id,
                    Fullname = ContractualCalcu.Fullname,
                    Birthday = ContractualCalcu.Birthday,
                    TIN = ContractualCalcu.TIN,
                    EmployeeType = ContractualCalcu.EmployeeType,
                    days = ContractualCalcu.days,
                };
                return View(CCalcu);
            }
                return RedirectToAction("CEmployees");
            }

        [HttpPost]

        public async Task<IActionResult> Calculator(CalculatorModel RegularEmployee, string click)
        {
            var check = await hRDatabase.Employees.FindAsync(RegularEmployee.Id);

            if (click == "submit")
            {
                int Salary = 20000;
                int days = 22;
                float tax = .12f;

                float perDay = Salary / days;
                float difference = days -  RegularEmployee.Absences;
                float multiply = perDay * difference;
                float Finalanswer = Salary - ((Salary * tax) + (perDay * RegularEmployee.Absences));

                check.Fullname = RegularEmployee.Fullname;
                check.TIN = RegularEmployee.TIN;
                check.OverallTotal = Finalanswer;
                check.EmployeeType = RegularEmployee.EmployeeType;

  
                await hRDatabase.SaveChangesAsync();
                //Back to the Employee List Page
                return RedirectToAction("List");
            }
                return View();

        }


        [HttpGet]
        public async Task<IActionResult> CEdit(Guid Id)


        {
            var CEditEmployee = await hRDatabase.Employees.FirstOrDefaultAsync(x => x.Id == Id);

            if (CEditEmployee != null)
            {
                var CEmployee = new EditModel()
                {

                    Id = CEditEmployee.Id,
                    Fullname = CEditEmployee.Fullname,
                    Birthday = CEditEmployee.Birthday,
                    TIN = CEditEmployee.TIN,
                    EmployeeType = CEditEmployee.EmployeeType,
                    Absences = CEditEmployee.Absences,

                };
                //Return to the Editmodel
                return View(CEmployee);
            }
            //Else Return to Employee List Table Page
            return RedirectToAction("CEmployees");
        }

        [HttpPost]

        public async Task<IActionResult> CEdit(EditModel CCreateEmp)
        {
            var Cchecking = await hRDatabase.Employees.FindAsync(CCreateEmp.Id);

            if (Cchecking != null)
            {
                Cchecking.Fullname = CCreateEmp.Fullname;
                Cchecking.Birthday = CCreateEmp.Birthday;
                Cchecking.TIN = CCreateEmp.TIN;

                await hRDatabase.SaveChangesAsync();

                return RedirectToAction("CEmployees");
            }
            return View();
        }


    }

}
