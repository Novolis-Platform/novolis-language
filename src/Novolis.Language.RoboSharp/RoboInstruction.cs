using Novolis.Language.Text;

namespace Novolis.Language.RoboSharp;

/// <summary>Inspectable RoboSharp teaching instruction.</summary>
public sealed record RoboInstruction(
    RoboInstructionKind Kind,
    string Operand,
    TextLocation Location)
{
    /// <summary>Human-readable teaching IL form.</summary>
    public string Display => $"{Kind.ToString().ToUpperInvariant()} {Operand}";
}
