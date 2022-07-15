using Samauma.Domain.Models;

namespace Samauma.UseCases.ListTrees.DTOS
{
    public class LightTree
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Value { get; set; }
        public string Biome { get; set; }

        internal static LightTree FromTree(Tree tree)
        {
            return new LightTree
            {
                Name = tree.Name,
                Description = tree.Description,
                Value = tree.Value,
                Biome = tree.Biome
            };
        }
    }
}