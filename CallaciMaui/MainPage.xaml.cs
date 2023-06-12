namespace CallaciMaui;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			MoveBtn.Text = $"Clicked {count} time";
		else
			MoveBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(MoveBtn.Text);
	}

    private async void MoveBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Quiz");
    }
}

