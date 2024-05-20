using EntityFrameworkDemo.Data;

namespace EntityFrameworkDemo.Models
{
    public class StudentDAL
    {
        private ApplicationDbContext db;
        public StudentDAL(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Student> GetStudents()
        {
            // LINQ
            var model = (from stud in db.Students
                         select stud).ToList();

            return model;

            // Lambda experssion

            //return db.Students.ToList();
        }
        public Student GetStudentById(int id)
        {
            //LINQ

            //var model = (from stud in db.Students 
            //            where stud.Id == id 
            //            select stud).FirstOrDefault();

            //return model;

            //lambda

            var model = db.Students.Where(x => x.RollNo == id).SingleOrDefault();
            return model;
        }
        public int AddStudent(Student stud)
        {
            int result = 0;
            // add object in DbSet
            db.Students.Add(stud); // Add method will add emp object in DbSet
            //update the changes in DB
            result = db.SaveChanges(); // SaveChanges() reflect the changes from DbSet to DB
            return result;
        }
        public int EditStudent(Student stud) // emp object has new data
        {
            int result = 0;
            var model = db.Students.Where(x => x.RollNo == stud.RollNo).SingleOrDefault();
            if (model != null)
            {
                model.Name = stud.Name; // model will hold old data
                model.Course = stud.Course;
                model.Fees = stud.Fees;
                result = db.SaveChanges();

            }
            return result;
        }
        public int DeleteStudent(int id)
        {
            int result = 0;
            var model = db.Students.Where(x => x.RollNo == id).SingleOrDefault();
            if (model != null)
            {
                // remove from dbSet
                db.Students.Remove(model);
                result = db.SaveChanges();
            }
            return result;
        }
    }
}
