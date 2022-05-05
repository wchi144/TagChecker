namespace TagCheckerProblem.Model
{
    public class TagCheckerResult
    {
        public bool IsCorrect { get; set; }
        public string? ExpectedTag { get; set; }
        public string? ActualTag { get; set; }
    }
}
