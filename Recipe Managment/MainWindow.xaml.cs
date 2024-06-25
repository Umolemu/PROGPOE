using System.Windows;
using System.Windows.Controls;

namespace Recipe_Managment
{
    public partial class MainWindow : Window
    {
        public List<Recipe> Recipes { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            //Main recipe list
            Recipes = new List<Recipe>();
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            AddRecipe addRecipeWindow = new AddRecipe(Recipes);
            
            //Subscribe to the RecipeAdded event
            addRecipeWindow.RecipeAdded += OnRecipeAdded;
            //Display the window as a modal dialog,
            //this will block the main window until the addRecipeWindow is closed
            addRecipeWindow.ShowDialog();
        }

        private void OnRecipeAdded()
        {
            //Refresh the data grid when a recipe is added
            RefreshDataGrid();
        }

        private void EditRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            //Get the button that was clicked and the recipe that was bound to it
            var button = sender as Button;
            if (button != null)
            {
                //Cast the Tag property of the button to a Recipe object
                //to get the recipe
                Recipe recipe = button.Tag as Recipe;
                if (recipe != null)
                {
                    EditRecipe editRecipeWindow = new EditRecipe(recipe);
                    editRecipeWindow.ShowDialog();
                    RefreshDataGrid();
                }
            }
        }

        private void DeleteRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            //Same as above  
            var button = sender as Button;
            if (button != null)
            {
                Recipe recipe = button.Tag as Recipe;
                if (recipe != null)
                {
                    Recipes.Remove(recipe);
                    RefreshDataGrid();
                }
            }
        }

        private void BtnStatistics_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected recipes
            var selectedRecipes = (RecipesDataGrid.ItemsSource as List<Recipe>).Where(r => r.IsSelected).ToList();

            if (selectedRecipes.Count == 0)
            {
                MessageBox.Show("Please select at least one recipe to view statistics.", "No Recipes Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Open the Statistics window
            StatisticsWindow statisticsWindow = new StatisticsWindow(selectedRecipes);
            statisticsWindow.Show();
        }

        public void RefreshDataGrid()
        {
            // Sort recipes by name before refreshing the DataGrid
            Recipes = Recipes.OrderBy(r => r.Name).ToList();

            //Set the ItemsSource of the DataGrid to null and then back to the list of recipes
            RecipesDataGrid.ItemsSource = null;
            RecipesDataGrid.ItemsSource = Recipes;
        }
    }
}
