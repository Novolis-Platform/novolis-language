namespace Novolis.Language.Diagnostics;

/// <summary>Severity of a language diagnostic.</summary>
public enum LanguageDiagnosticSeverity
{
    /// <summary>Informational message.</summary>
    Info,

    /// <summary>Warning that permits execution.</summary>
    Warning,

    /// <summary>Error that blocks the affected stage.</summary>
    Error,
}
