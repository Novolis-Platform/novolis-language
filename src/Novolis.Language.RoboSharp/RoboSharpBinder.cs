using Novolis.Language.Diagnostics;
using Novolis.Language.Syntax;

namespace Novolis.Language.RoboSharp;

/// <summary>Binds parsed RoboSharp statements into executable teaching instructions.</summary>
public sealed class RoboSharpBinder
{
    /// <summary>Applies RoboSharp command semantics to a parsed syntax tree.</summary>
    public CompilationStageResult<RoboSharpProgram> Bind(RoboSharpSyntaxTree syntaxTree)
    {
        ArgumentNullException.ThrowIfNull(syntaxTree);

        var diagnostics = new DiagnosticBag();
        foreach (var diagnostic in syntaxTree.Diagnostics)
        {
            diagnostics.Add(diagnostic);
        }

        var instructions = new List<RoboInstruction>();
        foreach (var statement in syntaxTree.Statements)
        {
            switch (statement.Command.ToLowerInvariant())
            {
                case "move" when int.TryParse(statement.Operand, out var distance) && distance >= 0:
                    instructions.Add(new RoboInstruction(RoboInstructionKind.Move, distance.ToString(), statement.Location));
                    break;
                case "turn" when statement.Operand.Equals("left", StringComparison.OrdinalIgnoreCase)
                    || statement.Operand.Equals("right", StringComparison.OrdinalIgnoreCase):
                    instructions.Add(new RoboInstruction(
                        RoboInstructionKind.Turn,
                        statement.Operand.ToLowerInvariant(),
                        statement.Location));
                    break;
                case "print" when statement.Operand.Length >= 2
                    && statement.Operand[0] == '"'
                    && statement.Operand[^1] == '"':
                    instructions.Add(new RoboInstruction(
                        RoboInstructionKind.Print,
                        statement.Operand[1..^1].Replace("\\\"", "\"", StringComparison.Ordinal),
                        statement.Location));
                    break;
                case "move":
                    diagnostics.Error(
                        "ROB001",
                        "move expects a non-negative integer distance.",
                        statement.Location);
                    break;
                case "turn":
                    diagnostics.Error("ROB002", "turn expects left or right.", statement.Location);
                    break;
                case "print":
                    diagnostics.Error("ROB003", "print expects a quoted string.", statement.Location);
                    break;
                default:
                    diagnostics.Error(
                        "ROB000",
                        $"Unknown RoboSharp command '{statement.Command}'.",
                        statement.Location);
                    break;
            }
        }

        var program = diagnostics.HasErrors ? null : new RoboSharpProgram(instructions);
        return new CompilationStageResult<RoboSharpProgram>(program, diagnostics.Items);
    }
}
