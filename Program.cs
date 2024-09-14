using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http.Headers;

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

    public List<string> getProducts() {
        return products;
    }

    public List<double> getRates() {
        return rates;
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
    static void printCommonProducts(Entity e1, Entity e2) {

        string e1Name = e1.getName();
        string e2Name = e2.getName();
        List<string> e1Products = e1.getProducts();
        List<string> e2Products = e2.getProducts();
        List<string> listToUse = null;
        List<string> otherList = null;

        if (e1Products.Count > e2Products.Count) {
            listToUse = e1Products;
            otherList = e2Products;
        } else {
            listToUse = e2Products;
            otherList = e1Products;
        }

        List<string> commonProductList = new List<string>();
        
        for (int i = 0; i < listToUse.Count; i++) {
            
            string itemToCheck = listToUse[i];

            if (otherList.Contains(itemToCheck)) {
                commonProductList.Add(itemToCheck);
            }

        }

        if (commonProductList.Count == 0) {
            Console.WriteLine("No common products found. Comparative advantage is not possible to determine.");
            return;
        } 

        if (commonProductList.Count == 1) {
            Console.WriteLine("Only 1 common product found. Comparative advantage is not possible to determine.");
            Console.WriteLine("Common Product: " + commonProductList[0]);
            return;
        }

        Console.WriteLine("*****LIST OF COMMON PRODUCTS*****");
        Console.WriteLine("for Entities " + e1Name + " and " + e2Name + "\n");

        for (int i = 0; i < commonProductList.Count; i++) {
            Console.WriteLine(commonProductList[i]);
        }

    }

    static List<Entity> determineComparativeAdvantage(Entity e1, Entity e2, string product1, string product2) {

        List<Entity> advantagedEntities = new List<Entity>();
        List<string> e1Products = e1.getProducts();
        List<double> e1Rates = e1.getRates();
        List<string> e2Products = e2.getProducts();
        List <double> e2Rates = e2.getRates();

        int index1Product1 = e1Products.IndexOf(product1);
        double rate1Product1 = e1Rates[index1Product1];
        int index2Product1 = e2Products.IndexOf(product1);
        double rate2Product1 = e2Rates[index2Product1];
        int index1Product2 = e1Products.IndexOf(product2);
        double rate1Product2 = e1Rates[index1Product2];
        int index2Product2 = e2Products.IndexOf(product2);
        double rate2Product2 = e2Rates[index2Product2];

        double oc1Product1 = rate1Product2 / rate1Product1;
        double oc1Product2 = 1 / oc1Product1;
        double oc2Product1 = rate2Product2/ rate2Product1;
        double oc2Product2 = 1 / oc2Product1;

        if (oc1Product1 == oc2Product1) {
            advantagedEntities.Add(null);
            advantagedEntities.Add(null);
            return advantagedEntities;
        }

        if (oc1Product1 > oc2Product1) {
            advantagedEntities.Add(e2);
        } else {
            advantagedEntities.Add(e1);
        }

        if (oc1Product2 > oc2Product2) {
            advantagedEntities.Add(e2);
        } else {
            advantagedEntities.Add(e1);
        }

        return advantagedEntities;

    }

    static void printComparativeAdvantage(Entity e1, Entity e2) {
       
        string e1Name = e1.getName();
        string e2Name = e2.getName();
        Console.WriteLine("Comparative advantage of production between entities " + e1Name + " and " + e2Name + "\n");
        printCommonProducts(e1, e2);
        Console.WriteLine("Choose two COMMON products to compare production for.");
        Console.WriteLine("Choose product 1: ");
        string product1 = Console.ReadLine();
        Console.WriteLine("Choose product 2: ");
        string product2 = Console.ReadLine();
        Console.WriteLine("Comparing products " + product1 + " and " + product2 + "\n");
        List<Entity> advantagedEntities = determineComparativeAdvantage(e1, e2, product1, product2);
        Entity p1Entity = advantagedEntities[0];
        Entity p2Entity = advantagedEntities[1];
        Console.WriteLine(p1Entity.getName() + " has the comparative advantage in making product " + product1);
        Console.WriteLine(p2Entity.getName() + " has the comparative advantage in making product " + product2);

    }

    //TODO: write the actual absolute advantage determinator
    static void printAbsoluteAdvantage(Entity e1, Entity e2) {
        //TODO: add system for incorporating multiple entities in the comparison

        string e1Name = e1.getName();
        string e2Name = e2.getName();
        Console.WriteLine("Absolute advantage of production between entities " + e1Name + " and " + e2Name + "\n");
        printCommonProducts(e1, e2);
        Console.WriteLine("Choose a COMMON product to compare absolute advantage in production for.");
        Console.WriteLine("Product Name: ");
        string productToCompare = Console.ReadLine();
        
    }

    static void printCommands() {

        Console.WriteLine("*****ACTIONS*****\n");
        //potential string formatting in future version?
        Console.WriteLine("add_e: creates a new entity that can trade with other entities");
        Console.WriteLine("mod_e: edits an existing entity (modifies production rate, adds new product, etc.)");
        Console.WriteLine("\tadd_p: adds a new product to the entity's production list");
        Console.WriteLine("\tmod_p: edits an existing product's information");
        Console.WriteLine("\tdel_p: removes a product from the entity's production list");
        Console.WriteLine("del_e: removes an entity from the list of entities");
        Console.WriteLine("open_s: creates a new simulation in which counntries can trade for advantage");
        Console.WriteLine("close_s: closes the current simulation");
        Console.WriteLine("comp_a: finds the comparative advantages of production for two existing entities");
        Console.WriteLine("abs_a: finds the absolute advantage(s) of production between two or more countries");
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
        string productName = null;

        while (productName == null) {

            productName = Console.ReadLine();

            if (productName != null) {
                break;
            } else {
                Console.WriteLine("No product given. Please type in a product name: ");
            }

        }

        Console.WriteLine("Rate of production (x items/hr): ");
        string productionRate = null;

        while (productionRate == null) {

            productionRate = Console.ReadLine();

            try {
                double productionRateDouble = Convert.ToDouble(productionRate);
                e.addToProducts(productName, productionRateDouble);
            } catch (Exception ex) {
                Console.WriteLine("Not a number. Please enter a number for the production rate: ");
                continue;
            }

            break;

        }

        Console.WriteLine("\nProduct added to " + e.getName() + "!\n");

    }

    static void modifyProduct(Entity e) {

        Console.WriteLine("Modifying Product of " + e.getName());
        e.printProducts();
        Console.WriteLine("\nPick a product to modify: ");
        List<string> productList = e.getProducts();
        string productToModify = null;

        while (productToModify == null) {

            productToModify = Console.ReadLine();
            
            if (!(productList.Contains(productToModify))) {
                Console.WriteLine("Not a recognized product. Please refer to the list above for a product to modify: ");
                continue;
            } 
            
            break;

        }
        
        double rateOfProduction = 0.0;
        Console.WriteLine("\nEnter the new rate of production:");

        while (rateOfProduction == 0) {
            
            string rateOfProductionString = Console.ReadLine();

            try {

                rateOfProduction = Convert.ToDouble(rateOfProductionString);

                if (rateOfProduction > 0) {
                    e.modifyProduct(productToModify, rateOfProduction);
                    break;
                } else {
                    Console.WriteLine("Rate of production must be greater than 0. Enter a new rate of production: ");
                    continue;
                }
                
            } catch (Exception ex) {
                Console.WriteLine("Rate of production must be a number. Enter a new rate of production: ");
            }

        }

        Console.WriteLine("\nProduct modified!\n");

    }

    static void deleteProduct(Entity e) {

        Console.WriteLine("Deleting Product of " + e.getName());
        e.printProducts();
        string productToDelete = null;
        List<string> listOfProducts = e.getProducts();
        Console.WriteLine("Pick a product to delete: ");

        while (productToDelete == null) {

            productToDelete = Console.ReadLine();

            if (!(listOfProducts.Contains(productToDelete))) {
                Console.WriteLine("Product not found in entity " + e.getName() + ". Please choose a product to delete: ");
                continue;
            } else {
                e.deleteProduct(productToDelete);
            }

        }

        Console.WriteLine("\nProduct deleted.");

    }
 
    static void addEntity() {

        Console.WriteLine("Adding Entity\n");
        Console.WriteLine("Entity Name: ");
        string entityName = Console.ReadLine();
        
        if (entityName == null) {
            while(entityName == null) {
                Console.WriteLine("No entity name provided. Entity Name: ");
                entityName = Console.ReadLine();
            }
        }

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
        string etmString = Console.ReadLine();

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
        printEntities();
        Console.WriteLine("Which entity do you want to delete?");
        string entityToDelete = Console.ReadLine();

        if (entityToDelete == null) {

            Console.WriteLine("Not an existing entity. Which entity do you want to delete?");
            entityToDelete = Console.ReadLine();

        }
        
        Console.WriteLine("Entity deleted.");

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