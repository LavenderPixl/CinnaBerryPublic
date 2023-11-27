using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CinnaBerry.ViewModels;

namespace CinnaBerry.Views;

public partial class PlanToWatchView : UserControl
{
    public PlanToWatchView()
    {
        DataContext = new PlanToWatchViewModel();
        InitializeComponent();
    }
}