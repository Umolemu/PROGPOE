using System;

namespace PROGPOE
{
    public class Ingredient
    {

        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Measurement { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        public string Group {  get; set; }
        public string totalCalories { get; set; }

        public Ingredient(string name, float quantity, string measurment, string description, int calories, string group)
        {
            Name = name;
            Quantity = quantity;
            Measurement = measurment;
            Description = description;
            Calories = calories;
            Group = group;
        }
    }
}
