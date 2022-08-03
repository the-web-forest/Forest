namespace Ipe.Domain.Models
{
    public class TreeByFilter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Value { get; set; }
        public string Biome { get; set; }

        public TreeByFilter(string name, string description, string image, double value, string biome)
        {
            Name = name;
            Description = description;
            Image = image;
            Value = value;
            Biome = biome;
        }
    }
}
