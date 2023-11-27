using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CinnaBerry.ViewModels;

namespace CinnaBerry.Views;

public partial class MoviesView : UserControl
{
    public MoviesView()
    {
        DataContext = new MoviesViewModel();
        InitializeComponent();
    }
}