#:package Humanizer@3.0.10
#:package Spectre.Console@0.57.2
#:package Spectre.Console.Cli@0.57.2

#:property PublishAot=false

using Humanizer;
using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

CommandApp<PhoneticCommand> app = new();

await app.RunAsync(args);

internal sealed class PhoneticSettings : CommandSettings
{
    [CommandArgument(0, "<WORD>")]
    [Description("The word to convert.")]
    public string Word { get; init; } = string.Empty;

    public override ValidationResult Validate() =>
        Word.All(character => character is >= 'A' and <= 'Z' or >= 'a' and <= 'z')
            ? ValidationResult.Success()
            : ValidationResult.Error("WORD must contain only English letters.");
}

internal sealed class PhoneticCommand : AsyncCommand<PhoneticSettings>
{
    public override async Task<int> ExecuteAsync(
        CommandContext context,
        PhoneticSettings settings,
        CancellationToken cancellationToken)
    {
        List<string> phoneticWords = ConvertToNato(settings.Word);

        string title = settings.Word.Transform(To.TitleCase);

        Tree tree = new($"[bold red]{Markup.Escape(title)}[/]");

        Panel panel = new Panel(tree)
            .Header("NATO Phonetic Alphabet")
            .BorderColor(Color.Cyan)
            .RoundedBorder();

        await AnsiConsole.Live(panel)
            .Overflow(VerticalOverflow.Visible)
            .StartAsync(async liveContext =>
            {
                foreach (string phoneticWord in phoneticWords)
                {
                    await Task.Delay(250, cancellationToken);

                    string node = $"[bold green]{Markup.Escape(phoneticWord)}[/]";

                    tree.AddNode(node);

                    liveContext.Refresh();
                }
            });

        return 0;
    }

    private static List<string> ConvertToNato(string word) =>
        [.. word.Select(ToNato)];

    private static string ToNato(char character) => char.ToUpperInvariant(character) switch
    {
        'A' => "Alfa",
        'B' => "Bravo",
        'C' => "Charlie",
        'D' => "Delta",
        'E' => "Echo",
        'F' => "Foxtrot",
        'G' => "Golf",
        'H' => "Hotel",
        'I' => "India",
        'J' => "Juliett",
        'K' => "Kilo",
        'L' => "Lima",
        'M' => "Mike",
        'N' => "November",
        'O' => "Oscar",
        'P' => "Papa",
        'Q' => "Quebec",
        'R' => "Romeo",
        'S' => "Sierra",
        'T' => "Tango",
        'U' => "Uniform",
        'V' => "Victor",
        'W' => "Whiskey",
        'X' => "X-ray",
        'Y' => "Yankee",
        'Z' => "Zulu",
        _ => throw new ArgumentOutOfRangeException(nameof(character)),
    };
}