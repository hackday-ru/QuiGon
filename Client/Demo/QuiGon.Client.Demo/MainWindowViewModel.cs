//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Threading.Tasks;
//using QiuGon.DataProviders.VkDakaProvider;
//using QuiGon.Analysis.Filters;
//using QuiGon.Analysis.Filters.TextFilters;
//using QuiGon.Analysis.Helpers;
//using QuiGon.Analysis.LanguageDetection;
//using QuiGon.Analysis.Sentiment;
//using QuiGon.Analysis.Text;
//using QuiGon.Infrastructure.Entities;

//namespace QuiGon.Client.Demo
//{
//    public class MainWindowViewModel 
//    {
//        private bool _isBusy;

//        public bool IsBusy1
//        {
//            get { return _isBusy; }
//            set
//            {
//            }
//        }

//        private ObservableCollection<SubjectViewModel> _subjects;
        
//        public string SubjectId
//        {
//            get { return _subjectId; }
//            set
//            {
//                _subjectId = value;
//                RaisePropertyChanged(nameof(SubjectId));
//                AnalysisCommand.RaiseCanExecuteChanged();
//            }
//        }
//        private string _subjectId;
        

//        public MainWindowViewModel()
//        {
//            AnalysisCommand = new DelegateCommand(AnalyseExecuteAsync, CanAnalysisCommandExecute);
//        }

//        private bool CanAnalysisCommandExecute()
//        {
//            return !IsBusy1 && !String.IsNullOrWhiteSpace(SubjectId);
//        }

//        private async void AnalyseExecuteAsync()
//        {
//        }


//        private async Task AnalyzeLast100PostForUserAsync(string userId)
//        {
//            var data =  await GetWallData(userId).ConfigureAwait(true);

//            var responses = new List<TextAnalysisRequest>();
//            foreach (var subjectAction in data ?? new List<SubjectAction>())
//            {
//                var terms = WordsSeparator.SeparateWords((subjectAction.Content as TextContent)?.Content ?? string.Empty);
//                responses.Add(new TextAnalysisRequest(subjectAction.Id, subjectAction.Type, new TextAnalysisData(terms)));
//            }

//            var filters = new List<IFilter>
//            {
//                new TextToLowerCaseFilter(),
//                new NumericFilter(),
//                new PunctuationFilter(),
//                new StemingFilter(),
//                new StopWordsFilter()

//            };

//            var filterChain = new FilterChain(filters);
//            var sentimentAnalyzer = new SentimentAnalyzer();
//            var languageDetector = new LanguageDetector();
//            var textAnalyzer = new TextAnalyzer(filterChain, sentimentAnalyzer, languageDetector);

//            var counter = 0;
//            foreach (var textAnalysisRequest in responses)
//            {
//                var response = textAnalyzer.Analyze(textAnalysisRequest);

//                if (response == null)
//                {
//                    Console.WriteLine("Fail with data filling");
//                }
//                if (response == null)
//                {
//                    Console.WriteLine("Fail with data filling");
//                    continue;
//                }
//                var content = (textAnalysisRequest.Data as TextContent)?.Content ?? "Нет данных";
//                var metric = response.Statistic.ToString();

//                Subjects.Add(new SubjectViewModel(response.ActionId.ToString(), content, metric, response.Mood.ToString() ));
//            }
//        }

//        private async Task<List<SubjectAction>> GetWallData(string userId)
//        {
//            var vkDataProvider = new VkDataProvider();
//            return await vkDataProvider.GetWallForUserAsync(userId).ConfigureAwait(true);
//        }

//        public ObservableCollection<SubjectViewModel> Subjects
//        {
//            get { return _subjects; }
//            set
//            {
//                _subjects = value; 
//                RaisePropertyChanged(nameof(Subjects));
//            }
//        }

//    }

//    public class SubjectViewModel : ViewModelBase
//    {
//        private string _id;

//        private string _content;

//        private string _metrika;

//        public SubjectViewModel(string id, string content, string metrika, string mood)
//        {
//            _id = id;
//            _content = content;
//            _metrika = metrika;
//            _mood = mood;
//        }

//        private string _mood;

//        public string Id
//        {
//            get { return _id; }
//            set
//            {
//                _id = value;
//                RaisePropertyChanged(nameof(Id));
//            }
//        }

//        public string SubjectContent
//        {
//            get { return _content; }
//            set
//            {
//                _content = value;
//                RaisePropertyChanged(nameof(SubjectContent));
//            }
//        }

//        public string Metrika
//        {
//            get { return _metrika; }
//            set
//            {
//                _metrika = value;
//                RaisePropertyChanged(nameof(Metrika));
//            }
//        }

//        public string Mood
//        {
//            get { return _mood; }
//            set
//            {
//                _mood = value;
//                RaisePropertyChanged(nameof(Mood));
//            }
//        }
//    }
//}