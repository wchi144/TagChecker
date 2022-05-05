using System.Text.RegularExpressions;
using TagCheckerProblem.Model;

namespace TagCheckerProblem
{
    public class TagChecker : ITagChecker
    {
        private readonly string CorrectTagSummary = "Correctly tagged paragraph";

        public TagCheckerResult IsTagCorrect(string intput)
        {
            var openingTagsRegex = new Regex(@"<([A-Z])>");
            var openingTags = openingTagsRegex.Matches(intput).Reverse().ToList();

            var closingTagsRegex = new Regex(@"</([A-Z])>");
            var closingTags = closingTagsRegex.Matches(intput).ToList();

            string? expectedTag = null;
            string? actualTag = null;
            var isCorrectTag = true;
            
            int iterationCount;

            if (openingTags.Count >= closingTags.Count)
            {
                iterationCount = openingTags.Count;
            } else
            {
                iterationCount = closingTags.Count;
            }

            for (var i = 0; i < iterationCount; i++)
            {
                var openingTag = openingTags.ElementAtOrDefault(i)?.Groups[1]?.Value;
                var closingTag = closingTags.ElementAtOrDefault(i)?.Groups[1]?.Value;

                if (openingTag == closingTag)
                {
                    continue;
                }
                else
                {
                    isCorrectTag = false;
                    expectedTag = string.IsNullOrEmpty(openingTag) ? "#" : $"</{openingTag}>";
                    actualTag = string.IsNullOrEmpty(closingTag) ? "#" : $"</{closingTag}>";
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
