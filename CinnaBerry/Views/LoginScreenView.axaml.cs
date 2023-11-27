using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CinnaBerry.ViewModels;

namespace CinnaBerry.Views;

public partial class LoginScreenView : UserControl
{
    public LoginScreenView()
    {
        InitializeComponent();
        DataContext = new LoginScreenViewModel();
    }
}