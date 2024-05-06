using System;

namespace PROGPOE
{
    public class InputItems
    {
        public static void Ingredients(List<Recipe> recipes)
        {
            Console.WriteLine();
            Console.Write("Enter the name of the Recipe: ");
            string name = Console.ReadLine();

            while (!HelperMethods.ValidString(name))
            {
                Console.Write($"Enter a valid name for the Recipe: ");
                name = Console.ReadLine();
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

            InputIngredientDetails(numberOfIngredients, newRecipe);

            }

        static void InputIngredientDetails(int numberOfIngredients, Recipe recipe)
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

                while (!HelperMethods.ValidFloat(quantityInput))
                {
                    Console.Write($"Please enter a valid quantity for ingredient {i + 1}: ");
                    quantityInput = Console.ReadLine();
                }

                float.TryParse(quantityInput, out float quantityOfIngredient);

                Console.Write($"Enter the unit measurement for ingredient {i + 1}: ");
                string measurement = Console.ReadLine();
                while (!HelperMethods.ValidString(measurement))
                {
                    Console.Write($"Enter a valid unit measurment for the ingredient {i + 1}: ");
                    measurement = Console.ReadLine();
                }

                Console.Write($"Enter the description for ingredient {i + 1}: ");
                string description = Console.ReadLine();

                while (!HelperMethods.ValidString(description))
                {
                    Console.Write($"Enter the a valid description for ingredient {i + 1}: ");
                    description = Console.ReadLine();
                }

                Console.Write($"Enter the number of calories for ingredient {i + 1}: ");
                string caloriesStr = Console.ReadLine();

                while (!HelperMethods.ValidInteger(caloriesStr))
                {
                    Console.Write($"Please enter a valid calorie count for ingredient {i + 1}: ");
                    caloriesStr = Console.ReadLine();
                }

                int calories = int.Parse(caloriesStr);

                while (calories > 300)
                {
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
                        Console.Write($"Enter the number new of calories for ingredient {i + 1}: ");
                        caloriesStr = Console.ReadLine();

                        while (!HelperMethods.ValidInteger(caloriesStr))
                        {
                            Console.Write($"Please enter a valid calorie count for ingredient {i + 1}: ");
                            caloriesStr = Console.ReadLine();
                        }

                        calories = int.Parse(caloriesStr);
                        break;
                    }
                }    
                    //Todo
                    Console.WriteLine();
                    Console.WriteLine($"Enter the food group for ingredient {nameOfIngredient}: ");
                    Console.WriteLine(
                        "\n1. Starch" +
                        "\n2. Vegetables and fruits" +
                        "\n3. Dry beans, peas, lentils or soya" +
                        "\n4. Chicken, fish, meat or egg" +
                        "\n5. Milk or dairy products" +
                        "\n6. Fats or oil" +
                        "\n7. Water");
                    Console.Write("Enter a choise: ");
                    
                    string group = Console.ReadLine();
                    string groupStr = "";


                    while (!HelperMethods.ValidInteger(group) || int.Parse(group) > 7 || int.Parse(group) < 1 )
                    {
                        Console.Write($"Enter the a valid number between 1 and 7");
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


                Ingredient ingredient = new Ingredient(nameOfIngredient, quantityOfIngredient, measurement, description, calories, groupStr);
                recipe.AddIngredient(ingredient);
            }    

            InputSteps(recipe);
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
                    Console.WriteLine($"Enter a valid number between 1 and {Application.GetSteps()}: ");
                    steps = Console.ReadLine();
                }

                for (int i = 0; i < numberOfSteps; i++)
                {
                    Console.Write($"Enter the description for step {i + 1}: ");
                    string stepDescription = Console.ReadLine();
                    recipe.AddStep(stepDescription);
                }

                Console.WriteLine();
                Console.WriteLine("Recipe added successfully.");
                Console.WriteLine();
                Application.DisplayMenu();
            }
        }
    }


