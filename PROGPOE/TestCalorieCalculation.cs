namespace PROGPOE
{
    public class TestCalorieCalculation
    {
        public static void RunTest()
        {
            // Create a recipe with 3 ingredients
            Recipe testRecipe = new Recipe("Test Recipe");
            testRecipe.AddIngredient(new Ingredient("Ingredient 1", 100, "grams", 50, "Starch", 100));
            testRecipe.AddIngredient(new Ingredient("Ingredient 2", 200, "grams", 80, "Vegetable or fruit", 200));
            testRecipe.AddIngredient(new Ingredient("Ingredient 3", 150, "grams", 120, "Meat", 150));

            // Calculate the expected total calories
            float expectedTotalCalories = 50 + 80 + 120;

            // Calculate the total calories using the helper method
            float actualTotalCalories = HelperMethods.CalculateTotalCalories(testRecipe);


            // Compare the expected and actual total calories
            if (expectedTotalCalories == actualTotalCalories)
            {
                Console.WriteLine("Calorie calculation test passed!");
            }
            else
            {
                Console.WriteLine("Calorie calculation test failed!");
            }
        }
              
    }
}
