using System.Text.Json.Serialization;

namespace Trellis.Core
{
    internal class Story
    {
        private readonly IReadOnlyDictionary<string, Passage> _storyData;
        internal readonly String InitialPassage;

        internal IReadOnlyDictionary<string, Passage> StoryData { get => _storyData;}

        [JsonConstructor]
        internal Story(IReadOnlyDictionary<string, Passage> storyData)
        {
            _storyData = storyData;
            if (storyData == null)
                throw new ArgumentException("Story data cannot be null when creating a new Story");
            InitialPassage = storyData.First().Key;
        }

        internal Story(IReadOnlyDictionary<string, Passage> storyData, string initialPassage)
        {
            _storyData = storyData;

            if (!PassageExists(initialPassage))
                throw new ArgumentException("Invalid initial passage provided.");

            InitialPassage = initialPassage;
        }

        internal Passage? GetPassage(string passageName) => StoryData.GetValueOrDefault(passageName);

        internal bool PassageExists(string passageName) => StoryData.ContainsKey(passageName);

        internal List<Passage> Passages() => StoryData.Values.ToList();
    }
}
