using CallaciMaui.Helpers;
using CallaciMaui.Models;

using System.Text.Json;

namespace CallaciMaui.Pages;

public partial class DataPage : ContentPage
{
	public DataPage()
	{
		InitializeComponent();
        OnGetBtnClicked();
	}

    public async void OnGetBtnClicked()
    {
        statusMessage.Text = "";
        List<Question> Questions = await App.QuestionRepo.GetAllQuestion();
        foreach (Question question in Questions)
        {
            label1.Text = question.Image;
            List<Answer> answers = JsonSerializer.Deserialize<List<Answer>>(question.Answers);
            label2.Text = answers.ElementAt(0).Text;
            label3.Text = answers.ElementAt(1).Text;
            label4.Text = answers.ElementAt(2).Text;
            label5.Text = question.QuestionText;
            await Task.Delay(200000);
        }
    }


}