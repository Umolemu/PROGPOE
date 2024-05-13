using System;
using System.Collections.Generic;

namespace PROGPOE
{
    //Class that allows the user to change to quantities 
    public class EditQuantities
    {
        public static void ResetQuantities(List<Recipe> recipes)
        {
            Console.WriteLine();

            if (recipes.Count == 0)
            {
                Console.WriteLine("\nRecipe list is empty\n");
                Application.DisplayMenu();
                return;
            }

            Console.WriteLine("Select a recipe to reset its quantity");
            int option = 0;

            foreach (Recipe recipe in recipes)
            {
                Console.WriteLine($"\n{++option}. Recipe Name: {recipe.GetName()} (Calories: {HelperMethods.CalculateTotalCalories(recipe)})");

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
                            $"\n- Calories: {ingredient.Calories}" +
                            $"\n- Food Group: {ingredient.Group}"
                       );
                    }
                }
            }

            Console.Write("Enter your choice: ");
            string choiseStr = Console.ReadLine();

            while (!HelperMethods.ValidInteger(choiseStr) || int.Parse(choiseStr) > recipes.Count || int.Parse(choiseStr) < 1)
            {
                Console.Write($"Enter a valid number between 1 and {recipes.Count}: ");
                choiseStr = Console.ReadLine();
            }

            int choise = int.Parse(choiseStr);
            int count = 0;

            foreach (Recipe recipe in recipes)
            {
                if (count == choise - 1)
                {
                    Console.WriteLine();
                    Console.WriteLine("Select an Ingredient to reset: ");
                    Console.WriteLine();

                    List<Ingredient> ingredients = recipe.GetIngredients();

                    int index = 0;
                    foreach (Ingredient ingredient in ingredients)
                    {
                        Console.WriteLine($"\n{++index}. Ingredient Name: {ingredient.Name} (Calories: {ingredient.Calories})");

                        Console.WriteLine(
                            $"\n- Name: {ingredient.Name}" +
                            $"\n- Quantity: {ingredient.Quantity}" +
                            $"\n- Measurment: {ingredient.Measurement}" +
                            $"\n- Calories: {ingredient.Calories}" +
                            $"\n- Food Group: {ingredient.Group}"
                       );
                    }

                    Console.WriteLine();
                    Console.Write("Select an option: ");
                    string optionTwoStr = Console.ReadLine();

                    while (!HelperMethods.ValidInteger(optionTwoStr) || int.Parse(optionTwoStr) > ingredients.Count || int.Parse(optionTwoStr) < 1)
                    {
                        Console.Write($"select a valid option between 1 and {ingredients.Count}: ");
                        optionTwoStr = Console.ReadLine();
                    }

                    int optionTwo = int.Parse(optionTwoStr);
                                      

                    int countTwo = 0;

                    foreach (Ingredient ingredient in ingredients)
                    {

                        if (countTwo == optionTwo - 1)
                        {
                            var scaledFactor = ingredient.OriginalQuantity / ingredient.Quantity;
                            ingredient.Quantity = ingredient.OriginalQuantity;
                            ingredient.Calories *= scaledFactor;
                        }
                        countTwo++;
                    }


                }
                else if (count > choise)
                {
                    break;
                }

                count++;
            }
            Console.WriteLine();
            Console.WriteLine("Quantity successfuly changed");
            Console.WriteLine();
            Application.DisplayMenu();
        }

        public static void ScaleQuantities(List<Recipe> recipes)
        {

            Console.WriteLine();

            if (recipes.Count == 0)
            {
                Console.WriteLine("\nRecipe list is empty\n");
                Application.DisplayMenu();
                return;
            }

            Console.WriteLine("Select a recipe to change the quantity");
            int option = 0;

            foreach (Recipe recipe in recipes)
            {
                Console.WriteLine($"\n{++option}. Recipe Name: {recipe.GetName()} (Calories: {HelperMethods.CalculateTotalCalories(recipe)})");

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
                            $"\n- Calories: {ingredient.Calories}" +
                            $"\n- Food Group: {ingredient.Group}"
                       );
                    }
                }
            }

            Console.Write("Enter your choice: ");
            string choiseStr = Console.ReadLine();

            while(!HelperMethods.ValidInteger(choiseStr) || int.Parse(choiseStr) > recipes.Count || int.Parse(choiseStr) < 1)
            {
                Console.Write($"Enter a valid number between 1 and {recipes.Count}: ");
                choiseStr = Console.ReadLine();
            }

            int choise = int.Parse(choiseStr);
            int count = 0;

            foreach(Recipe recipe in recipes)
            {
                if (count == choise - 1)
                {
                    Console.WriteLine();
                    Console.WriteLine("Select an Ingredient to scale: ");
                    Console.WriteLine();

                    List<Ingredient> ingredients = recipe.GetIngredients();

                    int index = 0;
                    foreach(Ingredient ingredient in ingredients)
                    {
                        Console.WriteLine($"\n{++index}. Ingredient Name: {ingredient.Name} (Calories: {ingredient.Calories})");

                        Console.WriteLine(
                            $"\n- Name: {ingredient.Name}" +
                            $"\n- Quantity: {ingredient.Quantity}" +
                            $"\n- Measurment: {ingredient.Measurement}" +
                            $"\n- Calories: {ingredient.Calories}" +
                            $"\n- Food Group: {ingredient.Group}"
                       );
                    }
                    
                    Console.WriteLine();
                    Console.Write("Select an option: ");
                    string optionTwoStr = Console.ReadLine();

                    while (!HelperMethods.ValidInteger(optionTwoStr) || int.Parse(optionTwoStr) > ingredients.Count || int.Parse(optionTwoStr) < 1)
                    {
                        Console.Write($"select a valid option between 1 and {ingredients.Count}: ");
                        optionTwoStr = Console.ReadLine();
                    }

                    int optionTwo = int.Parse(optionTwoStr);

                    Console.WriteLine();
                    Console.Write("Enter a scaling factor: ");

                    string factorStr = Console.ReadLine();
                    while(!HelperMethods.ValidFloat(factorStr) || float.Parse(factorStr) > 50.00)
                    {
                        Console.Write("Enter a valid scaling factor greater than 0 and less than 50: ");
                        factorStr = Console.ReadLine();
                    }
                    float factor = float.Parse(factorStr);

                    int countTwo = 0;
                    
                    foreach(Ingredient ingredient in ingredients) {
                        
                        if(countTwo == optionTwo - 1)
                        {
                            ingredient.Quantity *= factor;
                            ingredient.Calories *= factor;
                        }
                        countTwo++;
                    }

                    
                } else if (count > choise) {
                    break;
                }

                count++;
            }
            Console.WriteLine();
            Console.WriteLine("Quantity successfuly changed");
            Console.WriteLine();
            Application.DisplayMenu();
        }
    }
}
