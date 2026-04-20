using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using avalonia_issues_reproductions.Models;

namespace avalonia_issues_reproductions.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        foreach (var f in PlatformFilters)
            f.PropertyChanged += (_, _) => RefreshFilter();

        foreach (var f in StatusFilters)
            f.PropertyChanged += (_, _) => RefreshFilter();

        FilteredRepros.CollectionChanged += (_, _) =>
            OnPropertyChanged(nameof(FilteredReprosCount));

        RefreshFilter();
    }
    
    // Tags
    public ObservableCollection<TagFilter> PlatformFilters { get; } =
        new(Enum.GetValues<Platform>().Select(p => new TagFilter(p.ToString())));

    public ObservableCollection<TagFilter> StatusFilters { get; } =
        new(Enum.GetValues<IssueStatus>().Select(s => new TagFilter(s.ToString())));
    
    
    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private ReproInfo? _selectedReproInfo;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasCurrentRepro))]
    
    private ViewModelBase? _currentRepro;

    public bool HasCurrentRepro => CurrentRepro is not null;

    public NavigationPage? Navigator { get; set; }

    public ObservableCollection<ReproInfo> FilteredRepros { get; } = [];
    public int FilteredReprosCount => FilteredRepros.Count;

    partial void OnSearchTextChanged(string value) => RefreshFilter();

    partial void OnSelectedReproInfoChanged(ReproInfo? value)
    {
        if (value is null)
        {
            CurrentRepro = null; 
            return;
        }

        var reproVm = value.ViewModelFactory();
        CurrentRepro = new ReproPageViewModel(value, reproVm);

        if (Navigator == null) return;

        _ = Navigator.ReplaceAsync(new ContentPage
        {
            Header = value.Title,
            Content = new ContentControl { Content = CurrentRepro }
        });
    }

    [RelayCommand]
    private void ClearTags()
    {
        foreach (var f in PlatformFilters) f.IsSelected = false;
        foreach (var f in StatusFilters)  f.IsSelected = false;
    }
    
    private void RefreshFilter()
    {
        var selectedPlatforms = PlatformFilters
            .Where(f => f.IsSelected)
            .Select(f => f.Name)
            .ToHashSet(StringComparer.Ordinal);

        var selectedStatuses = StatusFilters
            .Where(f => f.IsSelected)
            .Select(f => f.Name)
            .ToHashSet(StringComparer.Ordinal);

        var search = SearchText.Trim();

        FilteredRepros.Clear();
        foreach (var repro in ReprosList.AllRepros)
        {
            var matchesSearch = string.IsNullOrEmpty(search)
                || repro.Title.Contains(search, StringComparison.OrdinalIgnoreCase)
                || repro.Description.Contains(search, StringComparison.OrdinalIgnoreCase);

            var matchesPlatform = selectedPlatforms.Count == 0
                || repro.Platforms.Any(p => selectedPlatforms.Contains(p.ToString()));

            var matchesStatus = selectedStatuses.Count == 0
                || selectedStatuses.Contains(repro.StatusName);

            if (matchesSearch && matchesPlatform && matchesStatus)
                FilteredRepros.Add(repro);
        }

        if (SelectedReproInfo is not null && !FilteredRepros.Contains(SelectedReproInfo))
            SelectedReproInfo = FilteredRepros.FirstOrDefault();
    }
}
