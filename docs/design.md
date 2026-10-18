# Design

The language family is split by reusable capability:

```text
SourceText → lexer → parser artifact → binder → teaching IL → execution → world snapshots
```

`Text`, `Diagnostics`, `Syntax`, and `Execution` define small host-neutral contracts. `RoboSharp` owns its lexer, parser, binder, command grammar, teaching instruction set, world model, and interpreter. Future languages can reuse the substrate without inheriting RoboSharp's world model or teaching vocabulary.
