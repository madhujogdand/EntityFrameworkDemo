using EntityFrameworkDemo.Data;
using static System.Reflection.Metadata.BlobBuilder;

namespace EntityFrameworkDemo.Models
{
    public class BookDAL
    {
        private ApplicationDbContext db;
        public BookDAL(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Book> GetBooks()
        {
            // LINQ
            var model = (from book in db.Books
                         select book).ToList();

            return model;

            // Lambda experssion

            //return db.Books.ToList();
        }
        public Book GetBookById(int id)
        {
            //LINQ

            //var model = (from book in db.Books 
            //            where book.Id == id 
            //            select book).FirstOrDefault();

            //return model;

            //lambda

            var model = db.Books.Where(x => x.Id == id).SingleOrDefault();
            return model;
        }
        public int AddBook(Book book)
        {
            int result = 0;
            // add object in DbSet
            db.Books.Add(book); // Add method will add emp object in DbSet
            //update the changes in DB
            result = db.SaveChanges(); // SaveChanges() reflect the changes from DbSet to DB
            return result;
        }
        public int EditBook(Book book) // emp object has new data
        {
            int result = 0;
            var model = db.Books.Where(x => x.Id == book.Id).SingleOrDefault();
            if (model != null)
            {
                model.Name = book.Name; // model will hold old data
                model.Author = book.Author;
                model.Price = book.Price;
                result = db.SaveChanges();

            }
            return result;
        }
        public int DeleteBook(int id)
        {
            int result = 0;
            var model = db.Books.Where(x => x.Id == id).SingleOrDefault();
            if (model != null)
            {
                // remove from dbSet
                db.Books.Remove(model);
                result = db.SaveChanges();
            }
            return result;
        }
    }
}
