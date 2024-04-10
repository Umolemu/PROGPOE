using System;

namespace PROGPOE
{
    public class HelperMethods
    {
        public static void ViewRecipe(string[] ingredientsList, string[] stepsList, string[] original)
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

        
        public static void Clear(string[]ingredientsList, string[] stepsList)
        {
            Console.WriteLine();
            Array.Clear(ingredientsList, 0, ingredientsList.Length);
            Array.Clear(stepsList, 0, stepsList.Length);
            Application.DisplayMenu();
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
