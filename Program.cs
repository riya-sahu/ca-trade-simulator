using System;
using System.IO;
using System.Collections.Generic;

namespace Comparative_Advantage_Simulator;

public class Entity {

    string name;
    
    List<string> products;
    List<double> rates;

    public Entity() {

        name = "Unknown";
        products = new List<string>();
        rates = new List<double>();

    }

    public Entity(string n, List<string> p, List<double> r) {
       
        name = n;
        products = p;
        rates = r;

    }

    public string getName() {
        return name;
    }

    public void addToProducts(string product, double rate) {
      
        products.Add(product);
        rates.Add(rate);

    }

    public void modifyProduct(string product, double rate) {

        int index = products.IndexOf(product);
        rates[index] = rate;

    }

    public void deleteProduct(string product) {
        
        int index = products.IndexOf(product);
        products.RemoveAt(index);
        rates.RemoveAt(index);

    }

    public void printProducts() {
        
        Console.WriteLine("*****LIST OF PRODUCTS*****");
        Console.WriteLine("for Entity " + getName());

        for (int i = 0; i < products.Count; i++) {
            Console.WriteLine(products[i] + " " + rates[i]);
        }

    }

    public string lineEntry() {

        //TODO: fix this
        return name + " " + products + " " + rates;

    }

}

class Program
{

    static void printCommands() {

        Console.WriteLine("*****ACTIONS*****\n");
        //potential string formatting in future version?
        Console.WriteLine("add_e: creates a new entity that can trade with other entities");
        Console.WriteLine("mod_e: edits an existing entity (modifies production rate, adds new product, etc.)");
        Console.WriteLine("\tadd_p: adds a new product to the entity's production list");
        Console.WriteLine("\tmod_p: edits an existing product's information");
        Console.WriteLine("\tdel_p: removes a product from the entity's production list");
        Console.WriteLine("del_e: removes an entity from the list of entities");
        Console.WriteLine("\nMore sub-actions specific to main actions will be described after a main action is chosen.");
        Console.WriteLine("\n*****************");

    }

    static void printEntities() {

        //eventually, this will read from the file; for now, it's blank
        Console.WriteLine("No entities found.");

    }

    static void addProduct(Entity e) {

        Console.WriteLine("Adding Product to " + e.getName());
        Console.WriteLine("Product Name: ");
        string productName = Console.ReadLine();
        Console.WriteLine("Rate of production (x items/hr): ");
        string productionRate = Console.ReadLine();
        e.addToProducts(productName, Convert.ToDouble(productionRate));
        Console.WriteLine("\nProduct added to " + e.getName() + "!\n");

    }

    static void modifyProduct(Entity e) {

        Console.WriteLine("Modifying Product of " + e.getName());
        e.printProducts();
        Console.WriteLine("\nPick a product to modify: ");
        string productToModify = Console.ReadLine();
        Console.WriteLine("\nEnter the new rate of production:");
        string rateOfProduction = Console.ReadLine();
        e.modifyProduct(productToModify, Convert.ToDouble(rateOfProduction));
        Console.WriteLine("\nProduct modified!\n");

    }

    static void deleteProduct(Entity e) {

        Console.WriteLine("Deleting Product of " + e.getName());
        e.printProducts();
        Console.WriteLine("Pick a product to delete: ");
        string productToDelete = Console.ReadLine();
        e.deleteProduct(productToDelete);
        Console.WriteLine("\nProduct deleted.");

    }
 
    static void addEntity() {

        Console.WriteLine("Adding Entity\n");
        Console.WriteLine("Entity Name: ");
        string entityName = Console.ReadLine();
        List<string> products = new List<string>();
        List<double> rates = new List<double>();

        while (true) {

            Console.WriteLine("Would you like to add a product? (y/n)");

            if (Console.ReadLine() == "y") {

                Console.WriteLine("\nProduct Name: ");
                string productName = Console.ReadLine();
                products.Add(productName);
                //TODO: add an option for analyzing using the input OR output method
                Console.WriteLine("\nRate of production (x items/hr):");
                string productionRate = Console.ReadLine();
                //TODO: add error handling for incorrect input format
                rates.Add(Convert.ToDouble(productionRate));

            } else {

                Entity e = new Entity(entityName, products, rates); 
                Console.WriteLine("Entity created!");
                break;

            }

        }

    }

    static void modifyEntity() {

        Console.WriteLine("Modifying Entity\n");
        printEntities();
        Console.WriteLine("\nWhich entity do you want to modify?");
        //when all objects are read in, this will search through them for the one with the matching name
        //for now it defaults to a new object
        Entity entityToModify = new Entity(); //CHANGE THIS!
        bool chosen = false;
        
        while (!chosen) {

            Console.WriteLine("Choose a sub-action:");
            string action = Console.ReadLine();

            switch (action) {
                case "add_p":
                    addProduct(entityToModify);
                    chosen = true;
                    break;
                case "mod_p":
                    modifyProduct(entityToModify);
                    chosen = true;
                    break;
                case "del_p":
                    deleteProduct(entityToModify);
                    chosen = true;
                    break;
                default:
                    Console.WriteLine("\nNot a recognized action.\n");
                    break;
            }

        }
        
    }

    static void deleteEntity() {
        //the line will need to be directly removed, for now it's unused
        Console.WriteLine("Which entity do you want to delete?");
        entityToDelete = Console.ReadLine();
    }

    static void Main(string[] args)
    {

        //defining file
        string filePath = @"C:\Users\rsahu\Documents\Comparative Advantage Simulator\entities.txt";

        Console.WriteLine("Welcome to the Comparative Advantage Trade Simulator! Type 'quit' to quit. Type 'help' to receive a list of actions.");
        bool quit = false;

        while (!quit) {
            
            Console.WriteLine("\nChoose an action: ");
            string action = Console.ReadLine();
            Console.WriteLine();

            switch(action) {
                case "help":
                    printCommands();
                    break;
                case "quit":
                    quit = true;
                    Console.WriteLine("Ending session...");
                    break;
                case "add_e":
                    addEntity();
                    break;
                case "mod_e":
                    modifyEntity();
                    break;
                case "del_e":
                    deleteEntity();
                    break;
                default:
                    Console.WriteLine("Not a recognized action. Type 'help' to receive a list of actions.");
                    break;
            }

        }
    }
}