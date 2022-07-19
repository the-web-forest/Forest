namespace Samauma.UseCases
{
    public class UpdateTreeUseCaseInput 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public string Biome { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
