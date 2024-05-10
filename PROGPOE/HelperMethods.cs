using System.Text.RegularExpressions;
namespace PROGPOE
{
    public class HelperMethods
    {
        public static void ViewRecipe(List<Recipe> recipes)
        {
            if (recipes.Count == 0) 
            {
                Console.WriteLine("\nRecipe list is empty\n");
                Application.DisplayMenu();
                return;
            }

            foreach (Recipe recipe in recipes)
            {
                Console.WriteLine($"\nRecipe Name: {recipe.GetName()} (Calories: {CalculateTotalCalories(recipe)})");

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

                Console.WriteLine("\nSteps:");

                int steps = 1;
                foreach (var step in recipe.GetSteps())
                {
                    Console.WriteLine($"- Step {steps}: {step}");
                    steps++;
                }

                Console.WriteLine();
            }

            Application.DisplayMenu();
        }

        public static float CalculateTotalCalories(Recipe recipe)
        {
            float total = 0;
            foreach(Ingredient ingridient in recipe.GetIngredients())
            {
                total += ingridient.Calories;
            }
            return total;
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

        public static bool ValidFloat(string input)
        {
            float number;

            if (!float.TryParse(input, out number))
            {
                return false;
            }
            
            if (number < 0)
            {
                return false;
            }

            return true;
        }


        public static void Clear(List<Recipe> recipes)
        {
            Console.WriteLine("Are you sure you want to clear all data");
            Console.WriteLine(
                "\n1.Yes" +
                "\n2.No"
                );
            Console.Write("Select an option: ");
            string choise = Console.ReadLine();

            while (!ValidInteger(choise))
            {
                Console.Write($"Please enter a valid option: ");
                choise = Console.ReadLine();
            }

            int.TryParse(choise, out int vallidChoise);
            
            Console.WriteLine();

            if(vallidChoise == 1)
            {
                recipes.Clear();
                
                Console.WriteLine("All data removed");
                Console.WriteLine();

                Application.DisplayMenu();
            } 
            if(vallidChoise == 2)
            {
                Application.DisplayMenu();
            }
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