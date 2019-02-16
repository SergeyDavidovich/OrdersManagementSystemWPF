using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using System.Data.Entity;
using DAL_LocalDb;

namespace Dashboard.ProductStatistics
{
    public class ProductsStatViewModel : ViewModelBase
    {
        readonly List<Category> categories;
        readonly List<Product> products;

        public ProductsStatViewModel(LocalDbContext context)
        {
            Title = "PRODUCTS";

            //get data for all queries
            context.Categories.Load();
            categories = context.Categories.ToList<Category>();

            context.Products.Load();
            products = context.Products.ToList<Product>();

            //query for ProductByCategoryGroups 
            var ProductsAndCountries =
                products.Join(categories,
                pro => pro.CategoryID,
                cat => cat.CategoryID,
                (pro, cat) => new { pro.ProductID, cat.CategoryName });

            //projection on ProductByCategoryObject
            this.ProductByCategoryGroups =
            new List<ProductByCategoryObject>(ProductsAndCountries.GroupBy(c => c.CategoryName).
            Select(g => new ProductByCategoryObject { CategoryName = g.Key, ProductCount = g.Count() }));
        }

        public class ProductByCategoryObject
        {
            public string CategoryName { get; set; }
            public int ProductCount { get; set; }
        }
        public List<ProductByCategoryObject> ProductByCategoryGroups { get; set; }
    }
}
