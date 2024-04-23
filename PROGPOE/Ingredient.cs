using System;

namespace PROGPOE
{
    public class Ingredient
    {

        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Measurement { get; set; }
        public string Description { get; set; }

        public Ingredient(string name, int quantity, string measurment, string description)
        {
            Name = name;
            Quantity = quantity;
            Measurement = measurment;
            Description = description;
        }
    }
}
