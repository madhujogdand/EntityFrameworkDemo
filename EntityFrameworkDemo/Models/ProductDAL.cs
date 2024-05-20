using EntityFrameworkDemo.Data;

namespace EntityFrameworkDemo.Models
{
    public class ProductDAL
    {
        private ApplicationDbContext db;
        public ProductDAL(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Product> GetProducts()
        {
            // LINQ
            var model = (from product in db.Products
                         select product).ToList();

            return model;

            // Lambda experssion

            //return db.Products.ToList();
        }
        public Product GetProductById(int id)
        {
            //LINQ

            //var model = (from product in db.Products 
            //            where product.Id == id 
            //            select product).FirstOrDefault();

            //return model;

            //lambda

            var model = db.Products.Where(x => x.Id == id).SingleOrDefault();
            return model;
        }
        public int AddProduct(Product product)
        {
            int result = 0;
            // add object in DbSet
            db.Products.Add(product); // Add method will add emp object in DbSet
            //update the changes in DB
            result = db.SaveChanges(); // SaveChanges() reflect the changes from DbSet to DB
            return result;
        }
        public int EditProduct(Product product) // emp object has new data
        {
            int result = 0;
            var model = db.Products.Where(x => x.Id == product.Id).SingleOrDefault();
            if (model != null)
            {
                model.Name = product.Name; // model will hold old data
                model.Price = product.Price;
                result = db.SaveChanges();

            }
            return result;
        }
        public int DeleteProduct(int id)
        {
            int result = 0;
            var model = db.Products.Where(x => x.Id == id).SingleOrDefault();
            if (model != null)
            {
                // remove from dbSet
                db.Products.Remove(model);
                result = db.SaveChanges();
            }
            return result;
        }
    }
}
