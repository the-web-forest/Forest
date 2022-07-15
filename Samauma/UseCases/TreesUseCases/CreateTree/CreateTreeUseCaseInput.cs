namespace Samauma.UseCases.CreateTree
{
    public class CreateTreeUseCaseInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public string Biome { get; set; }
        public string Base64Image { get; set; }
    }
}
