using TagCheckerProblem;
using Xunit;

namespace TagCheckerProblemTests
{
    public class TagCheckerTests
    {
        [Fact]
        public void IsTagCorrect_GivenNoString_ExceptDefaultResult()
        {
            var sut = new TagChecker();
            var result = sut.IsTagCorrect("");
            var getSummary = sut.GetSummary(result);
            Assert.True(result.IsCorrect);
            Assert.Equal("Correctly tagged paragraph", getSummary);
        }

        [Fact]
        public void IsTagCorrect_GivenCorrectTagging_ExpectCorrectlyTagged()
        {
            var sut = new TagChecker();
            var result = sut.IsTagCorrect("The following text<C><B>is centred and in boldface</B></C>");
            var getSummary = sut.GetSummary(result);
            Assert.True(result.IsCorrect);
            Assert.Equal("Correctly tagged paragraph", getSummary);
        }

        [Fact]
        public void IsTagCorrect_GivenComplicatedCorrectTagging_ExpectCorrectlyTagged()
        {
            var sut = new TagChecker();
            var result = sut.IsTagCorrect("<B>This <\\g>is <B>boldface</B> in <<*> a</B> <\\6> <<d>sentence");
            var getSummary = sut.GetSummary(result);
            Assert.True(result.IsCorrect);
            Assert.Equal("Correctly tagged paragraph", getSummary);
        }

        [Fact]
        public void IsTagCorrect_GivenIncorrectOrder_ExpectIncorrectlyTagged()
        {
            var sut = new TagChecker();
            var result = sut.IsTagCorrect("<B><C> This should be centred and in boldface, but thetags are wrongly nested </B></C>");
            var getSummary = sut.GetSummary(result);
            Assert.False(result.IsCorrect);
            Assert.Equal("Expected </C> found </B>", getSummary);
        }

        [Fact]
        public void IsTagCorrect_GivenIncorrectTag_MissingOpeningTag_ExpectIncorrectlyTagged()
        {
            var sut = new TagChecker();
            var result = sut.IsTagCorrect("<B>This should be in boldface, but there is an extra closingtag</B></C>");
            var getSummary = sut.GetSummary(result);
            Assert.False(result.IsCorrect);
            Assert.Equal("Expected # found </C>", getSummary);
        }

        [Fact]
        public void IsTagCorrect_GivenIncorrectTag_MissingClosingTag_ExpectIncorrectlyTagged()
        {
            var sut = new TagChecker();
            var result = sut.IsTagCorrect("<B><C>This should be centred and in boldface, but there isa missing closing tag</C>");
            var getSummary = sut.GetSummary(result);
            Assert.False(result.IsCorrect);
            Assert.Equal("Expected </B> found #", getSummary);
        }
    }
}