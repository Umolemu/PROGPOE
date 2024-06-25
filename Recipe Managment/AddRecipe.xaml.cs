using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Recipe_Managment
{
    public partial class AddRecipe : Window
    {
        private List<Recipe> Recipes;
        // Add a public event to notify the main window that a recipe has been added
        public event Action RecipeAdded;

        //Keep track of the number of ingredients and steps that need to be added
        private int ingredientsCount = 0;
        private int stepsCount = 0;

        //Dictionaries to keep track of the text boxes for each ingredient and step and their respective indexes
        private Dictionary<int, TextBox> ingredientNameTextBoxes = new Dictionary<int, TextBox>();
        private Dictionary<int, TextBox> quantityTextBoxes = new Dictionary<int, TextBox>();
        private Dictionary<int, TextBox> measurementTextBoxes = new Dictionary<int, TextBox>();
        private Dictionary<int, TextBox> caloriesTextBoxes = new Dictionary<int, TextBox>();
        private Dictionary<int, TextBox> groupTextBoxes = new Dictionary<int, TextBox>();
        private Dictionary<int, TextBox> stepTextBoxes = new Dictionary<int, TextBox>();

        private Recipe currentRecipe;

        public AddRecipe(List<Recipe> recipes)
        {
            InitializeComponent();
            Recipes = recipes;
            currentRecipe = new Recipe(string.Empty);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //Add validation for recipe name and number of ingredients
            string recipeName = txtRecipName.Text.Trim();
            if (string.IsNullOrEmpty(recipeName))
            {
                MessageBox.Show("Please enter a recipe name.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Regex.IsMatch(recipeName, @"^\d+$"))
            {
                MessageBox.Show("Recipe name cannot be just a number.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int numberOfIngredients = 0;
            if (!int.TryParse(txtNumberOfIngredients.Text.Trim(), out numberOfIngredients) || numberOfIngredients < 1)
            {
                MessageBox.Show("Please enter a valid number for the number of ingredients.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(numberOfIngredients > 10)
            {
                MessageBox.Show("Please enter a number of ingredients less than 10.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            currentRecipe = new Recipe(recipeName);

            submitButton.IsEnabled = false;
            txtRecipName.IsEnabled = false;
            txtNumberOfIngredients.IsEnabled = false;

            AddIngredientFields(numberOfIngredients);
        }

        private void AddIngredientFields(int numberOfIngredients)
        {
            //Create a stack panel to hold the ingredient input fields
            stackPanelIngredients.Children.Clear();

            for (int i = 1; i <= numberOfIngredients; i++)
            {
                //Create a stack panel for each ingredient
                StackPanel ingredientStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 5, 0, 5)
                };
                //Ingredient name text box
                TextBox txtIngredientName = new TextBox
                {
                    Width = 150,
                    Margin = new Thickness(0, 0, 10, 0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = $"Ingredient {i}"
                };
                ingredientNameTextBoxes.Add(i, txtIngredientName);
                //Quantity text box
                TextBox txtQuantity = new TextBox
                {
                    Width = 50,
                    Margin = new Thickness(0, 0, 10, 0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = "0"
                };
                quantityTextBoxes.Add(i, txtQuantity);
                //Measurement text box
                TextBox txtMeasurement = new TextBox
                {
                    Width = 100,
                    Margin = new Thickness(0, 0, 10, 0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = "grams"
                };
                measurementTextBoxes.Add(i, txtMeasurement);
                //Calories text box
                TextBox txtCalories = new TextBox
                {
                    Width = 50,
                    Margin = new Thickness(0, 0, 10, 0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = "0"
                };
                caloriesTextBoxes.Add(i, txtCalories);
                //Food group text box
                TextBox txtGroup = new TextBox
                {
                    Width = 125,
                    Margin = new Thickness(0, 0, 10, 0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = "Starch"
                };
                //Add the text boxes to the respective dictionaries
                groupTextBoxes.Add(i, txtGroup);
                //Add the labels and text boxes to the stack panel
                ingredientStackPanel.Children.Add(new Label { Content = $"Ingredient {i}:", VerticalAlignment = VerticalAlignment.Center });
                ingredientStackPanel.Children.Add(txtIngredientName);
                ingredientStackPanel.Children.Add(new Label { Content = "Quantity:", VerticalAlignment = VerticalAlignment.Center });
                ingredientStackPanel.Children.Add(txtQuantity);
                ingredientStackPanel.Children.Add(new Label { Content = "Measurement:", VerticalAlignment = VerticalAlignment.Center });
                ingredientStackPanel.Children.Add(txtMeasurement);
                ingredientStackPanel.Children.Add(new Label { Content = "Calories:", VerticalAlignment = VerticalAlignment.Center });
                ingredientStackPanel.Children.Add(txtCalories);
                ingredientStackPanel.Children.Add(new Label { Content = "Food Group:", VerticalAlignment = VerticalAlignment.Center });
                ingredientStackPanel.Children.Add(txtGroup);
                //Add the stack panel to the main stack panel
                stackPanelIngredients.Children.Add(ingredientStackPanel);
            }

            ingredientsCount = numberOfIngredients;
            //Create a submit button to submit the ingredients
            Button submitIngredientsButton = new Button
            {
                Content = "Submit Ingredients",
                Width = 150,
                Height = 30,
                Margin = new Thickness(0, 10, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Background = new SolidColorBrush(Color.FromRgb(77, 173, 128)),
                Foreground = new SolidColorBrush(Colors.White)
            };
            //Add an event handler to the submit button
            submitIngredientsButton.Click += SubmitIngredientsButton_Click;
            //Add the submit button to the main stack panel
            stackPanelIngredients.Children.Add(submitIngredientsButton);
        }

        private void SubmitIngredientsButton_Click(object sender, RoutedEventArgs e)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            float totalCalories = 0;

            for (int i = 1; i <= ingredientsCount; i++)
            {
                // Get the values from the text boxes for each ingredient
                if (ingredientNameTextBoxes.TryGetValue(i, out TextBox txtIngredientName) &&
                    quantityTextBoxes.TryGetValue(i, out TextBox txtQuantity) &&
                    measurementTextBoxes.TryGetValue(i, out TextBox txtMeasurement) &&
                    caloriesTextBoxes.TryGetValue(i, out TextBox txtCalories) &&
                    groupTextBoxes.TryGetValue(i, out TextBox txtGroup))
                {
                    // Ingredient name validation
                    if (string.IsNullOrEmpty(txtIngredientName.Text.Trim()))
                    {
                        MessageBox.Show($"Please enter ingredient name for Ingredient {i}.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (Regex.IsMatch(txtIngredientName.Text.Trim(), @"^\d+$"))
                    {
                        MessageBox.Show("Ingredient name cannot be just a number.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Quantity validation
                    if (!float.TryParse(txtQuantity.Text.Trim(), out float quantity) || quantity < 1 || quantity > 500)
                    {
                        MessageBox.Show($"Please enter a valid quantity between 1 and 500 for Ingredient {i}.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Measurement validation
                    if (string.IsNullOrEmpty(txtMeasurement.Text.Trim()))
                    {
                        MessageBox.Show($"Please enter measurement for Ingredient {i}.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (Regex.IsMatch(txtMeasurement.Text.Trim(), @"^\d+$"))
                    {
                        MessageBox.Show("Measurement cannot be just a number.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Calories validation
                    if (!float.TryParse(txtCalories.Text.Trim(), out float calories) || calories < 0 || calories > 2000)
                    {
                        MessageBox.Show($"Please enter valid calories between 0 and 2000 for Ingredient {i}.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Food group validation
                    if (string.IsNullOrEmpty(txtGroup.Text.Trim()))
                    {
                        MessageBox.Show($"Please enter food group for Ingredient {i}.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (Regex.IsMatch(txtGroup.Text.Trim(), @"^\d+$"))
                    {
                        MessageBox.Show($"Food group cannot be a number for Ingredient {i}.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Create an ingredient object and add it to the list of ingredients
                    Ingredient ingredient = new Ingredient(
                        txtIngredientName.Text.Trim(),
                        quantity,
                        txtMeasurement.Text.Trim(),
                        calories,
                        txtGroup.Text.Trim(),
                        quantity
                    );
                    ingredients.Add(ingredient);

                    // Add calories to total calories
                    totalCalories += calories;
                }
            }

            // Check if total calories exceed 300
            if (totalCalories > 300)
            {
                // Notify the user
                notify();
            }

            // Add the ingredients to the current recipe
            currentRecipe.AddIngredients(ingredients);

            // Disable the submit button
            Button submitIngredientsButton = (Button)sender;
            submitIngredientsButton.IsEnabled = false;

            MessageBox.Show("Ingredients entered successfully.");

            AddStepInput();
        }


        private void AddStepInput()
        {
            //Create a stack panel to hold the step input fields
            StackPanel stepsStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 5, 0, 5)
            };
            //Create a text box for the number of steps
            TextBox txtStepNumber = new TextBox
            {
                Name = $"txtStepNumber",
                Width = 150,
                Margin = new Thickness(0, 0, 10, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Text = "1"
            };
            //Add the text box to the stack panel
            stepsStackPanel.Children.Add(new Label { Content = $"Enter number of steps", VerticalAlignment = VerticalAlignment.Center });
            stepsStackPanel.Children.Add(txtStepNumber);

            stackPanelSteps.Children.Add(stepsStackPanel);
            //Create a submit button to submit the number of steps
            Button submitStepNumber = new Button
            {
                Content = "Submit Number of Steps",
                Width = 150,
                Height = 30,
                Margin = new Thickness(0, 10, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Background = new SolidColorBrush(Color.FromRgb(77, 173, 128)),
                Foreground = new SolidColorBrush(Colors.White)
            };
            //Add an event handler to the submit button
            submitStepNumber.Click += (sender, e) => SubmitStepNumber_Click(sender, e, txtStepNumber.Text);
            stackPanelSteps.Children.Add(submitStepNumber);
        }

        private void SubmitStepNumber_Click(object sender, RoutedEventArgs e, string stepNumberText)
        {
            // Validate the number of steps
            if (string.IsNullOrEmpty(stepNumberText) || !int.TryParse(stepNumberText, out int numberOfSteps) || numberOfSteps <= 0 || numberOfSteps > 10)
            {
                MessageBox.Show("Please enter a valid number of steps between 1 and 10.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AddStepsFields(numberOfSteps);

            Button submitStepNumber = (Button)sender;
            submitStepNumber.IsEnabled = false;
        }

        private void AddStepsFields(int numberOfSteps)
        {
            //Create a stack panel to hold the step input fields
            stackPanelSteps.Children.Clear();

            for (int i = 1; i <= numberOfSteps; i++)
            {
                //Create a stack panel for each step
                StackPanel stepStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 5, 0, 5)
                };
                //Create a text box for each step
                TextBox txtStep = new TextBox
                {
                    Width = 300,
                    Margin = new Thickness(0, 0, 10, 0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = $"Step {i}"
                };
                //Add the text box to the dictionary
                stepTextBoxes.Add(i, txtStep);
                //Add the labels and text boxes to the stack panel  
                stepStackPanel.Children.Add(new Label { Content = $"Step {i}:", VerticalAlignment = VerticalAlignment.Center });
                stepStackPanel.Children.Add(txtStep);
                //Add the stack panel to the main stack panel
                stackPanelSteps.Children.Add(stepStackPanel);
            }

            stepsCount = numberOfSteps;
            //Create a submit button to submit the steps
            Button submitStepsButton = new Button
            {
                Content = "Submit Steps",
                Width = 150,
                Height = 30,
                Margin = new Thickness(0, 10, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Background = new SolidColorBrush(Color.FromRgb(77, 173, 128)),
                Foreground = new SolidColorBrush(Colors.White)
            };
            //Add an event handler to the submit button
            submitStepsButton.Click += SubmitStepsButton_Click;
            stackPanelSteps.Children.Add(submitStepsButton);
        }

        private void SubmitStepsButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> steps = new List<string>();

            // Get the values from the text boxes for each step
            for (int i = 1; i <= stepsCount; i++)
            {
                if (stepTextBoxes.TryGetValue(i, out TextBox txtStep))
                {
                    // Step validation
                    string stepText = txtStep.Text.Trim();
                    if (string.IsNullOrEmpty(stepText) || Regex.IsMatch(stepText, @"^\d+$"))
                    {
                        MessageBox.Show($"Step {i} cannot be empty or just a number.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    steps.Add(stepText);
                }
            }

            currentRecipe.AddSteps(steps);

            Recipes.Add(currentRecipe);

            // Notify the main window that a recipe has been added and invoke the event
            RecipeAdded?.Invoke();

            Button submitStepsButton = (Button)sender;
            submitStepsButton.IsEnabled = false;

            MessageBox.Show("Steps entered successfully. Recipe added.");
            this.Close();
        }

        private void notify()
        {
            MessageBox.Show("Calories are more than 300.");
        }

    }
}
