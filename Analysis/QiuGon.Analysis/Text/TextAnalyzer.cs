using System;
using System.Collections.Generic;
using System.Linq;
using QuiGon.Analysis.Filters;
using QuiGon.Analysis.LanguageDetection;
using QuiGon.Analysis.Sentiment;
using QuiGon.Analysis.Text.Statistic;
using QuiGon.Analysis.Text.TerroristWords;

namespace QuiGon.Analysis.Text
{
    public class TextAnalyzer
    {
        private readonly FilterChain _filterChain;
        private readonly SentimentAnalyzer _sentimentAnalyzer;
        private readonly LanguageDetector _languageDetector;

        public TextAnalyzer(FilterChain filterChain, SentimentAnalyzer sentimentAnalyzer, LanguageDetector languageDetector)
        {
            if (filterChain == null) throw new ArgumentNullException(nameof(filterChain));
            if (sentimentAnalyzer == null) throw new ArgumentNullException(nameof(sentimentAnalyzer));
            if (languageDetector == null) throw  new ArgumentNullException(nameof(languageDetector));

            _filterChain = filterChain;
            _sentimentAnalyzer = sentimentAnalyzer;
            _languageDetector = languageDetector;
        }

        public TextAnalysisResponse Analyze(TextAnalysisRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var language = _languageDetector.Detect(request);
            if (!language.HasValue) return null;
            
            var filteredText = _filterChain.Filter(request);
            var sentimentMood = _sentimentAnalyzer.Analyze(language.Value, request) ?? SentimentMood.Neutral;

            var filteredData = filteredText.Data as TextAnalysisData;
            if (filteredData == null) return null;

            var terms = filteredData.Data.ToArray();

            var tfIdfCalculator = new TfidfCalculator(terms, request.ActionId);
            var statisticsResult = new Dictionary<string, double>();
            var terroristWords = TerroristWordSetFactory.Instance.GetStopWords(language.Value);
            foreach (var term in terms)
            {
                var metric = tfIdfCalculator.GetMetric(term) * 2 / terms.Length;
                if (!terroristWords.Contains(term)) continue;
                statisticsResult[term] = metric;
            }
            var totalMetric = 0.0;
            foreach (var metric in statisticsResult.Values)
            {
                totalMetric += metric;
            }

            return new TextAnalysisResponse(request.ActionId, sentimentMood, totalMetric);
        }

    }
}