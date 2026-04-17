using System;
using Avalonia.Controls;
using avalonia_issues_reproductions.ViewModels;

namespace avalonia_issues_reproductions.Views;

public partial class MainView : DrawerPage
{
    public MainView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);

        if (ViewModel != null)
            ViewModel.Navigator = NavPage;
    }

    internal MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext!;
}
