using PROGPOE;

public class Recipe
{
    private string name;
    private List<Ingredient> ingredientsList;
    private List<string> stepsList;

    public Recipe(string name)
    {
        this.name = name;
        this.ingredientsList = new List<Ingredient>();
        this.stepsList = new List<string>();
    }

    public string GetName()
    {
        return name;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredientsList.Add(ingredient);
    }

    public void AddStep(string step)
    {
        stepsList.Add(step);
    }

    // Method to retrieve the steps of the recipe
    public List<string> GetSteps()
    {
        return stepsList;
    }

    public List<Ingredient> GetIngredients()
    {
        return ingredientsList;
    }

}
