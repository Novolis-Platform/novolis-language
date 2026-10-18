namespace Novolis.Language.Diagnostics;

/// <summary>Mutable collector used while a pipeline stage is running.</summary>
public sealed class DiagnosticBag
{
    private readonly List<LanguageDiagnostic> _items = [];

    /// <summary>Collected diagnostics.</summary>
    public IReadOnlyList<LanguageDiagnostic> Items => _items;

    /// <summary>Whether at least one error was reported.</summary>
    public bool HasErrors => _items.Any(item => item.Severity == LanguageDiagnosticSeverity.Error);

    /// <summary>Adds a diagnostic.</summary>
    public void Add(LanguageDiagnostic diagnostic) => _items.Add(diagnostic);

    /// <summary>Adds an error diagnostic.</summary>
    public void Error(string code, string message, Novolis.Language.Text.TextLocation? location = null) =>
        Add(LanguageDiagnostic.Error(code, message, location));
}
