namespace Novolis.Language.RoboSharp;

/// <summary>Compiled RoboSharp teaching program.</summary>
public sealed record RoboSharpProgram(IReadOnlyList<RoboInstruction> Instructions);
