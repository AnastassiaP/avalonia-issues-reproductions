using avalonia_issues_reproductions.ViewModels;

namespace avalonia_issues_reproductions.Models;

/// <summary>
/// Descriptor for a single repro entry.
/// </summary>

public record ReproInfo(
    string Title,
    string Description,
    Platform[] Platforms,
    IssueStatus Status,
    Func<ViewModelBase> ViewModelFactory
)
{
    public IEnumerable<string> PlatformNames => Platforms.Select(p => p.ToString());
    public string StatusName => Status.ToString();
}
