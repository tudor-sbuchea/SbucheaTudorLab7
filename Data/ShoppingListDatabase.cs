using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using SbucheaTudorLab7.Models;

namespace SbucheaTudorLab7.Data
{
    public class ShopListDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public ShopListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ShopList>().Wait();
            _database.CreateTableAsync<Product>().Wait();
            _database.CreateTableAsync<ListProduct>().Wait();
        }

        // Salveaza sau actualizeaza un produs
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

        // Sterge un produs
        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }

        // Obtine toate produsele
        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        // Salveaza sau actualizeaza o legatura intre o lista si un produs
        public Task<int> SaveListProductAsync(ListProduct listProduct)
        {
            if (listProduct.ID != 0)
            {
                return _database.UpdateAsync(listProduct);
            }
            else
            {
                return _database.InsertAsync(listProduct);
            }
        }

        // Obtine toate produsele dintr-o lista de cumparaturi
        public Task<List<Product>> GetListProductsAsync(int shopListId)
        {
            return _database.QueryAsync<Product>(
                "SELECT P.ID, P.Description FROM Product P " +
                "INNER JOIN ListProduct LP ON P.ID = LP.ProductID " +
                "WHERE LP.ShopListID = ?",
                shopListId);
        }

        // Salveaza sau actualizeaza o lista de cumparaturi
        public Task<int> SaveShopListAsync(ShopList shopList)
        {
            if (shopList.ID != 0)
            {
                return _database.UpdateAsync(shopList);
            }
            else
            {
                return _database.InsertAsync(shopList);
            }
        }

        // Sterge o lista de cumparaturi
        public Task<int> DeleteShopListAsync(ShopList shopList)
        {
            return _database.DeleteAsync(shopList);
        }

        // Obtine toate listele de cumparaturi
        public Task<List<ShopList>> GetShopListsAsync()
        {
            return _database.Table<ShopList>().ToListAsync();
        }

        // Sterge un produs dintr-o lista de cumparaturi
        public Task<int> DeleteListProductAsync(ListProduct listProduct)
        {
            return _database.DeleteAsync(listProduct);
        }
    }
}

