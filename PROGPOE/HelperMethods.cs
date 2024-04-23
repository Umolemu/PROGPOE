using System;
using System.Text.RegularExpressions;
namespace PROGPOE
{
    public class HelperMethods
    {
        public static void ViewRecipe(List<Recipe> recipes)
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nIngredient list is empty\n");
                Application.DisplayMenu();
                return;
            }

            foreach (Recipe recipe in recipes)
            {
                Console.WriteLine("\nRecipe Name: " + recipe.GetName());

                Console.WriteLine("Ingredients List:");
                List<Ingredient> ingredients = recipe.GetIngredients();
                if (ingredients.Count == 0)
                {
                    Console.WriteLine("No ingredients added.");
                }
                else
                {
                    foreach (Ingredient ingredient in ingredients)
                    {
                        Console.WriteLine($"- {ingredient.Name}: \n{ingredient.Quantity} \n{ingredient.Measurement} - \n{ingredient.Description}\n");
                    }
                }

                Console.WriteLine("\nSteps:");
                foreach (var step in recipe.GetSteps())
                {
                    Console.WriteLine(step);
                }

                Console.WriteLine();
            }

            Application.DisplayMenu();
        }


        public static bool ValidString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            if (!Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
            {

                return false;
            }
            return true;
        }

        public static bool ValidInteger(string input)
        {
            int number;

            if (!int.TryParse(input, out number))
            {
                return false;
            }

            if (number < 1)
            {
                return false;
            }
            return true;
        }


        public static void Clear(List<Recipe> recipes)
        {
            //Remove current recipe
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }

        public static void SelectRecipe(List<Recipe> recipes)
        {
            foreach (Recipe recipe in recipes)
            {
                Console.WriteLine(recipe.GetName());
            }
        }
    }
}