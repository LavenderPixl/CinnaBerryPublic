using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CinnaBerry.ViewModels;

namespace CinnaBerry.Views;

public partial class SeriesView : UserControl
{
    public SeriesView()
    {
        DataContext = new SeriesViewModel();
        InitializeComponent();
    }
}