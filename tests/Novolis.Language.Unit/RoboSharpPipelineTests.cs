using Novolis.Language.RoboSharp;
using Novolis.Language.Syntax;

namespace Novolis.Language.Unit;

public sealed class RoboSharpPipelineTests
{
    [Test]
    public async Task Compiler_exposes_tokens_and_teaching_instructions()
    {
        var compilation = new RoboSharpCompiler().Compile("""
            move 2
            turn right
            print "hello"
            """);

        await Assert.That(compilation.Succeeded).IsTrue();
        await Assert.That(compilation.SyntaxTree.Tokens.Any(token => token.Kind == LanguageTokenKind.Keyword)).IsTrue();
        await Assert.That(compilation.TeachingInstructions.Select(item => item.Display))
            .IsEquivalentTo(["MOVE 2", "TURN right", "PRINT hello"]);
    }

    [Test]
    public async Task Compiler_exposes_parser_and_world_artifacts()
    {
        var compilation = new RoboSharpCompiler().Compile("move 1");

        await Assert.That(compilation.ParsedSyntax).IsNotNull();
        var session = new RoboSharpExecutionSession(compilation.Program!);
        session.Step();
        await Assert.That(session.World.Y).IsEqualTo(1);
        await Assert.That(session.Snapshot.Variables["y"]).IsEqualTo(1);
    }

    [Test]
    public async Task Execution_is_deterministic_and_emits_output()
    {
        var compilation = new RoboSharpCompiler().Compile("""
            move 2
            turn right
            move 1
            print "done"
            """);
        var session = new RoboSharpExecutionSession(compilation.Program!);

        while (!session.Snapshot.IsCompleted)
        {
            session.Step();
        }

        await Assert.That(session.Snapshot.Variables["x"]).IsEqualTo(1);
        await Assert.That(session.Snapshot.Variables["y"]).IsEqualTo(2);
        await Assert.That(session.Snapshot.Output).IsEquivalentTo(["done"]);
    }

    [Test]
    public async Task Compiler_reports_unknown_commands_with_locations()
    {
        var compilation = new RoboSharpCompiler().Compile("fly 3");

        await Assert.That(compilation.Succeeded).IsFalse();
        await Assert.That(compilation.Diagnostics.Single().Code).IsEqualTo("ROB000");
        await Assert.That(compilation.Diagnostics.Single().Location!.Value.Line).IsEqualTo(0);
    }
}
