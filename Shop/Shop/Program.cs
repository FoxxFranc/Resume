using System;
using System.Collections;
using System.Collections.Generic;

/*
Существует продавец, он имеет у себя список товаров, и при нужде, может вам его показать, 
также продавец может продать вам товар. После продажи товар переходит к вам, и вы можете также посмотреть свои вещи.
 */

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            VendorInventory allProducts = new VendorInventory();
            UserInventory userInventory = new UserInventory();
            bool isWorking = true;
            while (isWorking)
            {
                Console.WriteLine("Введите коману:\n" +
                    "1 - показать все товары\n" +
                    "2 - купить товар\n" +
                    "3 - открыть инвентарь\n" +
                    "esc - выход");
                switch (Console.ReadLine())
                {
                    case "1":
                        allProducts.ShowAllProducts();
                        break;
                    case "2":
                        Console.WriteLine("Введите название продукта для покупки");
                        string name = Console.ReadLine();
                        userInventory.BuyProduct(allProducts.SellProduct(name));
                        break;
                    case "3":
                        userInventory.ShowAllProducts();
                        break;
                    case "esc":
                        isWorking = false;
                        break;
                    default:
                        Console.WriteLine("Введена неверная команда");
                        break;
                }
                Console.WriteLine("Нажмите любую клавишу");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class VendorInventory
    {
        private List<Product> _allProducts = new List<Product>();
        public void ShowAllProducts()
        {
            foreach (var product in _allProducts)
            {
                Console.WriteLine($"{product.Name} стоймость - {product.Cost}");
            }
        }

        public void RemoveProduct(Product product)
        {
            if (product != null)
            {
                _allProducts.Remove(product);
            }
            else
            {
                Console.WriteLine("Такого товара нет у торговца");
            }
        }

        public Product SellProduct(string name)
        {
            Product soldProduct = null;
            bool fondProduct = false;
            foreach (var product in _allProducts)
            {
                if (product.Name == name)
                {
                    soldProduct = product;
                    fondProduct = true;
                }
            }
            RemoveProduct(soldProduct);
            if (fondProduct)
            {
                return soldProduct;
            }
            else
            {
                return null;
            }
        }
    }

    class UserInventory
    {
        private List<Product> _allPtoducts = new List<Product>();

        public void ShowAllProducts()
        {
            foreach (var product in _allPtoducts)
            {
                Console.WriteLine($"{product.Name} по цене: {product.Cost}");
            }
        }

        public void BuyProduct(Product product)
        {
            _allPtoducts.Add(product);
        }
    }

    class Product
    {
        public string Name { get; private set; }
        public int Cost { get; private set; }

        public Product(string name, int cost)
        {
            Random randCost = new Random();
            Name = name;
            Cost = cost;
        }
    }
}
