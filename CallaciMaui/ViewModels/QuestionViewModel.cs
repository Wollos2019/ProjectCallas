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

        public QuestionsViewModel()
        {
            source = new List<Question>();
            CreateQuestionCollection();

            selectedQuestion = Questions.Skip(3).FirstOrDefault();
            MonkeySelectionChanged();

            SelectedQuestions = new ObservableCollection<object>()
            {
                Questions[1], Questions[3], Questions[4]
            };
        }

        void CreateQuestionCollection()
        {
            source.Add(new Question
            {
                Name = "Baboon",
                Location = "Africa & Asia",
                Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
            });

            source.Add(new Monkey
            {
                Name = "Capuchin Monkey",
                Location = "Central & South America",
                Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
            });

            source.Add(new Monkey
            {
                Name = "Blue Monkey",
                Location = "Central and East Africa",
                Details = "The blue monkey or diademed monkey is a species of Old World monkey native to Central and East Africa, ranging from the upper Congo River basin east to the East African Rift and south to northern Angola and Zambia",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
            });

            source.Add(new Monkey
            {
                Name = "Squirrel Monkey",
                Location = "Central & South America",
                Details = "The squirrel monkeys are the New World monkeys of the genus Saimiri. They are the only genus in the subfamily Saimirinae. The name of the genus Saimiri is of Tupi origin, and was also used as an English name by early researchers.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Saimiri_sciureus-1_Luc_Viatour.jpg/220px-Saimiri_sciureus-1_Luc_Viatour.jpg"
            });

            source.Add(new Question
            {
                QuestionText = "Golden Lion Tamarin",
                Solution = "Brazil",
                Mark = "The golden lion tamarin also known as the golden marmoset, is a small New World monkey of the family Callitrichidae.",
                Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/Golden_lion_tamarin_portrait3.jpg/220px-Golden_lion_tamarin_portrait3.jpg"
            });

          
            Questions = new ObservableCollection<Question>(source);
        }

        void FilterItems(string filter)
        {
            var filteredItems = source.Where(monkey => monkey.Name.ToLower().Contains(filter.ToLower())).ToList();
            foreach (var monkey in source)
            {
                if (!filteredItems.Contains(monkey))
                {
                    Monkeys.Remove(monkey);
                }
                else
                {
                    if (!Monkeys.Contains(monkey))
                    {
                        Monkeys.Add(monkey);
                    }
                }
            }
        }

        void MonkeySelectionChanged()
        {
            SelectedMonkeyMessage = $"Selection {selectionCount}: {SelectedMonkey.Name}";
            OnPropertyChanged("SelectedMonkeyMessage");
            selectionCount++;
        }

        void RemoveMonkey(Monkey monkey)
        {
            if (Monkeys.Contains(monkey))
            {
                Monkeys.Remove(monkey);
            }
        }

        void FavoriteMonkey(Monkey monkey)
        {
            monkey.IsFavorite = !monkey.IsFavorite;
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
