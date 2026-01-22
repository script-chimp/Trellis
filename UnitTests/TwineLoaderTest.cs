using Trellis.Loader;
using Trellis.Engine;
using NUnit.Framework;

namespace UnitTests
{
    internal class TwineLoaderTest
    {
        [Test]
        public void TestTest()
        {
            Assert.That(true);
        }

        [Test]
        public void TestLoadTwineStory_Success()
        {
            // Arrange
            string storyPath = Path.Combine(AppContext.BaseDirectory, "TestData", "test-story.json");
            //throw new Exception(storyPath);
            string storyType = "Twine";
            // Act
            var storyData = new TrellisEngine(storyPath, storyType);
            // Assert
            Assert.That(storyData != null, "Story data should not be null");
            Assert.That(storyData.GetCurrentPassage() != null, "Current passage should not be null");
            Assert.That(storyData.GetCurrentPassage().Name == "Start", "Initial passage should be 'Start'");
            Assert.That(storyData.GetCurrentPassage().Steps.Count == 1, "Initial passage should have 1 step");
        }

    }
}
