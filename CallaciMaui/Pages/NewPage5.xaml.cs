using CallaciMaui.Helpers;
using CallaciMaui.Models;

using System.Text.Json;

namespace CallaciMaui.Pages;

public partial class NewPage5 : ContentPage
{
	public NewPage5()
	{
        InitializeComponent();
        OnGetBtnClicked();
	}

    private void ShowReaction(Object sender, EventArgs e)
    {
        DisplayAlert("Bonjour", "Wollos", "Ok");
    }

    public async void OnGetBtnClicked()
    {
        statusMessage.Text = "";
        List<Question> questions = await App.QuestionRepo.GetAllQuestion();
        foreach (Question question in questions)
        {
            imageBox.Source = question.Image;
            List<Answer> answers = JsonSerializer.Deserialize<List<Answer>>(question.Answers);
            AnswerBtn.Text = answers.ElementAt(0).Text;
            AnswerBtn2.Text = answers.ElementAt(1).Text;
            AnswerBtn3.Text = answers.ElementAt(2).Text;
            qtnLabel.Text = question.QuestionText;
            await Task.Delay(200000);
        }
    }
}