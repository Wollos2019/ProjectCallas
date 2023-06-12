using CallaciMaui.Pages;

namespace CallaciMaui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute("Quiz", typeof(NewPage5));
    }
}
