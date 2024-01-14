using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stir_Denisa_ProiectApp.Models;
using CloudKit;
namespace Stir_Denisa_ProiectApp.Data

    public Task<int> SaveListProductAsync(ListProduct listp)
{
    if (listp.ID != 0)
    {
        return _database.UpdateAsync(listp);
    }
    else
    {
        return _database.InsertAsync(listp);
    }
}
public Task<List<Product>> GetListProductsAsync(int menuid)
{
    return _database.QueryAsync<Product>(
    "select P.ID, P.Description from Product P"
    + " inner join ListProduct LP"
    + " on P.ID = LP.ProductID where LP.MenuID = ?",
    menuid);
}
    public class ShoppingCart
    {
        readonly SQLiteAsyncConnection _database;
        public ShoppingCart(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Menu>().Wait();
            _database.CreateTableAsync<Product>().Wait();
            _database.CreateTableAsync<ListProduct>().Wait();
        }
        public Task<int> SaveProductAsync(Product product)
        {
            if (product.ID != 0)
            {
                return _database.UpdateAsync(product);
            }
            else
            {
                return _database.InsertAsync(product);
            }
        }
        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }
        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }
    }
}
