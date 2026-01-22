using System.Text.Json.Serialization;

namespace Trellis.Core
{
    /// <summary>
    /// Represents a collection of passages that make up a story, providing access to passages by name and tracking the
    /// initial passage.
    /// </summary>
    /// <remarks>The Story class is intended for public use to manage and retrieve passages within a
    /// narrative structure. It allows querying for the existence of passages and retrieving all available passages. The
    /// initial passage is determined by the first entry in the provided story data unless explicitly
    /// specified.</remarks>
    public class Story
    {
        private readonly IReadOnlyDictionary<string, Passage> _storyData;
        public readonly String InitialPassage;
        public IReadOnlyDictionary<string, Passage> StoryData { get => _storyData;}

        /// <summary>
        /// Constructs a new Story instance using the provided story data, setting the initial passage to the first entry.
        /// </summary>
        /// <param name="storyData">Loaded story data</param>
        /// <exception cref="ArgumentException">Invalid story data</exception>
        [JsonConstructor]
        public Story(IReadOnlyDictionary<string, Passage> storyData)
        {
            _storyData = storyData;
            if (storyData == null)
                throw new ArgumentException("Story data cannot be null when creating a new Story");
            InitialPassage = storyData.First().Key;
        }

        /// <summary>
        /// Constructs a new Story instance using the provided story data and initial passage name.
        /// </summary>
        /// <param name="storyData">Loaded story data</param>
        /// <param name="initialPassage">Entry passage</param>
        /// <exception cref="ArgumentException">Invalid story data</exception>
        public Story(IReadOnlyDictionary<string, Passage> storyData, string initialPassage)
        {
            _storyData = storyData;

            if (!PassageExists(initialPassage))
                throw new ArgumentException("Invalid initial passage provided.");

            InitialPassage = initialPassage;
        }

        /// <summary>
        /// Gets a passage by name.
        /// </summary>
        /// <param name="passageName">Name of passage</param>
        /// <returns>Passage</returns>
        public Passage? GetPassage(string passageName) => StoryData.GetValueOrDefault(passageName);

        /// <summary>
        /// Gets whether a passage exists by name.
        /// </summary>
        /// <param name="passageName">Passage name</param>
        /// <returns>Whether passage exists</returns>
        public bool PassageExists(string passageName) => StoryData.ContainsKey(passageName);

        /// <summary>
        /// Gets all passages in the story.
        /// </summary>
        /// <returns>All loaded passages</returns>
        public List<Passage> Passages() => StoryData.Values.ToList();
    }
}
