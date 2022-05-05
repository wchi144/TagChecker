using System.Text.RegularExpressions;
using TagCheckerProblem.Model;

namespace TagCheckerProblem
{
    public interface ITagChecker {
        public TagCheckerResult IsTagCorrect(string input);
        public string GetSummary(TagCheckerResult result);
    }

    public class TagChecker : ITagChecker
    {
        private readonly string CorrectTagSummary = "Correctly tagged paragraph";
        //private readonly string IncorrectTagSummaryTemplate = "Expected {0} found {1}";

        public TagCheckerResult IsTagCorrect(string intput)
        {
            var openingTagsRegex = new Regex(@"<([A-Z])>");
            var openingTags = openingTagsRegex.Matches(intput).Reverse().ToList();

            var closingTagsRegex = new Regex(@"</([A-Z])>");
            var closingTags = closingTagsRegex.Matches(intput).ToList();

            if(openingTags.Count == closingTags.Count || openingTags.Count > closingTags.Count)
            {
                return CheckTagOrder(openingTags, closingTags);
            }

            return CheckTagOrder(closingTags, openingTags);
        }

        public TagCheckerResult CheckTagOrder(IList<Match> tagsA, IList<Match> tagsB)
        {
            var expectedTag = "";
            var actualTag = "";
            var isCorrectTag = true;

            for (var i = 0; i < tagsA?.Count; i++)
            {
                var openingTag = tagsA[i]?.Groups[1]?.Value;
                string? closingTag;

                try
                {
                    //array.ElementAtOrDefault(index) != null;
                    closingTag = tagsB[i]?.Groups[1]?.Value;
                }
                catch (ArgumentOutOfRangeException)
                {
                    closingTag = null;
                }
                

                if (openingTag == closingTag)
                {
                    continue;
                }
                else
                {
                    isCorrectTag = false;
                    expectedTag = openingTag == null ? "#" : $"</{openingTag}>";
                    actualTag = closingTag == null ? "#" : $"</{closingTag}>";
                    break;
                }
            }

            return new TagCheckerResult
            {
                IsCorrect = isCorrectTag,
                ExpectedTag = expectedTag,
                ActualTag = actualTag,
            };
        }

        public string GetSummary(TagCheckerResult result)
        {
            if (result.IsCorrect)
            {
                return CorrectTagSummary;
            } else
            {
                return $"Expected {result.ExpectedTag} found {result.ActualTag}";
            }
        }
    }
}
