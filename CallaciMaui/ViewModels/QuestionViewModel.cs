using CallaciMaui.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CallaciMaui.ViewModels
{
    public class QuestionViewModel : INotifyPropertyChanged
    {
        readonly IList<Question> source;
        Question selectedQuestion;
        int selectionCount = 1;

        public ObservableCollection<Question> Questions { get; private set; }
        public IList<Question> EmptyQuestions { get; private set; }

        public Question SelectedQuestion
        {
            get
            {
                return selectedQuestion;
            }
            set
            {
                if (selectedQuestion != value)
                {
                    selectedQuestion = value;
                }
            }
        }

        ObservableCollection<object> selectedQuestions;
        public ObservableCollection<object> SelectedQuestions
        {
            get
            {
                return selectedQuestions;
            }
            set
            {
                if (selectedQuestions != value)
                {
                    selectedQuestions = value;
                }
            }
        }

        public string SelectedQuestionMessage { get; private set; }

        public ICommand DeleteCommand => new Command<Question>(RemoveQuestion);
        public ICommand FavoriteCommand => new Command<Question>(FavoriteQuestion);
        public ICommand FilterCommand => new Command<string>(FilterItems);
        public ICommand QuestionSelectionChangedCommand => new Command(QuestionSelectionChanged);

        public QuestionViewModel()
        {
            source = new List<Question>();
            CreateQuestionCollection();

            selectedQuestion = Questions.Skip(3).FirstOrDefault();
            //QuestionSelectionChanged();

            //SelectedQuestions = new ObservableCollection<object>()
            //{
            //    Questions[1], Questions[3], Questions[4]
            //};
        }

        void CreateQuestionCollection()
        {
            source.Add(new Question
            {
                Mark = "5",
                QuestionText = "Baboon",
                Solution = "Africa & Asia",
                Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
                Answers = ""
            });

            source.Add(new Question
            {
                Mark = "6",
                QuestionText = "Capuchin Monkey",
                Solution = "Central & South America",
                Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg",
                Answers = ""
            });

            source.Add(new Question
            {
                Mark = "7",
                QuestionText = "Blue Monkey",
                Solution = "Central and East Africa",
                Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg",
                Answers = ""
            });
          
            Questions = new ObservableCollection<Question>(source);
        }

        void FilterItems(string filter)
        {
            var filteredItems = source.Where(question => question.QuestionText.ToLower().Contains(filter.ToLower())).ToList();
            foreach (var question in source)
            {
                if (!filteredItems.Contains(question))
                {
                    Questions.Remove(question);
                }
                else
                {
                    if (!Questions.Contains(question))
                    {
                        Questions.Add(question);
                    }
                }
            }
        }

        void QuestionSelectionChanged()
        {
            SelectedQuestionMessage = $"Selection {selectionCount}: {SelectedQuestion.QuestionText}";
            OnPropertyChanged("SelectedQuestionMessage");
            selectionCount++;
        }

        void RemoveQuestion(Question question)
        {
            if (Questions.Contains(question))
            {
                Questions.Remove(question);
            }
        }

        void FavoriteQuestion(Question question)
        {
            question.Solution = question.Solution;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
