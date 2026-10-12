---
name: C# Code Review | Fixed Model | Display Name
on:
  workflow_dispatch:
permissions:
  contents: read
  copilot-requests: write
engine:
  id: copilot
  model: gpt-5.6-luna
  agent: dotnet-code-reviewer:csharp-code-reviewer
plugins:
  - seesharprun/demo-plugin-marketplace/plugins/dotnet-code-reviewer@f0cc6f8b61c10b2fad3647da471360ccdfb4a98b
network:
  allowed:
    - defaults
    - dotnet
safe-outputs:
  create-pull-request:
    title-prefix: "[CODE REVIEW] "
---

# Review C# source code

Review the source code in `src/script.cs` and generate a pull request with the changes you recommend.
