using Avalonia.Controls;
using Avalonia.Interactivity;

namespace avalonia_issues_reproductions.Views.Repros;

public partial class MinHeightResizeReproView : UserControl
{
    public MinHeightResizeReproView()
    {
        InitializeComponent();
    }

    private void OpenReproWindow(object? sender, RoutedEventArgs e)
    {
        var window = new MinHeightResizeWindow();
        window.Show();
    }
}
