using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using QiuGon.DataProviders.VkDakaProvider;
using QuiGon.Analysis.Filters;
using QuiGon.Analysis.Filters.TextFilters;
using QuiGon.Analysis.Helpers;
using QuiGon.Analysis.LanguageDetection;
using QuiGon.Analysis.Sentiment;
using QuiGon.Analysis.Text;
using QuiGon.Infrastructure.Entities;

namespace QuiGon.Client.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public ObservableCollection<SubjectViewModel> SubjectViewModels; 

        public MainWindow()
        {
            InitializeComponent();
            SubjectViewModels = new ObservableCollection<SubjectViewModel>();
            PatientsDG.ItemsSource = SubjectViewModels;

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = String.Empty;
            try
            {
                Button.IsEnabled = false;
                Login.IsEnabled = false;
                if (String.IsNullOrEmpty(Login.Text))
                {
                    await this.ShowMessageAsync("Фейл", "Заполните ид").ConfigureAwait(true);
                }
                await AnalyzeLast100PostForUserAsync(Login.Text).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            if (!String.IsNullOrEmpty(errorMessage))
            {

                await this.ShowMessageAsync("Фейл", "Вот это поворот. На демонстрации...").ConfigureAwait(true);
            }
            Button.IsEnabled = true;
            Login.IsEnabled = true;

        }

        private async Task AnalyzeLast100PostForUserAsync(string userId)
        {
            var data = await GetWallData(userId).ConfigureAwait(true);

            var responses = new List<TextAnalysisRequest>();
            foreach (var subjectAction in data ?? new List<SubjectAction>())
            {
                var terms = WordsSeparator.SeparateWords((subjectAction.Content as TextContent)?.Content ?? string.Empty);
                responses.Add(new TextAnalysisRequest(subjectAction.Id, subjectAction.Type, new TextAnalysisData(terms)));
            }

            var filters = new List<IFilter>
                    {
                        new TextToLowerCaseFilter(),
                        new NumericFilter(),
                        new PunctuationFilter(),
                        new StemingFilter(),
                        new StopWordsFilter()

                    };

            var filterChain = new FilterChain(filters);
            var sentimentAnalyzer = new SentimentAnalyzer();
            var languageDetector = new LanguageDetector();
            var textAnalyzer = new TextAnalyzer(filterChain, sentimentAnalyzer, languageDetector);

            var counter = 0;
            foreach (var textAnalysisRequest in responses)
            {
                var response = textAnalyzer.Analyze(textAnalysisRequest);

                if (response == null)
                {
                    Console.WriteLine("Fail with data filling");
                }
                if (response == null)
                {
                    Console.WriteLine("Fail with data filling");
                    continue;
                }
                var content = (textAnalysisRequest.Data as TextContent)?.Content ?? "Нет данных";
                var metric = response.Statistic.ToString();

                SubjectViewModels.Add(new SubjectViewModel(response.ActionId.ToString(), content, metric, response.Mood.ToString()));
            }
        }

        private async Task<List<SubjectAction>> GetWallData(string userId)
        {
            var vkDataProvider = new VkDataProvider();
            return await vkDataProvider.GetWallForUserAsync(userId).ConfigureAwait(true);
        }
    }

    public class SubjectViewModel 
    {
        private string _id;

        private string _content;

        private string _metrika;

        public SubjectViewModel(string id, string content, string metrika, string mood)
        {
            _id = id;
            _content = content;
            _metrika = metrika;
            _mood = mood;
        }

        private string _mood;

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        public string SubjectContent
        {
            get { return _content; }
            set
            {
                _content = value;
            }
        }

        public string Metrika
        {
            get { return _metrika; }
            set
            {
                _metrika = value;
            }
        }

        public string Mood
        {
            get { return _mood; }
            set
            {
                _mood = value;
            }
        }
    }
}

