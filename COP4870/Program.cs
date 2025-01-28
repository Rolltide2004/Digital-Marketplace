//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using COP4870.Models;
using COP4870.Services;
namespace MyApp {
    internal class Program {
        static void Main(string[] args) {

            Console.WriteLine("Welcome to the List");
            int input;
            int tempInt = 0;

            List<Product?> cart = ProductServiceProxy.Current.Products;
            List<Product?> inventory = ProductServiceProxy.Current.Products;

            do{
                Console.WriteLine("\n1. Create Item\n2. Read List\n3. Update Item\n4. Delete Item\n5. Exit\n");

                Console.WriteLine("Enter choice: ");
                input = int.Parse(Console.ReadLine() ?? "0");

                switch (input) {
                    case 1:
                        Console.WriteLine("Enter Item to add:");
                        ProductServiceProxy.Current.AddOrUpdate(new Product{
                            item = Console.ReadLine()
                        });
                        break;
                    case 2:
                        foreach (var prod in cart){
                            Console.WriteLine(prod);
                        }
                        break;
                    case 3:
                        Console.WriteLine("Enter Id to update: ");
                        tempInt = int.Parse(Console.ReadLine() ?? "-1");

                        var selectedProd = cart.FirstOrDefault(p => p.id == tempInt);
                        if (selectedProd != null){
                            selectedProd.item = Console.ReadLine() ?? "ERROR";
                            ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                        }
                        break;
                    case 4:
                        Console.WriteLine("Enter Id to Delete: ");
                        tempInt = int.Parse(Console.ReadLine() ?? "-1");
                        ProductServiceProxy.Current.Delete(tempInt);
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;

                }
            }while (input != 5);
        }
        //void AddItem(List<string?> cart)
        //{
        //    var newProduct = Console.ReadLine() ?? "Error";

        //    cart.Add(newProduct);
        //}
    } 
}