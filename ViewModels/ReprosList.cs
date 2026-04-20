using avalonia_issues_reproductions.Models;
using avalonia_issues_reproductions.ViewModels.Repros;

namespace avalonia_issues_reproductions.ViewModels;

// Add new ReproInfo entries here to register a repro in the catalog.
public partial class ReprosList
{
    public static readonly ReproInfo[] AllRepros =
    [
        new("Button",
            "Button variants, states, toggle buttons, and click interactions",
            [Platform.MacOS, Platform.Windows, Platform.Linux],
            IssueStatus.Reproducible,
            () => new ButtonReproViewModel()),

        new("TextBox",
            "Single-line, multi-line, password, and read-only text inputs",
            [Platform.MacOS, Platform.Windows],
            IssueStatus.Fixed,
            () => new TextBoxReproViewModel()),

        new("Slider",
            "Horizontal and vertical sliders linked to a ProgressBar",
            [Platform.Android],
            IssueStatus.Reproducible,
            () => new SliderReproViewModel()),

        new("MinHeight Resize",
            "Resizing a window with MinHeight set causes it to move upward on macOS (#3117)",
            [Platform.MacOS],
            IssueStatus.Fixed,
            () => new MinHeightResizeReproViewModel(),
            IssueUrl: "https://github.com/AvaloniaUI/Avalonia/issues/3117"),
    ];
}
