using System;

namespace PROGPOE
{
    public class InputItems
    {
        public static void Ingredients(List<string> ingredientsList, List<string> originalIngredientsList, List<string> scaledIngredientsList, List<string> stepsList)
        {
           
                     
            if (ingredientsList.Count == 0)
            {
                Application.SetChangedQuantity(false);

                //Array.Clear(originalIngredientsList, 0, originalIngredientsList.Length);

                //new
                originalIngredientsList.Clear();

                //Array.Clear(scaledIngredientsList, 0, scaledIngredientsList.Length);

                //new
                scaledIngredientsList.Clear(); 

                Console.WriteLine();
                Console.Write("Enter the number of ingredients: ");
                string input = Console.ReadLine();
                int numberOfIngredients;

                //Check if the integer is valid 
                while (!int.TryParse(input, out numberOfIngredients) || numberOfIngredients > Application.GetIngredients() || numberOfIngredients < 1)
                {
                    Console.Write($"Enter a valid number between 1 and {Application.GetIngredients()}: ");
                    input = Console.ReadLine();
                }

                InputIngredientDetails(numberOfIngredients, ingredientsList, originalIngredientsList, stepsList);
            }
            else
            {
                //If list not empty prompt user to clear it
                Console.WriteLine();
                Console.WriteLine("The ingredient is not empty, clear it to enter another recipe");
                Console.WriteLine();

                Application.DisplayMenu();
            }
        }

        static void InputIngredientDetails(int numberOfIngredients, List<string> ingredientsList, List<string> originalIngredientsList, List<string> stepsList)
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

                ingredientsList.Add(ingredient);
                originalIngredientsList.Add(ingredient);
            }

            InputSteps(stepsList);
        }

        static void InputSteps(List<string> stepsList)
        {
            Console.Write("Please enter the number of steps needed to make the recipe: ");
            string steps = Console.ReadLine();
            int numberOfSteps;

            //Check if the integer is valid 
            while (!int.TryParse(steps, out numberOfSteps) || numberOfSteps > Application.GetSteps() || numberOfSteps < 1)
            {
                Console.WriteLine($"Enter a valid number between 1 and {Application.GetSteps()}: ");
                steps = Console.ReadLine();
            }

            for (int i = 0; i < numberOfSteps; i++)
            {
                Console.Write($"Enter the description for step {i + 1}: ");
                string stepDescription = Console.ReadLine();
                stepsList.Add($"Step {i + 1}: {stepDescription}");
            }

            Console.WriteLine();
            Application.DisplayMenu();
        }
    }
}
