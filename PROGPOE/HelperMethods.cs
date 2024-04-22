using System;

namespace PROGPOE
{
    public class HelperMethods
    {
        public static void ViewRecipe(List<string> ingredientsList, List<string> stepsList, List<string> original)
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
            Application.DisplayMenu();
        }

        
        public static void Clear(List<string> ingredientsList, List<string> stepsList)  {
            Console.WriteLine();
            //Array.Clear(ingredientsList, 0, ingredientsList.Length);

            //new
            ingredientsList.Clear();
            
            //Array.Clear(stepsList, 0, stepsList.Length);
            
            //new
            stepsList.Clear();
            Console.WriteLine("Recipe removed.");
            Console.WriteLine();

            Application.DisplayMenu();
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
