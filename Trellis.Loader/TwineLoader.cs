using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using Trellis.Core;
using Trellis.Text;

[assembly: InternalsVisibleTo("Trellis.UnitTests")]
[assembly: InternalsVisibleTo("Trellis.Engine")]

namespace Trellis.Loader
{
    internal class TwineLoader : IStoryLoader
    {
        private const string splitMacroPattern = @"<[\w\W\s]+?>";
        private const string linkPattern = @"\[\[\s*([^|\]]+?)(?:\s*\|\s*([^\]]+))?\s*\]\]";

        private static TwineStoryDto DeserializeStoryData(string storyDataText) => 
            JsonSerializer.Deserialize<TwineStoryDto>(storyDataText) 
            ?? throw new InvalidDataException("Story deserialized to null.");

        private static Story BuildStory(TwineStoryDto twineStoryDto) =>
            new Story(
                twineStoryDto.passages?
                .Select(BuildPassage)
                .ToDictionary(p => p.Name, p => p)
                ?? throw new InvalidDataException("Could not load story data.")
            );

        private static Passage BuildPassage(TwinePassageDto passage) =>
            new Passage(
                passage.name,
                BuildPassageSteps(passage.text),
                passage.links?.Select(BuildPassageLink).ToList() 
                    ?? new List<PassageLink>()
            );

        private static string CleanPassageText(string passageText)
        {
            var processedText = passageText;
            processedText = Regex.Replace(passageText, linkPattern, "");

            return processedText;
        }

        private static PassageLink BuildPassageLink(TwineLinkDto twineLinkDto)
        {
            var linkParts = twineLinkDto.name.Split('|');
            return new PassageLink(
                linkParts.Length > 1 ? linkParts[1] : linkParts[0],
                linkParts.Length > 1 ? linkParts[0] : linkParts[0],
                twineLinkDto.broken
                );
        }

        public static List<PassageStep> BuildPassageSteps(string passageText)
        {
            List<PassageStep> passageSteps = new List<PassageStep>();
            Regex regex = new Regex(splitMacroPattern);
            int lastIndex = 0;

            foreach (Match match in regex.Matches(passageText))
            {
                // Text before macro
                if (match.Index > lastIndex)
                {
                    var chunk = passageText.Substring(lastIndex, match.Index - lastIndex);
                    if (!string.IsNullOrWhiteSpace(chunk))
                        passageSteps.Add(new PassageStep(
                            PassageStepType.display,
                            CleanPassageText(chunk)));
                }

                // Mack Rowe
                //Console.WriteLine($"Match index = {match.Index}, length: {match.Length}");
                string macroChunk = passageText.Substring(match.Index + 1, match.Length - 2);
                Console.WriteLine(macroChunk);
                var macroTokens = Tokenizer.Tokenize(macroChunk);
                var macroName = macroTokens.First();
                var macroValues = macroTokens.Skip(1).ToArray();
                var macro = new TrellisMacro(macroName, macroValues);


                passageSteps.Add(new PassageStep(
                    PassageStepType.macro,
                    macro));

                lastIndex = match.Index + match.Length;
            }
                //Trailing text
                if (lastIndex < passageText.Length)
                {
                    var tail = passageText.Substring(lastIndex);
                    if (!string.IsNullOrWhiteSpace(tail))
                        passageSteps.Add(new PassageStep(
                            PassageStepType.display,
                            CleanPassageText(tail)));
                }

            return passageSteps;
        }

        public Story LoadStoryData(string fileText) => BuildStory(DeserializeStoryData(fileText));
    }
}
