using Novolis.Language.Diagnostics;
using Novolis.Language.Text;

namespace Novolis.Language.Syntax;

/// <summary>Inspectable syntax tree result from a parser.</summary>
public sealed record SyntaxTree(
    SourceText Source,
    IReadOnlyList<LanguageToken> Tokens,
    object? Root,
    IReadOnlyList<LanguageDiagnostic> Diagnostics)
{
    /// <summary>Whether parsing produced no errors.</summary>
    public bool IsValid => Diagnostics.All(item => item.Severity != LanguageDiagnosticSeverity.Error);
}
