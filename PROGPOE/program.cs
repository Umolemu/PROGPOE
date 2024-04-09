using System;

namespace PROGPOE
{
    public class Program
    {
        private const int MaxIngredients = 10;
        private const int MaxSteps = 10;

        private static string[] ingredientsList = new string[MaxIngredients];
        private static string[] originalIngredientsList = new string[MaxIngredients];
        private static string[] scaledIngredientsList = new string[MaxIngredients];
        private static string[] stepsList = new string[MaxSteps];
       
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

            //Check if the integer is valid 
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
            Array.Clear(ingredientsList, 0, ingredientsList.Length);
            Array.Clear(originalIngredientsList, 0, originalIngredientsList.Length);
            Array.Clear(scaledIngredientsList, 0, scaledIngredientsList.Length);
            Array.Clear(stepsList, 0, stepsList.Length);

            if (ingredientsList[0] == null)
            {
                Console.WriteLine();
                Console.Write("Enter the number of ingredients: ");
                string input = Console.ReadLine();
                int numberOfIngredients;

                //Check if the integer is valid 
                while (!int.TryParse(input, out numberOfIngredients) || numberOfIngredients > MaxIngredients || numberOfIngredients < 1)
                {
                    Console.Write($"Enter a valid number between 1 and {MaxIngredients}: ");
                    input = Console.ReadLine();
                }

                InputIngredientDetails(numberOfIngredients);
            }
            else
            {
                //If list not empty prompt user to clear it
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

                ingredientsList[i] = ingredient;
                originalIngredientsList[i] = ingredient;
            }

            InputSteps();
        }

        static void InputSteps()
        {
            Console.Write("Please enter the number of steps needed to make the recipe: ");
            string steps = Console.ReadLine();
            int numberOfSteps;

            //Check if the integer is valid 
            while (!int.TryParse(steps, out numberOfSteps) || numberOfSteps > MaxSteps || numberOfSteps < 1)
            {
                Console.WriteLine($"Enter a valid number between 1 and {MaxSteps}: ");
                steps = Console.ReadLine();
            }

            for (int i = 0; i < numberOfSteps; i++)
            {
                Console.Write($"Enter the description for step {i + 1}: ");
                string stepDescription = Console.ReadLine();
                stepsList[i] = $"Step {i + 1}: {stepDescription}";
            }

            Console.WriteLine();
            Application();
        }

        static void ViewRecipe()
        {
            Console.WriteLine("\nIngredients List:");
            foreach (var ingredient in ingredientsList)
            {
                if (ingredient != null)
                    Console.WriteLine(ingredient);
            }

            Console.WriteLine("\nSteps:");
            foreach (var step in stepsList)
            {
                if (step != null)
                    Console.WriteLine(step);
            }
            Console.WriteLine();
            Application();
        }

        static void Clear()
        {
            Console.WriteLine();
            Array.Clear(ingredientsList, 0, ingredientsList.Length);
            Array.Clear(stepsList, 0, stepsList.Length);
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

            //Check if the integer is valid
            while (!int.TryParse(factorInput, out factor) || factor <= 0)
            {
                Console.Write("Invalid input. Please enter a positive integer: ");
                factorInput = Console.ReadLine();
            }

            Array.Clear(scaledIngredientsList, 0, scaledIngredientsList.Length);

            for (int i = 0; i < ingredientsList.Length; i++)
            {
                if (ingredientsList[i] != null)
                {
                    string[] parts = ingredientsList[i].Split('\n');
                    foreach (var part in parts)
                    {
                        if (part.Contains("quantity"))
                        {
                            string[] quantityParts = part.Split(':');
                            if (int.TryParse(quantityParts[1].Trim(), out int quantity))
                            {
                                quantity *= factor;
                                scaledIngredientsList[i] = $"{parts[0]}\nquantity: {quantity}\n{parts[2]}\n{parts[3]}\n";
                            }
                        }
                    }
                }
            }

            Array.Copy(scaledIngredientsList, ingredientsList, scaledIngredientsList.Length);
            Console.WriteLine();
            Console.WriteLine("Quantities scaled successfully.");
            Console.WriteLine();
            Application();
        }

        static void ResetQuantities()
        {
            Array.Clear(ingredientsList, 0, ingredientsList.Length);

            for (int i = 0; i < originalIngredientsList.Length; i++)
            {
                ingredientsList[i] = originalIngredientsList[i];
            }

            Console.WriteLine();
            Console.WriteLine("Quantities reset successfully.");
            Console.WriteLine();
            Application();
        }

        static void Main(string[] args)
        {
            Application();
        }
    }
}
