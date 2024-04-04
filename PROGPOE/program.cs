using System;
using System.Collections;

namespace PROGPOE
{
    public class Program
    {
        private static int numberOfIngredients;
        private static int quantityOfIngredient;
        private static ArrayList ingredientsList = new ArrayList();
        private static ArrayList stepsList = new ArrayList();

        static void Application()
        {
            Console.WriteLine("" +
                "1. Input recipie " +
                "\n2. View recipie " +
                "\n3. Scale quantities " +
                "\n4. Reset quantities " +
                "\n5. Clear all data " +
                "\n6. Exit");

            Console.Write("Enter your choice: ");
            string choise = Console.ReadLine();
            int choiseInt;

            while (!int.TryParse(choise, out choiseInt) || choiseInt > 6 || choiseInt < 1)
            {
                Console.Write("Enter a valid number between 1 and 6");
                choise = Console.ReadLine();
            }

            switch (choiseInt)
            {
                case 1:
                    Ingredients();
                    break;
                case 2:
                    ViewRecipie();
                    break;
                case 6: 
                    Exit(); 
                    break;
            }
        }

        static void Ingredients()
        {
            Console.Write("Enter the number of ingredients: ");
            string input = Console.ReadLine();

            while (!int.TryParse(input, out numberOfIngredients))
            {
                Console.Write("Invalid input. Please enter a valid number: ");
                input = Console.ReadLine();
            }

            InputIngredientDetails();
        }

        static void InputIngredientDetails()
        {
            for (int i = 0; i < numberOfIngredients; i++)
            {
                Console.Write($"Enter the name of ingredient {i + 1}: ");
                string nameOfIngredient = Console.ReadLine();

                Console.Write($"Enter the quantity of ingredient {i + 1}: ");
                string quantityInput = Console.ReadLine();

                while (!int.TryParse(quantityInput, out quantityOfIngredient))
                {
                    Console.Write($"Please enter a valid number for ingredient {i + 1}: ");
                }

                Console.Write($"Enter the unit measurement for ingredient {i + 1}: ");
                string measurement = Console.ReadLine();

                Console.Write($"Enter the description for ingredient {i + 1}: ");
                string description = Console.ReadLine();

                ingredientsList.Add($"Name: {nameOfIngredient} " +
                    $"quantity: {quantityOfIngredient} " +
                    $"measurment: {measurement} " +
                    $"description: {description}");
            }

            InputSteps();
        }

        static void InputSteps()
        {
            Console.Write("Please enter the number of steps needed to make the recipe: ");
            string steps = Console.ReadLine();
            int numberOfSteps;
            while (!int.TryParse(steps, out numberOfSteps))
            {
                Console.WriteLine("Enter a valid number of steps: ");
                steps = Console.ReadLine();
            }

            for (int i = 0; i < numberOfSteps; i++)
            {
                Console.Write($"Enter the description for step {i + 1}: ");
                
                string stepDescription = Console.ReadLine();
                string step = $"Step {i + 1}: ";
                step += stepDescription;

                stepsList.Add(step);
            }

            Application();
        }

        static void ViewRecipie()
        {
            Console.WriteLine("\nIngredients List:");
            foreach (var ingredient in ingredientsList)
            {
                Console.WriteLine(ingredient);
            }

            // Example: Printing out the steps
            Console.WriteLine("\nSteps:");
            foreach (var step in stepsList)
            {
                Console.WriteLine(step);
            }

            Application();
        }

        static void Exit()
        {
            Environment.Exit(0);
        } 
        static void Main(string[] args)
        {
            Application();     
                       
        }
    }
}
