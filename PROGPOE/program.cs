using System;
using System.Collections;

namespace PROGPOE
{
    public class Program
    {

        private static ArrayList ingredientsList = new ArrayList();
        private static ArrayList stepsList = new ArrayList();
        private static ArrayList originalIngredientsList = new ArrayList();
        private static ArrayList scaledIngredientsList = new ArrayList();

        //Main running method that controlls all of the applications functions
        static void Application()
        {
            Console.WriteLine("" +
                "1. Input recipe " +
                "\n2. View recipe " +
                "\n3. Scale quantities " +
                "\n4. Reset quantities " +
                "\n5. Clear all data " +
                "\n6. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            int choiceInt;
            //Check for a valid integer
            while (!int.TryParse(choice, out choiceInt) || choiceInt > 6 || choiceInt < 1)
            {
                Console.Write("Enter a valid number between 1 and 6: ");
                choice = Console.ReadLine();
            }

            switch (choiceInt)
            {
                case 1:
                    Ingredients();
                    break;
                case 2:
                    ViewRecipe();
                    break;
                case 3:
                    ScaleQuantities();
                    break;
                case 4:
                    ResetQuantities();
                    break;
                case 5:
                    Clear();
                    break;
                case 6:
                    Exit();
                    break;
            }
        }

        static void Ingredients()
        {
            //Clear the ingredients list to store new values
            originalIngredientsList.Clear(); 
            scaledIngredientsList.Clear();

            //Cheack if list has ingredients
            if (ingredientsList.Count == 0)
            {
                Console.WriteLine();
                Console.Write("Enter the number of ingredients: ");
                string input = Console.ReadLine();
                int numberOfIngredients;
                //Check for a valid integer
                while (!int.TryParse(input, out numberOfIngredients))
                {
                    Console.Write("Invalid input. Please enter a valid number: ");
                    input = Console.ReadLine();
                }

                ingredientsList.Clear(); // Clear the existing ingredients list before adding new ingredients
                InputIngredientDetails(numberOfIngredients);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("The ingredient is not empty, clear it to enter another recipe");
                Application();
            }
        }

        static void InputIngredientDetails(int numberOfIngredients)
        {
            for (int i = 0; i < numberOfIngredients; i++)
            {
                Console.Write($"Enter the name of ingredient {i + 1}: ");
                string nameOfIngredient = Console.ReadLine();

                Console.Write($"Enter the quantity of ingredient {i + 1}: ");
                string quantityInput = Console.ReadLine();
                int quantityOfIngredient;
                //Check for a valid integer
                while (!int.TryParse(quantityInput, out quantityOfIngredient))
                {
                    Console.Write($"Please enter a valid number for ingredient {i + 1}: ");
                    quantityInput = Console.ReadLine();
                }

                Console.Write($"Enter the unit measurement for ingredient {i + 1}: ");
                string measurement = Console.ReadLine();

                Console.Write($"Enter the description for ingredient {i + 1}: ");
                string description = Console.ReadLine();

                string ingredient = $"Name: {nameOfIngredient}\n" +
                                    $"quantity: {quantityOfIngredient}\n" +
                                    $"measurement: {measurement}\n" +
                                    $"description: {description}\n";

                ingredientsList.Add(ingredient);
                originalIngredientsList.Add(ingredient); // Store original ingredient
            }

            InputSteps();
        }

        static void InputSteps()
        {
            Console.Write("Please enter the number of steps needed to make the recipe: ");
            string steps = Console.ReadLine();
            int numberOfSteps;
            //Check for a valid integer
            while (!int.TryParse(steps, out numberOfSteps))
            {
                Console.WriteLine("Enter a valid number of steps: ");
                steps = Console.ReadLine();
            }
            //Enter a description for each step 
            for (int i = 0; i < numberOfSteps; i++)
            {
                Console.Write($"Enter the description for step {i + 1}: ");

                string stepDescription = Console.ReadLine();
                string step = $"Step {i + 1}: ";
                step += stepDescription;
                stepsList.Add(step);
            }
            Console.WriteLine();
            Application();
        }

        static void ViewRecipe()
        {
            Console.WriteLine("\nIngredients List:");
            foreach (var ingredient in ingredientsList)
            {
                Console.WriteLine(ingredient);
            }

            Console.WriteLine("\nSteps:");
            foreach (var step in stepsList)
            {
                Console.WriteLine(step);
            }
            Console.WriteLine();
            Application();
        }

        static void Clear()
        {
            Console.WriteLine();
            ingredientsList.Clear();
            stepsList.Clear();
            Application();
        }

        static void Exit()
        {
            Environment.Exit(0);
        }

        static void ScaleQuantities()
        {
            Console.WriteLine();
            Console.Write("Enter the scaling factor: ");
            string factorInput = Console.ReadLine();
            int factor;
            //Check for a valid integer
            while (!int.TryParse(factorInput, out factor) || factor <= 0)
            {
                Console.Write("Invalid input. Please enter a positive integer: ");
                factorInput = Console.ReadLine();
            }

            scaledIngredientsList.Clear(); // Clear scaled ingredients list

            // Store scaled quantities
            foreach (var originalIngredient in originalIngredientsList)
            {
                //This line will get all four fields 
                string[] parts = ((string)originalIngredient).Split('\n');
                foreach (var part in parts)
                {
                    //We then take those four fields and only take quantities
                    if (part.Contains("quantity"))
                    {
                        //We then remove the : from the list to get " quantity"
                        string[] quantityParts = part.Split(':');
                        //We then check if it can be placed as a valid int and remove the space 
                        if (int.TryParse(quantityParts[1].Trim(), out int quantity))
                        {
                            quantity *= factor;
                            scaledIngredientsList.Add($"Name: {parts[0]}\nquantity: {quantity}\n{parts[2]}\n{parts[3]}");
                        }
                    }
                }
            }

            // Update ingredientsList with scaled quantities
            ingredientsList.Clear();
            foreach (var scaledIngredient in scaledIngredientsList)
            {
                ingredientsList.Add(scaledIngredient);
            }
            Console.WriteLine();
            Console.WriteLine("Quantities scaled successfully.");
            Console.WriteLine();

            Application();
        }

        static void ResetQuantities()
        {
            // Clear ingredientsList before adding original quantities
            ingredientsList.Clear();

            // Add original quantities to ingredientsList
            foreach (var originalIngredient in originalIngredientsList)
            {
                ingredientsList.Add(originalIngredient);
            }
            Console.WriteLine();

            Console.WriteLine("Quantities reset successfully.");
            Application();

            Console.WriteLine();

        }

        static void Main(string[] args)
        {
            Application();
        }
    }
}
