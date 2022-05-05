using TagCheckerProblem.Model;

namespace TagCheckerProblem
{
    public interface ITagChecker {
        public TagCheckerResult IsTagCorrect(string input);
        public string GetSummary(TagCheckerResult result);
    }
}
