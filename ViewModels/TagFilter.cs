using CommunityToolkit.Mvvm.ComponentModel;

namespace avalonia_issues_reproductions.ViewModels;

/// <summary>
/// A toggle-able tag used to filter the repro catalog.
/// </summary>
public partial class TagFilter(string name) : ObservableObject
{
    public string Name { get; } = name;

    [ObservableProperty]
    private bool _isSelected;
}
