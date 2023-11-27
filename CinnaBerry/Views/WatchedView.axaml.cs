using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CinnaBerry.ViewModels;

namespace CinnaBerry;

public partial class WatchedView : UserControl
{
    public WatchedView()
    {
        DataContext = new WatchedViewModel();
        InitializeComponent();
    }
}