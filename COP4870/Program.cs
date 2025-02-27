//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Xml.Serialization;
using COP4870.Models;
using COP4870.Services;
namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to the List");
            int input;
            int tempInt = 0;
            char choice;

            List<Product?> cart = CartServiceProxy.Current.Products;
            List<Product?> inventory = InventoryServiceProxy.Current.Products;

            do
            {
                Console.WriteLine("I. Inventory\nC. Shopping Cart\nE. Exit");
                choice = Console.ReadLine()?.ToUpper()[0] ?? 'X';

                switch (choice)
                {
                    case 'I':
                        Console.WriteLine("\n1. Create Item\n2. Read Inventory\n3. Update Item\n4. Delete Item\n5. Exit\n");
                        Console.WriteLine("Enter choice: ");
                        input = int.Parse(Console.ReadLine() ?? "0");
                        //Inventory
                        switch (input)
                        {
                            case 1:
                                Console.WriteLine("Enter Item to add:");
                                string? item = Console.ReadLine();
                                Console.WriteLine("Enter Quantity:");
                                int quantity = int.Parse(Console.ReadLine() ?? "0");
                                Console.WriteLine("Enter Price:");
                                double price = double.Parse(Console.ReadLine() ?? "0");
                                InventoryServiceProxy.Current.AddOrUpdate(new Product
                                {
                                    name = item,
                                    quantity = quantity,
                                    price = price
                                });
                                break;
                            case 2:
                                Console.WriteLine("Id. Item\tQuantity\tPrice");
                                foreach (var prod in inventory)
                                {
                                    Console.WriteLine(prod);
                                }
                                Console.WriteLine();
                                break;
                            case 3:
                                Console.WriteLine("Enter Id to update: ");
                                tempInt = int.Parse(Console.ReadLine() ?? "-1");
                                var selectedProd = inventory.FirstOrDefault(p => p.id == tempInt);
                                if (selectedProd != null)
                                {
                                    Console.WriteLine("Enter new Item: ");
                                    selectedProd.name = Console.ReadLine() ?? "ERROR";
                                    Console.WriteLine("Enter new Quantity: ");
                                    selectedProd.quantity = int.Parse(Console.ReadLine() ?? "0");
                                    Console.WriteLine("Enter new Price: ");
                                    selectedProd.price = double.Parse(Console.ReadLine() ?? "0");
                                    InventoryServiceProxy.Current.AddOrUpdate(selectedProd);
                                }
                                break;
                            case 4:
                                Console.WriteLine("Enter Id to Delete: ");
                                tempInt = int.Parse(Console.ReadLine() ?? "-1");
                                InventoryServiceProxy.Current.Delete(tempInt);
                                break;
                            case 5:
                                break;
                            default:
                                Console.WriteLine("Error: Unknown Command");
                                break;
                        }
                        break;
                    case 'C':
                        Console.WriteLine("\n1. Add Item\n2. Read Cart\n3. Update Item\n4. Remove Item\n5. Checkout\n6. Exit\n");
                        Console.WriteLine("Enter choice: ");
                        input = int.Parse(Console.ReadLine() ?? "0");
                        //shopping Cart
                        switch (input)
                        {
                            case 1:
                                Console.WriteLine("Enter Item to add:");
                                string? item = Console.ReadLine();
                                Console.WriteLine("Enter Quantity:");
                                int quantity = int.Parse(Console.ReadLine() ?? "0");
                                foreach (var prod in inventory)
                                {
                                    if (prod?.name == item)
                                    {
                                        /*add if inventory has stock*/
                                        if (prod?.quantity >= quantity)
                                        {
                                            CartServiceProxy.Current.AddOrUpdate(new Product
                                            {
                                                name = item,
                                                quantity = quantity,
                                                price = prod.price
                                            });
                                            prod.quantity -= quantity;
                                        }
                                        break;
                                    }
                                }
                                break;
                            case 2:
                                Console.WriteLine("Id. Item\tQuantity\tPrice");
                                foreach (var prod in cart)
                                {
                                    Console.WriteLine(prod);
                                }
                                break;
                            case 3:
                                Console.WriteLine("Enter Id to Update (int): ");
                                tempInt = int.Parse(Console.ReadLine() ?? "-1");
                                var selectedProd = cart.FirstOrDefault(p => p.id == tempInt);
                                if (selectedProd != null)
                                {
                                    tempInt = selectedProd.quantity;
                                    Console.WriteLine("Enter new Quantity: ");
                                    int temp = int.Parse(Console.ReadLine() ?? "0");
                                    foreach (var prod in inventory) {
                                        if (prod.name == selectedProd.name && prod.quantity>= temp) { 
                                            prod.quantity += (tempInt-temp);
                                            selectedProd.quantity = temp;
                                        }
                                    }
                                }
                                break;
                            case 4:
                                Console.WriteLine("Enter Id to Delete (int): ");
                                tempInt = int.Parse(Console.ReadLine() ?? "-1");
                                //add item and quantity back to inventory
                                foreach (var prod in inventory)
                                {
                                    foreach (var cartProd in cart)
                                    {
                                        if (cartProd?.name == prod.name)
                                        {
                                            prod.quantity += cartProd.quantity;
                                        }
                                    }
                                }
                                CartServiceProxy.Current.Delete(tempInt);
                                break;
                            case 5:
                                double total = 0;
                                Console.WriteLine("Id. Item\tQuantity\tPrice");
                                foreach (var prod in cart)
                                {
                                    Console.WriteLine(prod);
                                    total += prod.price * prod.quantity;
                                }
                                Console.WriteLine("\nTotal: $ "+Math.Round((total*1.07),2));
                                Console.WriteLine("\nThank you for shopping with us!");
                                Environment.Exit(0);
                                break;
                            case 6:
                                break;
                            default:
                                Console.WriteLine("Error: Unknown Command");
                                break;
                        }
                        break;
                    case 'E':
                        break;
                    default: break;
                }
            } while (choice != 'E');
        }
    }
}