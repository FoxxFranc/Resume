using System;
using System.Collections;
using System.Collections.Generic;
/*
Написать программу администрирования супермаркетом.
В супермаркете есть очередь клиентов.
У каждого клиента в корзине есть товары, также у клиентов есть деньги.
Клиент, когда подходит на кассу, получает итоговую сумму покупки и старается её оплатить.
Если оплатить клиент не может, то он рандомный товар из корзины выкидывает до тех пор, пока его денег не хватит для оплаты.
 */


namespace Market
{
    class Program
    {
        static void Main(string[] args)
        {
            int customersCount;
            Random random = new Random();
            customersCount = random.Next(3, 8);
            for (int i = 0; i < customersCount; i++)
            {
                Customer customer = new Customer();
                customer.Crate();
                customer.ShowInfo();
                customer.PayForProducts();
                Console.Clear();
            }
            Console.WriteLine("Вы обслужили всех клиентов");
        }
    }

    class Product
    {
        public string Name { get; private set; }
        public int Cost { get; private set; }

        public Product(string name, int cost)
        {
            Name = name;
            Cost = cost;
        }

        public Product()
        {
            Name = "";
            Cost = 0;
        }
    }

    class ShoppingCart
    {
        private List<Product> _products;

        public Product Product { get; private set; }
        public int TotalCost { get; private set; }

        public ShoppingCart()
        {
            _products = new List<Product>();
            TotalCost = 0;
        }

        private void TakeAnyProduct()
        {
            Random random = new Random();
            List<Product> productsInSupermarket = new List<Product>();
            productsInSupermarket.Add(new Product("Картофель", 100));
            productsInSupermarket.Add(new Product("Томат", 75));
            productsInSupermarket.Add(new Product("Капуста", 150));
            productsInSupermarket.Add(new Product("Апельсин", 90));
            productsInSupermarket.Add(Product = new Product("Лимон", 80));
            Product = productsInSupermarket[random.Next(0, productsInSupermarket.Count)];
        }

        public void ShowInfo()
        {
            Console.WriteLine("Продукты в корзине:");
            foreach (var product in _products)
            {
                Console.WriteLine($"{product.Name} стоимостью {product.Cost}");
            }
            Console.WriteLine($"Стоимостью {TotalCost}");
        }

        public void Create()
        {
            Random random = new Random();
            int count = random.Next(3, 6);

            for (int i = 0; i < count; i++)
            {
                TakeAnyProduct();
                _products.Add(Product);
            }
            foreach (var product in _products)
            {
                TotalCost += product.Cost;
            }
        }

        public void RemoveAnyProduct()
        {
            Random random = new Random();
            int productId = random.Next(0, _products.Count - 1);
            Console.WriteLine($"Продукт {_products[productId].Name} стоимостью {_products[productId].Cost} выложен обратно на ветрину.");
            TotalCost -= _products[productId].Cost;
            _products.Remove(_products[productId]);
        }
    }

    class Customer
    {
        public int TotalMoney { get; private set; }

        private ShoppingCart _shoppingCart = new ShoppingCart();

        public Customer(ShoppingCart shoppingCart)
        {
            Random random = new Random();
            _shoppingCart = shoppingCart;
            TotalMoney = random.Next(150, 300);
        }

        public Customer()
        {
            Random random = new Random();
            _shoppingCart = new ShoppingCart();
            TotalMoney = random.Next(150, 300);
        }

        private void RemoveExcessFood()
        {
            while (_shoppingCart.TotalCost > TotalMoney)
            {
                Console.WriteLine($"Покупателю не хватает {_shoppingCart.TotalCost - TotalMoney}");
                Console.ReadKey();
                _shoppingCart.RemoveAnyProduct();
            }
        }

        public void Crate()
        {
            _shoppingCart.Create();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"У покупателя {TotalMoney} денег");
            _shoppingCart.ShowInfo();
        }

        public void PayForProducts()
        {
            RemoveExcessFood();
            Console.WriteLine($"Покупатель заплатил за продукты {_shoppingCart.TotalCost}");
            Console.ReadKey();
        }
    }
}
