using System;
using System.Collections.Generic;

namespace PROGPOE
{
    public class Application
    {
        private static bool HasScaledIngredients = false;
        private static int MaxIngredients = 10;
        private static int MaxSteps = 10;
        
        public static List<Recipe> recipes = new List<Recipe>();


        //Main running method that controlls all of the applications functions
        public static void DisplayMenu()
        {
            Console.WriteLine("" +
                "1. Input recipe " +
                "\n2. View recipe " +
                "\n3. Scale quantities " +
                "\n4. Reset quantities " +
                "\n5. Clear all data " +
                "\n6. Select recipe " +
                "\n7. Exit");

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
                    //Allows the user to enter ingredients
                    InputItems.Ingredients(recipes);
                    break;
                case 2:
                    //Displays the recipe to to console 
                    //HelperMethods.ViewRecipe(recipes);
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
                    break;
                case 3:
                    //Allows use to sacle quantities by their own factor 
                    EditQuantities.ScaleQuantities(recipes);
                    break;
                case 4:
                    //Resets the quantities to the original value only if they have been edited
                    EditQuantities.ResetQuantities(recipes);
                    break;
                case 5:
                    //Clears the ingredients list
                    HelperMethods.Clear(recipes);
                    break;
                case 6:
                    //Allows user to change the current recipe 
                    HelperMethods.SelectRecipe(recipes);
                    break;
                case 7:
                    //Stops the application
                    HelperMethods.Exit();
                    break;
            }
        }

        public static int GetSteps()
        {
            return MaxSteps;
        }
        public static int GetIngredients()
        {
            return MaxIngredients;
        }

        public static bool GetChangedQuantity()
        {
            return HasScaledIngredients;
        }

        public static void SetChangedQuantity(bool change)
        {
            HasScaledIngredients = change;
        }
                        
    }
}