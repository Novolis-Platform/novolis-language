using Novolis.Language.Diagnostics;
using Novolis.Language.Syntax;
using Novolis.Language.Text;

namespace Novolis.Language.RoboSharp;

/// <summary>Parses RoboSharp source into a syntax-stage artifact.</summary>
public sealed class RoboSharpParser
{
    /// <summary>Parses source without applying command semantics.</summary>
    public CompilationStageResult<RoboSharpSyntaxTree> Parse(SourceText source)
    {
        ArgumentNullException.ThrowIfNull(source);

        var tokens = RoboSharpLexer.Tokenize(source);
        var statements = new List<RoboSharpSyntaxStatement>();
        var diagnostics = new DiagnosticBag();
        var lineStart = 0;

        foreach (var line in source.Text.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n'))
        {
            var trimmed = line.Trim();
            var leading = line.Length - line.TrimStart().Length;
            if (trimmed.Length == 0 || trimmed.StartsWith("//", StringComparison.Ordinal))
            {
                lineStart += line.Length + 1;
                continue;
            }

            var location = TextLocation.Create(source, lineStart + leading, trimmed.Length);
            var separator = trimmed.IndexOf(' ');
            var command = separator < 0 ? trimmed : trimmed[..separator];
            var operand = separator < 0 ? string.Empty : trimmed[(separator + 1)..].Trim();
            statements.Add(new RoboSharpSyntaxStatement(
                command,
                operand,
                location,
                tokens.Where(token => token.Location.Span.Start >= location.Span.Start
                    && token.Location.Span.Start < location.Span.Start + location.Span.Length).ToArray()));
            lineStart += line.Length + 1;
        }

        var tree = new RoboSharpSyntaxTree(source, tokens, statements, diagnostics.Items);
        return new CompilationStageResult<RoboSharpSyntaxTree>(tree, diagnostics.Items);
    }
}
