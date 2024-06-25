using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;

namespace Recipe_Managment
{
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow(List<Recipe> selectedRecipes)
        {
            InitializeComponent();

            // Process the selected recipes to gather food group data
            var foodGroupData = selectedRecipes
                .SelectMany(r => r.GetIngredients())
                .GroupBy(i => i.Group)
                .Select(g => new { Group = g.Key, Count = g.Count() })
                .ToList();

            // Prepare the data for the pie chart
            SeriesCollection series = new SeriesCollection();
            foreach (var group in foodGroupData)
            {
                series.Add(new PieSeries
                {
                    Title = group.Group,
                    Values = new ChartValues<int> { group.Count },
                    DataLabels = true
                });
            }

            // Assign the data to the pie chart
            pieChart.Series = series;
        }
    }
}
