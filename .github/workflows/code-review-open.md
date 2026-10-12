---
name: C# Code Review | Open Model
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
  - seesharprun/example-plugin-marketplace/plugins/dotnet-code-reviewer@65ea67a1cb9c7c1add2659238361df0f78c06870
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
