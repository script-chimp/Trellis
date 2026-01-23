using Trellis.Core;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Trellis.UnitTests")]
[assembly: InternalsVisibleTo("Trellis.Engine")]


namespace Trellis.Loader
{
    internal class StoryLoader
    {
        private static string LoadFileText(string path) => File.ReadAllText(path);
        internal static Story LoadStoryData(string storyDataPath, string storyDataType) =>

            storyDataType switch
            {
                "Twine" => new TwineLoader().LoadStoryData(LoadFileText(storyDataPath)),
                _ => throw new ArgumentException("Invalid story data type.")
            };
    }
}
