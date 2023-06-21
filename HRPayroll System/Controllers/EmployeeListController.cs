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

        // DATABASE
        public EmployeeListController(HRPayrollDb HRDatabase)
        {
            hRDatabase = HRDatabase;
        }

        // * Routing : Contractual Employees * //

        //Contractual Employee List
        public async Task<IActionResult> List()
        {
            var Display = await hRDatabase.Employees.ToListAsync();
            return View(Display);
        }

        //Contratual ADD Employees : GET
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //Contratual ADD Employees : POST
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

        //Contractual Edit Employee : GET
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
            };
                //Return to the Editmodel
                return View(Employee);
            }
               //Else Return to Employee List Table Page
                return RedirectToAction("List"); 
        }

        //Contractual Edit Employee : POST
        [HttpPost]
        public async Task<IActionResult> Edit(EditModel CreateEmp)
        {
            var checking = await hRDatabase.Employees.FindAsync(CreateEmp.Id);

            if(checking != null) 
            {
                checking.Fullname = CreateEmp.Fullname;
                checking.Birthday = CreateEmp.Birthday;
                checking.TIN = CreateEmp.TIN;
                checking.EmployeeType = CreateEmp.EmployeeType;
                checking.OverallTotal = CreateEmp.OverallTotal;

                await hRDatabase.SaveChangesAsync();

                return RedirectToAction("List");
            }
            return View("Edit");
        }

        //Regular Calculator : POST
        [HttpGet]
        public async Task<IActionResult> Calculator(Guid Id)
        {

            var RegularCalcu =  hRDatabase.Employees.FirstOrDefault(i => i.Id == Id);
            
            if(RegularCalcu != null)
                
            {

                var Calcu = new CalculatorModel()
                {
                    Id = RegularCalcu.Id,
                    Fullname = RegularCalcu.Fullname,
                    Birthday = RegularCalcu.Birthday,
                    TIN = RegularCalcu.TIN,
                    EmployeeType = RegularCalcu.EmployeeType,
                    TaxRate = RegularCalcu.TaxRate,
                    OverallTotal = RegularCalcu.OverallTotal,
                    RAbsences = RegularCalcu.RAbsences,
                };
                return View(Calcu); 
            }
                return RedirectToAction("List");
        }

        //Regular Calculator : POST
        [HttpPost]
        public async Task<IActionResult> Calculator(CalculatorModel RegularEmployee)
        {
            var Load = await hRDatabase.Employees.FindAsync(RegularEmployee.Id);

            if (Load != null)
            {
                int Salary = 20000;
                float Days = 22;
                float Tax = .12f;

                //Divide the salary depending of the days per month
                float PerDay = Salary / Days;
                //Get Input of the user pass to the float datatype
                float Input = RegularEmployee.RAbsences;
                //Get the difference of the Standard date to absences
                float Difference = Days - Input;
                //Get the Standard salary and tax percentage with per daysalary and absences
                float Final = Salary - ((Salary * Tax) + ( PerDay * Input));

                //Convert the into 2 Decimal place coming from Total of Salary
                string ToDecimal = Final.ToString("0.00");
                //After that convert the STring value into float to post to database
                float Finalanswer = float.Parse(ToDecimal);

                Load.RAbsences = RegularEmployee.RAbsences;
                Load.OverallTotal = Finalanswer;

                await hRDatabase.SaveChangesAsync();

                return RedirectToAction("List");

            }
            return View();

        }



        // * Routing : Contractual Employees * //

        //Contratual ADD Employees : GET
        [HttpGet]
        public IActionResult CAdd()
        {
            return View();
        }

        //Contratual ADD Employees : POST
        [HttpPost]
        public async Task<IActionResult> CAdd(CAddModel CNewEmployee)
        {
            var Employee = new EmployeeModel()
            {

                Id = Guid.NewGuid(),
                Fullname = CNewEmployee.Fullname,
                Birthday = CNewEmployee.Birthday,
                TIN = CNewEmployee.TIN,
                EmployeeType = CNewEmployee.EmployeeType,

            };
            //Add Data To Database
            await hRDatabase.Employees.AddAsync(Employee);
            //Save Changes from the Database
            await hRDatabase.SaveChangesAsync();
            //Back to the Employee List Page
            return RedirectToAction("CEmployees");
        }

        //Contractual Employee List
        public async Task <IActionResult> CEmployees()
        {
            var CList = await hRDatabase.Employees.ToListAsync();
            return View(CList);
        }

        //Contractual Calculator : GET
        [HttpGet]
        public IActionResult CCalculator(Guid Id)
        {

            var ContractualCalcu = hRDatabase.Employees.FirstOrDefault(i => i.Id == Id);

            if (ContractualCalcu != null)

            {

                var CCalcu = new CCalculatorModel()
                {
                    Id = ContractualCalcu.Id,
                    Fullname = ContractualCalcu.Fullname,
                    Birthday = ContractualCalcu.Birthday,
                    TIN = ContractualCalcu.TIN,
                    EmployeeType = ContractualCalcu.EmployeeType,
                    Ndays = ContractualCalcu.Ndays,
                };
                return View(CCalcu);
            }
                return RedirectToAction("CEmployees");
            }

        //Contractual Calculator : POST
        [HttpPost]
        public async Task<IActionResult> CCalculator(CCalculatorModel ContracttEmployee)
        {
            var Load = await hRDatabase.Employees.FindAsync(ContracttEmployee.Id);

            if (Load != null)
            {
                int Num = 500;
                float Finalanswer = ContracttEmployee.Ndays * Num;

                //Convert the into 2 Decimal place coming from Total of Salary
                string ToDecimal = Finalanswer.ToString("0.00");
                //After that convert the STring value into float to post to database
                float Total = float.Parse(ToDecimal);

                Load.Ndays= ContracttEmployee.Ndays;
                Load.OverallTotal = Total;


                await hRDatabase.SaveChangesAsync();

                return RedirectToAction("CEmployees");

            }
            return View();

        }

        //Contractual Edit Employee : GET
        [HttpGet]
        public async Task<IActionResult> CEdit(Guid Id)


        {
            var CEditEmployee = await hRDatabase.Employees.FirstOrDefaultAsync(x => x.Id == Id);

            if (CEditEmployee != null)
            {
                var CEmployee = new CEditModel()
                {

                    Id = CEditEmployee.Id,
                    Fullname = CEditEmployee.Fullname,
                    Birthday = CEditEmployee.Birthday,
                    TIN = CEditEmployee.TIN,
                    EmployeeType = CEditEmployee.EmployeeType,
                    Ndays = CEditEmployee.Ndays,
                    OverallTotal = CEditEmployee.OverallTotal,

                };
                //Return to the Editmodel
                return View(CEmployee);
            }
            //Else Return to Employee List Table Page
            return RedirectToAction("CEmployees");
        }

        //Contractual Edit Employee : POST
        [HttpPost]
        public async Task<IActionResult> CEdit(CEditModel CCreateEmp)
        {
            var Cchecking = await hRDatabase.Employees.FindAsync(CCreateEmp.Id);

            if (Cchecking != null)
            {
                Cchecking.Fullname = CCreateEmp.Fullname;
                Cchecking.Birthday = CCreateEmp.Birthday;
                Cchecking.TIN = CCreateEmp.TIN;
                Cchecking.EmployeeType = CCreateEmp.EmployeeType;
                Cchecking.Ndays = CCreateEmp.Ndays;
                Cchecking.OverallTotal = CCreateEmp.OverallTotal;

                await hRDatabase.SaveChangesAsync();

                return RedirectToAction("CEmployees");
            }
            return View();
        }

        //DELETE : Regular Employees
        [HttpGet]
        public async Task<IActionResult> Deletes(Guid Id)
        {
            var checking = await hRDatabase.Employees.FirstOrDefaultAsync(x => x.Id == Id);
        
        if(checking != null)
        {
            hRDatabase.Employees.Remove(checking);
                await hRDatabase.SaveChangesAsync();
                return RedirectToAction("List");

        }
            return View("Deletes");
        }

        //DELETE : Contractual Employees
        [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var checking = await hRDatabase.Employees.FirstOrDefaultAsync(x => x.Id == Id);

            if (checking != null)
            {
                hRDatabase.Employees.Remove(checking);
                await hRDatabase.SaveChangesAsync();
                return RedirectToAction("CEmployees");

            }
            return View();
        }


    }

}
