using EntityFrameworkDemo.Data;

namespace EntityFrameworkDemo.Models
{
    public class EmployeeDAL
    {
        private ApplicationDbContext db;
        public EmployeeDAL(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Employee> GetEmployees()
        {
            // LINQ
            //var model=(from emp in db.Employees
            //          select emp).ToList();

            //return model;

            // Lambda experssion

            return db.Employees.ToList();
        }
        public Employee GetEmployeeById(int id)
        {
            //LINQ

            //var model = (from emp in db.Employees 
            //            where emp.Id == id 
            //            select emp).FirstOrDefault();

            //return model;

            //lambda

            var model = db.Employees.Where(x => x.Id == id).SingleOrDefault();
            return model;
        }
        public int AddEmployee(Employee emp)
        {
            int result = 0;
            // add object in DbSet
            db.Employees.Add(emp); // Add method will add emp object in DbSet
            //update the changes in DB
            result = db.SaveChanges(); // SaveChanges() reflect the changes from DbSet to DB
            return result;
        }
        public int EditEmployee(Employee emp) // emp object has new data
        {
            int result = 0;
            var model = db.Employees.Where(x => x.Id == emp.Id).SingleOrDefault();
            if (model != null)
            {
                model.Name = emp.Name; // model will hold old data
                model.City = emp.City;
                model.Salary = emp.Salary;
                result = db.SaveChanges();

            }
            return result;
        }
        public int DeleteEmployee(int id)
        {
            int result = 0;
            var model = db.Employees.Where(x => x.Id == id).SingleOrDefault();
            if (model != null)
            {
                // remove from dbSet
                db.Employees.Remove(model);
                result = db.SaveChanges();
            }
            return result;
        }

    }
}
