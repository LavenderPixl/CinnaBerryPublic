using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CinnaBerry.ViewModels;

namespace CinnaBerry.Views;

public partial class PrivateProfileView : UserControl
{
    public PrivateProfileView()
    {
        DataContext = new PrivateProfileViewModel();
        InitializeComponent();
    }
}