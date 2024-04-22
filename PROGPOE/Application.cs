using System;
using System.Collections.Generic;

namespace PROGPOE
{
    public class Application
    {
        private static bool HasScaledIngredients = false;
        private static int MaxIngredients = 10;
        private static int MaxSteps = 10;

        //private static string[] ingredientsList = new string[MaxIngredients];
        //private static string[] originalIngredientsList = new string[MaxIngredients];
        //private static string[] scaledIngredientsList = new string[MaxIngredients];
        //private static string[] stepsList = new string[MaxSteps];
       
        //New code  
        private static List<string> ingredientsList = new List<string>();
        private static List<string> originalIngredientsList = new List<string>();
        private static List<string> scaledIngredientsList = new List<string>();
        private static List<string> stepsList = new List<string>();


        //Main running method that controlls all of the applications functions
        public static void DisplayMenu()
        {
            Console.WriteLine("" +
                "1. Input recipe " +
                "\n2. View recipe " +
                "\n3. Scale quantities " +
                "\n4. Reset quantities " +
                "\n5. Clear all data " +
                "\n6. Exit");

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
                    InputItems.Ingredients(ingredientsList, originalIngredientsList, scaledIngredientsList, stepsList);
                    break;
                case 2:
                    //Displays the recipe to to console 
                    HelperMethods.ViewRecipe(ingredientsList, stepsList,originalIngredientsList);
                    break;
                case 3:
                    //Allows use to sacle quantities by their own factor 
                    EditQuantities.ScaleQuantities(ingredientsList, scaledIngredientsList);
                    break;
                case 4:
                    //Resets the quantities to the original value only if they have been edited
                    EditQuantities.ResetQuantities(ingredientsList, originalIngredientsList);
                    break;
                case 5:
                    //Clears the ingredients list
                    HelperMethods.Clear(ingredientsList, stepsList);
                    break;
                case 6:
                    //Stops the application
                    HelperMethods.Exit();
                    break;
            }
        }

        public static int GetSteps ()
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
