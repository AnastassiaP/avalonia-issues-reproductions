using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using avalonia_issues_reproductions.Models;

namespace avalonia_issues_reproductions.ViewModels;

public partial class ReproPageViewModel : ViewModelBase
{
    public ReproInfo Info { get; }
    public ViewModelBase ReproContent { get; }
    public bool HasIssueUrl => Info.IssueUrl is not null;

    public ReproPageViewModel(ReproInfo info, ViewModelBase reproContent)
    {
        Info = info;
        ReproContent = reproContent;
    }

    [RelayCommand]
    private void OpenIssue()
    {
        if (Info.IssueUrl is { } url)
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }
}
