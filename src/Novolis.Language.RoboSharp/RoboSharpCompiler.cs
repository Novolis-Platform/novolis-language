using Novolis.Language.Diagnostics;
using Novolis.Language.Syntax;
using Novolis.Language.Text;

namespace Novolis.Language.RoboSharp;

/// <summary>Compiles RoboSharp source into inspectable teaching instructions.</summary>
public sealed class RoboSharpCompiler
{
    private readonly RoboSharpParser parser = new();
    private readonly RoboSharpBinder binder = new();

    /// <summary>Compiles source text through lexing and command binding.</summary>
    public RoboSharpCompilation Compile(string source) => Compile(new SourceText(source));

    /// <summary>Compiles source text through lexing and command binding.</summary>
    public RoboSharpCompilation Compile(SourceText source)
    {
        ArgumentNullException.ThrowIfNull(source);

        var parsed = parser.Parse(source);
        var syntax = parsed.Value!;
        var bound = parsed.Succeeded && syntax is not null
            ? binder.Bind(syntax)
            : new CompilationStageResult<RoboSharpProgram>(null, parsed.Diagnostics);
        var diagnostics = parsed.Diagnostics.Concat(bound.Diagnostics).ToArray();
        var program = bound.Value;
        var instructions = program?.Instructions ?? [];
        var syntaxTree = new SyntaxTree(source, syntax?.Tokens ?? RoboSharpLexer.Tokenize(source), program, diagnostics);
        return new RoboSharpCompilation(syntaxTree, program, instructions, diagnostics)
        {
            ParsedSyntax = syntax,
        };
    }
}
