---
name: C# Code Review | Fixed Model | Slug
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
  - seesharprun/test-plugin-marketplace/plugins/dotnet-code-reviewer@ed0af45f23125baa8022cc09d05c41435c9b4087
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
