using System;

namespace PROGPOE
{
    public class InputItems
    {
        public static void Ingredients(List<Recipe> recipies)
        {

            Console.WriteLine();
            Console.Write("Enter the name of the Recipe: ");
            string name = Console.ReadLine();

            while (!HelperMethods.ValidString(name))
            {
                    Console.Write($"Enter a valid name for the Recipe: ");
                    name = Console.ReadLine();
            }

            Recipe newRecipe = new Recipe(name);

            Console.Write("Enter the number of ingredients: ");
            string input = Console.ReadLine();
            int numberOfIngredients;

            //Check if the integer is valid 
             while (!int.TryParse(input, out numberOfIngredients) || numberOfIngredients > Application.GetIngredients() || numberOfIngredients < 1)
             {
                Console.Write($"Enter a valid number between 1 and {Application.GetIngredients()}: ");
                input = Console.ReadLine();
             }

            InputIngredientDetails(numberOfIngredients, newRecipe);

            recipies.Add(newRecipe);

            Console.WriteLine("Recipe added successfully.");
            Console.WriteLine();
            //Application.DisplayMenu();

        }

        static void InputIngredientDetails(int numberOfIngredients, Recipe recipe)
        {
            for (int i = 0; i < numberOfIngredients; i++)
            {
                Console.Write($"Enter the name of ingredient {i + 1}: ");
                string nameOfIngredient = Console.ReadLine();

                while (!HelperMethods.ValidString(nameOfIngredient))
                {
                    Console.Write($"Enter a valid name for the ingredient {i + 1}: ");
                    nameOfIngredient = Console.ReadLine();
                }

                Console.Write($"Enter the quantity of ingredient {i + 1}: ");
                string quantityInput = Console.ReadLine();

                while (!HelperMethods.ValidInteger(quantityInput))
                {
                    Console.Write($"Please enter a valid quantity for ingredient {i + 1}: ");
                    quantityInput = Console.ReadLine();
                }

                int.TryParse(quantityInput, out int quantityOfIngredient);

                Console.Write($"Enter the unit measurement for ingredient {i + 1}: ");
                string measurement = Console.ReadLine();
                while (!HelperMethods.ValidString(measurement))
                {
                    Console.Write($"Enter a valid unit measurment for the ingredient {i + 1}: ");
                    measurement = Console.ReadLine();
                }

                Console.Write($"Enter the description for ingredient {i + 1}: ");
                string description = Console.ReadLine();

                while (!HelperMethods.ValidString(description))
                {
                    Console.Write($"Enter the a valid description for ingredient {i + 1}: ");
                    description = Console.ReadLine();
                }

                Ingredient ingredient = new Ingredient(nameOfIngredient, quantityOfIngredient, measurement, description);
                recipe.AddIngredient(ingredient);
            }    

            InputSteps(recipe);
        }

        static void InputSteps(Recipe recipe)
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
                recipe.AddStep(stepDescription);
            }

            Console.WriteLine();
            Application.DisplayMenu();
        }
    }
}

