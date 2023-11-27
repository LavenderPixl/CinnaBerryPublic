using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CinnaBerry.ViewModels;

namespace CinnaBerry.Views;

public partial class PublicProfileView : UserControl
{
    public PublicProfileView()
    {
        DataContext = new PublicProfileViewModel();
        InitializeComponent();
    }
}