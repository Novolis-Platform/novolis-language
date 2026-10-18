using Novolis.Language.Diagnostics;
using Novolis.Language.Text;

namespace Novolis.Language.RoboSharp;

/// <summary>RoboSharp parser output kept separate from semantic binding.</summary>
public sealed record RoboSharpSyntaxTree(
    SourceText Source,
    IReadOnlyList<Novolis.Language.Syntax.LanguageToken> Tokens,
    IReadOnlyList<RoboSharpSyntaxStatement> Statements,
    IReadOnlyList<LanguageDiagnostic> Diagnostics)
{
    /// <summary>Whether parsing produced no errors.</summary>
    public bool IsValid => Diagnostics.All(item => item.Severity != LanguageDiagnosticSeverity.Error);
}
