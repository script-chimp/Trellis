using System.Text.RegularExpressions;

namespace Trellis.Text;
public static class Tokenizer
{
    private const string tokenPattern = @"(""[^""]*""|\S+)";
    public static string[] Tokenize(string text) => 
        Regex.Matches(text, tokenPattern)
        .Select(m  => m.Value)
        .ToArray();
}
