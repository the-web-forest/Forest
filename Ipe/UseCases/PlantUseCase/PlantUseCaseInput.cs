namespace Ipe.UseCases.PlantUseCase.CreatePlant
{
    public class PlantUseCaseInput
    {
        public string UserId { get; set; }
        public string CardToken { get; set; }
        public List<TreeUseCaseInput> Trees { get; set; }
    }
}
