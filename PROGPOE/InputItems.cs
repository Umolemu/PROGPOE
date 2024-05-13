using System;
using System.Xml.Linq;

namespace PROGPOE
{
    public class InputItems
    {
        //Declare the delegate 
        public delegate string NotifyCaloriesExceedThreshold(Recipe ingredientName);

        public static void Ingredients(List<Recipe> recipes)
        {

            Console.WriteLine();
            Console.Write("Enter the name of the Recipe: ");
            string name = Console.ReadLine();


            while (!HelperMethods.ValidString(name))
            {
                Console.Write($"Enter a valid name for the Ingredient: ");
                name = Console.ReadLine();
            }

            foreach (Recipe recipe in recipes)
            {
                while (recipe.GetName().ToLower() == name.ToLower())
                {
                    Console.WriteLine($"Recipe with the name of {name} exists, Please use another recipe name: ");
                    name = Console.ReadLine();
                    while (!HelperMethods.ValidString(name))
                    {
                        Console.Write($"Enter a valid name for the Recipe: ");
                        name = Console.ReadLine();
                    }
                }
            }    
            
            Recipe newRecipe = new Recipe(name);

            Console.Write("Enter the number of ingredients: ");
            string input = Console.ReadLine();
            int numberOfIngredients;

            while (!int.TryParse(input, out numberOfIngredients) || numberOfIngredients > Application.GetIngredients() || numberOfIngredients < 1)
            {
                Console.Write($"Enter a valid number between 1 and {Application.GetIngredients()}: ");
                input = Console.ReadLine();
            }

            recipes.Add(newRecipe);

            //Pass the delegate to the InputIngredientDetails method
            InputIngredientDetails(numberOfIngredients, newRecipe, NotifyCaloriesExceed);

            }

        static void InputIngredientDetails(int numberOfIngredients, Recipe recipe, NotifyCaloriesExceedThreshold notify)
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

                while (!HelperMethods.ValidFloat(quantityInput) || float.Parse(quantityInput) > 50)
                {
                    Console.Write($"Please enter a valid quantity for ingredient {i + 1} between 1 and 50: ");
                    quantityInput = Console.ReadLine();
                }

                float.TryParse(quantityInput, out float quantityOfIngredient);
                float originalQuantity = quantityOfIngredient;


                Console.Write($"Enter the unit measurement for ingredient {i + 1}: ");
                string measurement = Console.ReadLine();
                while (!HelperMethods.ValidString(measurement))
                {
                    Console.Write($"Enter a valid unit measurment for the ingredient {i + 1}: ");
                    measurement = Console.ReadLine();
                }

                Console.Write($"Enter the number of calories for ingredient {i + 1}: ");
                string caloriesStr = Console.ReadLine();

                while (!HelperMethods.ValidFloat(caloriesStr) || float.Parse(caloriesStr) > 2000)
                {
                    Console.Write($"Please enter a valid calorie count for ingredient {i + 1} that is between 1 and 2000: ");
                    caloriesStr = Console.ReadLine();
                }

                float calories = float.Parse(caloriesStr);

                Console.WriteLine();
                    Console.WriteLine($"Enter the food group for ingredient {nameOfIngredient}: ");
                    Console.WriteLine(
                        "1. Starch" +
                        "\n2. Vegetables and fruits" +
                        "\n3. Dry beans, peas, lentils or soya" +
                        "\n4. Chicken, fish, meat or egg" +
                        "\n5. Milk or dairy products" +
                        "\n6. Fats or oil" +
                        "\n7. Water");
                    Console.Write("Enter a choice: ");
                    
                    string group = Console.ReadLine();
                    string groupStr = "";


                    while (!HelperMethods.ValidInteger(group) || int.Parse(group) > 7 || int.Parse(group) < 1 )
                    {
                        Console.Write($"Enter the a valid number between 1 and 7: ");
                        group = Console.ReadLine();
                    }
                    

