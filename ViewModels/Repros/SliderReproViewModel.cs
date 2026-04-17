using CommunityToolkit.Mvvm.ComponentModel;

namespace avalonia_issues_reproductions.ViewModels.Repros;

public partial class SliderReproViewModel : ViewModelBase
{
    [ObservableProperty]
    private double _horizontalValue = 40;

    [ObservableProperty]
    private double _verticalValue = 60;
}
