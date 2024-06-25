using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Recipe_Managment
{
    public partial class EditRecipe : Window
    {
        private Recipe recipe;

        // Dictionaries to hold text boxes for each ingredient and step with their respective index
        private Dictionary<int, TextBox> ingredientNameTextBoxes = new Dictionary<int, TextBox>();
        private Dictionary<int, TextBox> quantityTextBoxes = new Dictionary<int, TextBox>();
        private Dictionary<int, TextBox> measurementTextBoxes = new Dictionary<int, TextBox>();
        private Dictionary<int, TextBox> caloriesTextBoxes = new Dictionary<int, TextBox>();
        private Dictionary<int, TextBox> groupTextBoxes = new Dictionary<int, TextBox>();
        private Dictionary<int, TextBox> stepTextBoxes = new Dictionary<int, TextBox>();

        public EditRecipe(Recipe recipe)
        {
            InitializeComponent();
            this.recipe = recipe;
            LoadRecipeData();
            ToggleEditing(false);
        }

        private void LoadRecipeData()
        {
            // Set recipe name to the current recipe's name
            txtRecipeName.Text = recipe.Name;

            // Clear stack panels to avoid duplicates
            stackPanelIngredients.Children.Clear();
            stackPanelSteps.Children.Clear();

            // Dynamically add ingredient and step fields to the screen
            int ingredientIndex = 1;
            foreach (var ingredient in recipe.GetIngredients())
            {
                AddIngredientField(ingredientIndex, ingredient);
                ingredientIndex++;
            }

            int stepIndex = 1;
            foreach (var step in recipe.GetSteps())
            {
                AddStepField(stepIndex, step);
                stepIndex++;
            }
        }

        private void AddIngredientField(int index, Ingredient ingredient)
        {
            // Create stack panel to hold text boxes for each ingredient
            StackPanel ingredientStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 5, 0, 5)
            };

            // Ingredient name textbox
            TextBox txtIngredientName = new TextBox
            {
                Width = 150,
                Margin = new Thickness(0, 0, 10, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Text = ingredient.Name
            };
            ingredientNameTextBoxes.Add(index, txtIngredientName);

            // Quantity textbox
            TextBox txtQuantity = new TextBox
            {
                Width = 50,
                Margin = new Thickness(0, 0, 10, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Text = ingredient.Quantity.ToString()
            };
            quantityTextBoxes.Add(index, txtQuantity);

            // Measurement textbox
            TextBox txtMeasurement = new TextBox
            {
                Width = 100,
                Margin = new Thickness(0, 0, 10, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Text = ingredient.Measurement
            };
            measurementTextBoxes.Add(index, txtMeasurement);

            // Calories textbox
            TextBox txtCalories = new TextBox
            {
                Width = 50,
                Margin = new Thickness(0, 0, 10, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Text = ingredient.Calories.ToString()
            };
            caloriesTextBoxes.Add(index, txtCalories);

            // Food group textbox
            TextBox txtGroup = new TextBox
            {
                Width = 125,
                Margin = new Thickness(0, 0, 10, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Text = ingredient.Group
            };
            groupTextBoxes.Add(index, txtGroup);

            // Add labels and text boxes to stack panel
            ingredientStackPanel.Children.Add(new Label { Content = $"Ingredient {index}:", VerticalAlignment = VerticalAlignment.Center });
            ingredientStackPanel.Children.Add(txtIngredientName);
            ingredientStackPanel.Children.Add(new Label { Content = "Quantity:", VerticalAlignment = VerticalAlignment.Center });
            ingredientStackPanel.Children.Add(txtQuantity);
            ingredientStackPanel.Children.Add(new Label { Content = "Measurement:", VerticalAlignment = VerticalAlignment.Center });
            ingredientStackPanel.Children.Add(txtMeasurement);
            ingredientStackPanel.Children.Add(new Label { Content = "Calories:", VerticalAlignment = VerticalAlignment.Center });
            ingredientStackPanel.Children.Add(txtCalories);
            ingredientStackPanel.Children.Add(new Label { Content = "Food Group:", VerticalAlignment = VerticalAlignment.Center });
            ingredientStackPanel.Children.Add(txtGroup);

            stackPanelIngredients.Children.Add(ingredientStackPanel);
        }

        private void AddStepField(int index, string step)
        {
            // Create stack panel to hold text boxes for each step
            StackPanel stepStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 5, 0, 5)
            };

            // Step textbox
            TextBox txtStep = new TextBox
            {
                Width = 400,
                Margin = new Thickness(0, 0, 10, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Text = step
            };
            stepTextBoxes.Add(index, txtStep);

            // Add labels and text boxes to stack panel
            stepStackPanel.Children.Add(new Label { Content = $"Step {index}:", VerticalAlignment = VerticalAlignment.Center });
            stepStackPanel.Children.Add(txtStep);

            stackPanelSteps.Children.Add(stepStackPanel);
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Enable all text boxes for editing
            ToggleEditing(true);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Reset text boxes to original values
            foreach (var kvp in quantityTextBoxes)
            {
                int index = kvp.Key;
                if (recipe.GetIngredients().Count >= index)
                {
                    kvp.Value.Text = recipe.GetIngredients()[index - 1].OriginalQuantity.ToString();
                }
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate recipe name
            string recipeName = txtRecipeName.Text.Trim();
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

            // Validate ingredients
            foreach (var kvp in ingredientNameTextBoxes)
            {
                int index = kvp.Key;
                TextBox txtIngredientName = kvp.Value;
                //Ingredient Name validation 
                if (string.IsNullOrEmpty(txtIngredientName.Text.Trim()))
                {
                    MessageBox.Show($"Please enter ingredient name for Ingredient {index}.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (Regex.IsMatch(txtIngredientName.Text.Trim(), @"^\d+$"))
                {
                    MessageBox.Show($"Ingredient name for Ingredient {index} cannot be just a number.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                //Quantity validation
                TextBox txtQuantity = quantityTextBoxes[index];
                if (!float.TryParse(txtQuantity.Text.Trim(), out float quantity) || quantity < 1 || quantity > 500)
                {
                    MessageBox.Show($"Please enter a valid quantity between 1 and 500 for Ingredient {index}.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                //Measurement validation
                TextBox txtMeasurement = measurementTextBoxes[index];
                if (string.IsNullOrEmpty(txtMeasurement.Text.Trim()))
                {
                    MessageBox.Show($"Please enter measurement for Ingredient {index}.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (Regex.IsMatch(txtMeasurement.Text.Trim(), @"^\d+$"))
                {
                    MessageBox.Show($"Measurement for Ingredient {index} cannot be just a number.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                //Calories validation
                TextBox txtCalories = caloriesTextBoxes[index];
                if (!float.TryParse(txtCalories.Text.Trim(), out float calories) || calories < 0 || calories > 2000)
                {
                    MessageBox.Show($"Please enter valid calories between 0 and 2000 for Ingredient {index}.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            // Validate steps
            foreach (var kvp in stepTextBoxes)
            {
                int index = kvp.Key;
                TextBox txtStep = kvp.Value;
                if (string.IsNullOrEmpty(txtStep.Text.Trim()))
                {
                    MessageBox.Show($"Please enter step {index}.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            // Apply changes to the recipe
            recipe.Name = recipeName;

            for (int i = 1; i <= ingredientNameTextBoxes.Count; i++)
            {
                var ingredient = recipe.GetIngredients()[i - 1];
                ingredient.Name = ingredientNameTextBoxes[i].Text;
                ingredient.Quantity = float.Parse(quantityTextBoxes[i].Text);
                ingredient.Measurement = measurementTextBoxes[i].Text;
                ingredient.Calories = float.Parse(caloriesTextBoxes[i].Text);
                ingredient.Group = groupTextBoxes[i].Text;
            }

            recipe.GetSteps().Clear();
            for (int i = 1; i <= stepTextBoxes.Count; i++)
            {
                recipe.GetSteps().Add(stepTextBoxes[i].Text);
            }

            // Save changes and disable text boxes
            ToggleEditing(false);
        }

        private void ToggleEditing(bool isEditing)
        {
            // Enable or disable text boxes based on isEditing parameter
            txtRecipeName.IsEnabled = isEditing;

            foreach (var textBox in ingredientNameTextBoxes.Values)
            {
                textBox.IsEnabled = isEditing;
            }

            foreach (var textBox in quantityTextBoxes.Values)
            {
                textBox.IsEnabled = isEditing;
            }

            foreach (var textBox in measurementTextBoxes.Values)
            {
                textBox.IsEnabled = isEditing;
            }

            foreach (var textBox in caloriesTextBoxes.Values)
            {
                textBox.IsEnabled = isEditing;
            }

            foreach (var textBox in groupTextBoxes.Values)
            {
                textBox.IsEnabled = isEditing;
            }

            foreach (var textBox in stepTextBoxes.Values)
            {
                textBox.IsEnabled = isEditing;
            }

            editButton.IsEnabled = !isEditing;
            submitButton.IsEnabled = isEditing;
            resetButton.IsEnabled = isEditing;
        }
    }
}
