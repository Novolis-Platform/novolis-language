using Novolis.Language.Syntax;
using Novolis.Language.Text;

namespace Novolis.Language.RoboSharp;

/// <summary>Small deterministic lexer for the teaching RoboSharp language.</summary>
public static class RoboSharpLexer
{
    private static readonly HashSet<string> Keywords =
        new(StringComparer.OrdinalIgnoreCase) { "move", "turn", "print" };

    /// <summary>Tokenizes source text into inspectable tokens.</summary>
    public static IReadOnlyList<LanguageToken> Tokenize(SourceText source)
    {
        var tokens = new List<LanguageToken>();
        var text = source.Text;
        var position = 0;
        while (position < text.Length)
        {
            var current = text[position];
            if (char.IsWhiteSpace(current))
            {
                position++;
                continue;
            }

            var start = position;
            if (current == '/' && position + 1 < text.Length && text[position + 1] == '/')
            {
                while (position < text.Length && text[position] is not '\r' and not '\n')
                {
                    position++;
                }

                tokens.Add(Token(LanguageTokenKind.Comment, text[start..position], start, source));
                continue;
            }

            if (current == '"')
            {
                position++;
                while (position < text.Length && text[position] != '"')
                {
                    position += text[position] == '\\' && position + 1 < text.Length ? 2 : 1;
                }

                if (position < text.Length)
                {
                    position++;
                }

                var raw = text[start..position];
                tokens.Add(Token(LanguageTokenKind.String, raw, start, source, Unquote(raw)));
                continue;
            }

            if (char.IsDigit(current))
            {
                while (position < text.Length && char.IsDigit(text[position]))
                {
                    position++;
                }

                var raw = text[start..position];
                tokens.Add(Token(LanguageTokenKind.Number, raw, start, source, int.Parse(raw)));
                continue;
            }

            if (char.IsLetter(current) || current == '_')
            {
                position++;
                while (position < text.Length && (char.IsLetterOrDigit(text[position]) || text[position] == '_'))
                {
                    position++;
                }

                var raw = text[start..position];
                tokens.Add(Token(
                    Keywords.Contains(raw) ? LanguageTokenKind.Keyword : LanguageTokenKind.Identifier,
                    raw,
                    start,
                    source));
                continue;
            }

            var kind = current is '(' or ')' or ',' ? LanguageTokenKind.Punctuation : LanguageTokenKind.Operator;
            position++;
            tokens.Add(Token(kind, text[start..position], start, source));
        }

        tokens.Add(Token(LanguageTokenKind.EndOfFile, string.Empty, text.Length, source));
        return tokens;
    }

    private static LanguageToken Token(
        LanguageTokenKind kind,
        string text,
        int start,
        SourceText source,
        object? value = null) =>
        new(kind, text, TextLocation.Create(source, start, text.Length), value);

    private static string Unquote(string raw) =>
        raw.Length >= 2 ? raw[1..^1].Replace("\\\"", "\"", StringComparison.Ordinal) : raw;
}
