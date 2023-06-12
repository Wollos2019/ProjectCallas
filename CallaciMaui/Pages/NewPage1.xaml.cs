using CallaciMaui.Helpers;
using CallaciMaui.Models;
using System.Text.Json;

namespace CallaciMaui.Pages;

public partial class NewPage1 : ContentPage
{
    Answer ans = new Answer();
    List<Answer> answersList = new List<Answer>();
    
    public NewPage1()
	{
		InitializeComponent();
	}

    private async void OnCreateBtnClicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonAnswers = JsonSerializer.Serialize(answersList, options);
        await App.QuestionRepo.AddNewQuestion(
            questionText.Text, mark.Text, image.Text, solution.Text, jsonAnswers);
        statusMessage.Text = App.QuestionRepo.StatusMessage;
    }

    void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        if(selectedIndex != 1) 
        {
            ans.Id = selectedIndex;
        }
    }

    private void OnSetBtnClicked(object sender, EventArgs e)
    {
        ans.Text = response.Text;
        answersList[ans.Id] = ans;
    }
}