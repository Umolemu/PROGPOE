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

                ingredientsList.Add($"{nameOfIngredient} {quantityOfIngredient} {measurement} {description}");
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
                Console.WriteLine($"Enter the description for step {i + 1}: ");
                string stepDescription = Console.ReadLine();
                stepsList.Add(stepDescription);
            }
        }

        static void Main(string[] args)
        {
            Ingredients();

            // Example: Printing out the ingredients and their quantities
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
        }
    }
}
