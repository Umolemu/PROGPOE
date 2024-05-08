using System;

namespace PROGPOE
{
    public class Ingredient
    {

        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Measurement { get; set; }
        public string Description { get; set; }
        public float Calories { get; set; }
        public string Group {  get; set; }
        public float OriginalQuantity { get; set; }

        public Ingredient(string name, float quantity, string measurment, string description, float calories, string group, float originalQuantity)
        {
            Name = name;
            Quantity = quantity;
            Measurement = measurment;
            Description = description;
            Calories = calories;
            Group = group;
            OriginalQuantity = originalQuantity;
        }
    }
}
