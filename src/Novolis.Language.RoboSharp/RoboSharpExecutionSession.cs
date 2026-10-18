using Novolis.Language.Diagnostics;
using Novolis.Language.Execution;

namespace Novolis.Language.RoboSharp;

/// <summary>Deterministic, steppable RoboSharp interpreter session.</summary>
public sealed class RoboSharpExecutionSession : IExecutionSession
{
    private readonly RoboSharpProgram _program;
    private int _instructionPointer;
    private bool _completed;

    /// <summary>Creates a session for a compiled program.</summary>
    public RoboSharpExecutionSession(RoboSharpProgram program)
    {
        _program = program ?? throw new ArgumentNullException(nameof(program));
    }

    /// <summary>Mutable world state updated by this session.</summary>
    public RoboSharpWorld World { get; } = new();

    /// <inheritdoc />
    public ExecutionSnapshot Snapshot => CreateSnapshot();

    /// <inheritdoc />
    public ExecutionStep Step()
    {
        if (_completed || _instructionPointer >= _program.Instructions.Count)
        {
            _completed = true;
            return new ExecutionStep(ExecutionStepKind.Completed, CreateSnapshot(), "Program completed.");
        }

        var instruction = _program.Instructions[_instructionPointer++];
        switch (instruction.Kind)
        {
            case RoboInstructionKind.Move:
                World.Move(int.Parse(instruction.Operand));
                break;
            case RoboInstructionKind.Turn:
                World.Turn(instruction.Operand);
                break;
            case RoboInstructionKind.Print:
                World.Print(instruction.Operand);
                break;
            default:
                var diagnostic = LanguageDiagnostic.Error("ROB100", "Unknown runtime instruction.", instruction.Location);
                return new ExecutionStep(
                    ExecutionStepKind.Faulted,
                    CreateSnapshot(),
                    "Runtime fault.",
                    diagnostic,
                    instruction.Location);
        }

        var kind = instruction.Kind == RoboInstructionKind.Print
            ? ExecutionStepKind.Output
            : ExecutionStepKind.Instruction;
        return new ExecutionStep(kind, CreateSnapshot(), instruction.Display, Location: instruction.Location);
    }

    private ExecutionSnapshot CreateSnapshot() =>
        new(
            _instructionPointer,
            World.Variables,
            World.Output,
            _completed || _instructionPointer >= _program.Instructions.Count,
            false);
}
