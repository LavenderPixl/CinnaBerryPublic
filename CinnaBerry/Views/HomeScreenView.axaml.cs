using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CinnaBerry.ViewModels;

namespace CinnaBerry.Views;

public partial class HomeScreenView : UserControl
{
    public HomeScreenView()
    {
        DataContext = new HomeScreenViewModel();
        InitializeComponent();
    }
}