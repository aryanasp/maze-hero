namespace Ai.GeneticAlgorithm
{
    public interface IAiGaLogic
    {
        public void Enable();
        public void Disable();
        public AiType GetAiType();
    }
}