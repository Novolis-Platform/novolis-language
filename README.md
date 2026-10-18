# novolis-language

Reusable language-tooling contracts plus the cohesive RoboSharp language implementation.

## Packages

| Package | Purpose |
|---|---|
| `Novolis.Language.Text` | Source text, spans, and locations |
| `Novolis.Language.Diagnostics` | Structured diagnostics and faults |
| `Novolis.Language.Syntax` | Tokens, syntax trees, and stage results |
| `Novolis.Language.Execution` | Stepping and runtime snapshots |
| `Novolis.Language.RoboSharp` | RoboSharp lexer, parser, binder, teaching instructions, world model, and interpreter |

The substrate is intentionally small. RoboSharp-specific syntax, builtins, and runtime behavior stay in `Novolis.Language.RoboSharp`; they are not presented as universal compiler abstractions.

See [getting started](docs/getting-started.md), [design](docs/design.md), and [release](docs/release.md).
