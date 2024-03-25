using System;
using System.Collections.Generic;
using System.Linq;

public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Product(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}

public class Inventory
{
    private List<Product> products = new List<Product>();

    public void AddProduct(Product product)
    {
        var existingProduct = products.FirstOrDefault(p => p.Name == product.Name);
        if (existingProduct != null)
        {
            existingProduct.Quantity += product.Quantity;
        }
        else
        {
            products.Add(product);
        }
    }

    public bool RemoveProduct(string productName, int quantity)
    {
        var product = products.FirstOrDefault(p => p.Name == productName);
        if (product != null && product.Quantity >= quantity)
        {
            product.Quantity -= quantity;
            if (product.Quantity == 0)
            {
                products.Remove(product);
            }
            return true;
        }
        return false;
    }

    public decimal GetTotalValue()
    {
        return products.Sum(p => p.Price * p.Quantity);
    }

    public void PrintInventory()
    {
        foreach (var product in products)
        {
            Console.WriteLine($"Product: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
        }
    }
}

public class InventoryManager
{
    private Inventory inventory = new Inventory();

    public void Execute()
    {
        // Добавление товаров на склад
        inventory.AddProduct(new Product("Laptop", 1000m, 5));
        inventory.AddProduct(new Product("Smartphone", 500m, 10));

        // Вывод информации о товарах на складе
        Console.WriteLine("Current inventory:");
        inventory.PrintInventory();

        // Удаление товара со склада
        inventory.RemoveProduct("Smartphone", 2);

        Console.WriteLine("\nInventory after removal:");
        inventory.PrintInventory();

        // Подсчет и вывод общей стоимости товаров на складе
        Console.WriteLine($"\nTotal inventory value: {inventory.GetTotalValue()}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var manager = new InventoryManager();
        manager.Execute();
    }
}