                    switch(int.Parse(group))
                    {
                        case 1:
                            groupStr = "Starch";
                            break;
                        case 2:
                            groupStr = "Vegetable or fruit";
                            break;
                        case 3:
                            groupStr = "Dry beans, peas, lentils and soya";
                            break;
                        case 4:
                            groupStr = "Chicken, fish, meat or egg";
                            break;
                        case 5:
                            groupStr = "Milk or dairy products";
                            break;
                        case 6:
                            groupStr = "Fats or oil";
                            break;
                        case 7:
                            groupStr = "Water";
                            break;
                    } 
                 

                Ingredient ingredient = new Ingredient(nameOfIngredient, quantityOfIngredient, measurement, calories, groupStr, originalQuantity);

                                
                recipe.AddIngredient(ingredient);
            }    
                
                
                while (HelperMethods.CalculateTotalCalories(recipe) > 300)
                {
                    string outcome = notify(recipe);
                    if (outcome == "change")
                {
                    continue;
                }
                    else if(outcome == "nochange")
                {
                    break;
                }
                } 
                
            InputSteps(recipe);
        }
        
        //Code path uses a delegate to determine if calories are over 300 
        static string NotifyCaloriesExceed(Recipe recipe)
        {

            string outcome = "";
            
            Console.WriteLine();
            Console.WriteLine("Calories are greater than 300 Would you like to change it?");
            Console.WriteLine(
                    "\n1.Yes" +
                    "\n2.No"
                    );
            Console.Write("Select an option: ");

            string choiseStr = Console.ReadLine();

            while (!HelperMethods.ValidInteger(choiseStr) || int.Parse(choiseStr) > 2 || int.Parse(choiseStr) < 1)
            {
                Console.Write($"Please enter a valid number between 1 and 2: ");
                choiseStr = Console.ReadLine();
            }

            int choise = int.Parse(choiseStr);


            if (choise == 1)
            {
                List<Ingredient> ingredients = recipe.GetIngredients();
                
                Console.WriteLine();
                Console.Write($"Enter the ingredient to change its calories");
                Console.WriteLine();

                //print list of ingredients
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

                while (!HelperMethods.ValidInteger(optionTwoStr) || int.Parse(optionTwoStr) < 1 || int.Parse(optionTwoStr) > ingredients.Count)
                {
                    Console.Write($"Please enter a valid number between count for ingredient 1 and {ingredients.Count}: ");
                    optionTwoStr = Console.ReadLine();
                }

                int optionTwo = int.Parse(optionTwoStr);

                int countTwo = 0;

                foreach (Ingredient ingredient in ingredients)
                {

                    if (countTwo == optionTwo - 1)
                    {
                        Console.WriteLine();
                        Console.Write($"Enter the number of calories for {ingredient.Name}: ");
                        string caloriesStr = Console.ReadLine();

                        while (!HelperMethods.ValidFloat(caloriesStr))
                        {
                            Console.Write($"Please enter a valid calorie count for ingredient {ingredient.Name}: ");
                            caloriesStr = Console.ReadLine();
                        }

                        float calories = float.Parse(caloriesStr);
                        ingredient.Calories = calories;

                        outcome = "changed";
                    }
                    countTwo++;
                }

            }
            else if (choise == 2)
            {
                return "nochange";
            }
            return outcome;
        }



        static void InputSteps(Recipe recipe)
            {
                Console.WriteLine();
                Console.Write("Please enter the number of steps needed to make the recipe: ");
                string steps = Console.ReadLine();
                int numberOfSteps;

                //Check if the integer is valid 
                while (!int.TryParse(steps, out numberOfSteps) || numberOfSteps > Application.GetSteps() || numberOfSteps < 1)
                {
                    Console.Write($"Enter a valid number between 1 and {Application.GetSteps()}: ");
                    steps = Console.ReadLine();
                }

                for (int i = 0; i < numberOfSteps; i++)
                {
                    Console.Write($"Enter the description for step {i + 1}: ");
                    string stepDescription = Console.ReadLine();

                    while(!HelperMethods.ValidString(stepDescription))
                {
                    Console.Write($"Enter a valid description for step {i + 1}: ");
                    stepDescription = Console.ReadLine();
                }

                    recipe.AddStep(stepDescription);
                }

                Console.WriteLine();
                Console.WriteLine("Recipe added successfully.");
                Console.WriteLine();
                Application.DisplayMenu();
            }
        }
    }


