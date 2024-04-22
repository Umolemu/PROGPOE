﻿using System;
using System.Collections.Generic;

namespace PROGPOE
{
    //Class that allows the user to change to quantities 
    public class EditQuantities
    {
        public static void ResetQuantities(List<string> ingredientsList, List<string> originalIngredientsList)
        {
            //Array.Clear(ingredientsList, 0, ingredientsList.Length);

            //new
            ingredientsList.Clear();


            //for (int i = 0; i < originalIngredientsList.Count; i++)
            //{
            //    ingredientsList[i] = originalIngredientsList[i];
            //}

            foreach(string ing in originalIngredientsList)
            {
                ingredientsList.Add(ing);
            }
            
            Application.SetChangedQuantity(false);
            Console.WriteLine();
            Console.WriteLine("Quantities reset successfully.");
            Console.WriteLine();
            Application.DisplayMenu();
        }

        public static void ScaleQuantities(List<string> ingredientsList, List<string> scaledIngredientsList)
        {

            Console.WriteLine();
            if (ingredientsList.Count == 0)
            {
                Console.WriteLine("Cannot change recipe without creating one, first create a recipie.");
                Console.WriteLine();
                Application.DisplayMenu();
            }

            if (Application.GetChangedQuantity())
            {

                Console.WriteLine("You have already changed the quantity, reset it to change it again.");
                Console.WriteLine();
                Application.DisplayMenu();
            } 

            Console.Write("Enter the scaling factor: ");
            string factorInput = Console.ReadLine();
            int factor;

            //Check if the integer is valid
            while (!int.TryParse(factorInput, out factor) || factor <= 0 || factor > 30)
            {
                Console.Write("Invalid input. Please enter a positive number less than 30: ");
                factorInput = Console.ReadLine();
            }

            //Array.Clear(scaledIngredientsList, 0, scaledIngredientsList.Length);

            //new
            scaledIngredientsList.Clear();

            for (int i = 0; i < ingredientsList.Count; i++)
            {
                if (ingredientsList.Count != 0)
                {
                    string[] parts = ingredientsList[i].Split('\n');
                    foreach (var part in parts)
                    {
                        if (part.Contains("quantity"))
                        {
                            string[] quantityParts = part.Split(':');
                            if (int.TryParse(quantityParts[1].Trim(), out int quantity))
                            {
                                quantity *= factor;
                                scaledIngredientsList.Add($"{parts[0]}\nquantity: {quantity}\n{parts[2]}\n{parts[3]}\n");
                            }
                        }
                    }
                }
            }

            //Array.Copy(scaledIngredientsList, ingredientsList, scaledIngredientsList.Length);

            //new
            ingredientsList.Clear ();
            ingredientsList.AddRange(scaledIngredientsList);

            Application.SetChangedQuantity(true);
            Console.WriteLine();
            Console.WriteLine("Quantities scaled successfully.");
            Console.WriteLine();
            Application.DisplayMenu();
        }

    }
}