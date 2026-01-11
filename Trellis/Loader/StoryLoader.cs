using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trellis.Core;

namespace Trellis.Loader
{
    internal class StoryLoader
    {
        private static string LoadFileText(string path) => File.ReadAllText(path);
        public static Story LoadStoryData(string storyDataPath, string storyDataType) =>

            storyDataType switch
            {
                "Twine" => new TwineLoader().LoadStoryData(LoadFileText(storyDataPath)),
                _ => throw new ArgumentException("Invalid story data type.")
            };
    }
}
