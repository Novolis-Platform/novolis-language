using Novolis.Language.Text;

namespace Novolis.Language.Diagnostics;

/// <summary>Structured diagnostic produced by a language stage.</summary>
public sealed record LanguageDiagnostic(
    string Code,
    string Message,
    LanguageDiagnosticSeverity Severity,
    TextLocation? Location = null)
{
    /// <summary>Creates an error diagnostic.</summary>
    public static LanguageDiagnostic Error(
        string code,
        string message,
        TextLocation? location = null) =>
        new(code, message, LanguageDiagnosticSeverity.Error, location);
}
