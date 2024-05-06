using System;
using System.Collections.Generic;

namespace PROGPOE
{
    //Class that allows the user to change to quantities 
    public class EditQuantities
    {
        public static void ResetQuantities(List<Recipe> recipes)
        {

        }

        public static void ScaleQuantities(List<Recipe> recipes)
        {

            Console.WriteLine();

            if (recipes.Count == 0)
            {
                Console.WriteLine("\nIngredient list is empty\n");
                Application.DisplayMenu();
                return;
            }

            Console.WriteLine("Select a recipe to change the quantity");
            int option = 1;

            foreach (Recipe recipe in recipes)
            {
                Console.WriteLine($"\n{++option} Recipe Name: " + recipe.GetName());

                Console.WriteLine();

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

                        Console.WriteLine(
                            $"\n- Name: {ingredient.Name}" +
                            $"\n- Quantity: {ingredient.Quantity}" +
                            $"\n- Measurment: {ingredient.Measurement}" +
                            $"\n- Description: {ingredient.Description}"
                       );
                    }
                }
            }
        }
    }
}
