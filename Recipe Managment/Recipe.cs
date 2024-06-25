using System;
using System.Collections.Generic;

namespace Recipe_Managment
{
    public class Recipe
    {
        public string Name { get; set; }
        private List<Ingredient> ingredientsList;
        private List<string> stepsList;
        //These two fields are used to get the count of ingredients and steps which are displaed on main window
        public int IngredientsCount => ingredientsList.Count;
        public int StepsCount => stepsList.Count;
        //Track selection state
        public bool IsSelected { get; set; }

        public Recipe(string name)
        {
            Name = name;
            ingredientsList = new List<Ingredient>();
            stepsList = new List<string>();
        }

        public void AddIngredients(List<Ingredient> ingredients)
        {
            ingredientsList.AddRange(ingredients);
        }

        public List<Ingredient> GetIngredients()
        {
            return ingredientsList;
        }

        public void AddSteps(List<string> steps)
        {
            stepsList.AddRange(steps);
        }

        public List<string> GetSteps()
        {
            return stepsList;
        }
    }
}
