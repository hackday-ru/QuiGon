using System;
using QuiGon.Analysis.LanguageDetection;
using QuiGon.Analysis.Text;
using QuiGon.Infrastructure.Entities;

namespace QuiGon.Analysis.Sentiment
{
    public class SentimentAnalyzer
    {
        private const double PositiveBottomBorder = 3;
        private const double NeutralBottomBorder = 1.4;

        public SentimentMood? Analyze(Language language, TextAnalysisRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            switch (request.Type)
            {
                case SubjectActionType.Comment:
                case SubjectActionType.Post:
                case SubjectActionType.Repost:
                    var textData = request.Data as TextAnalysisData;
                    if (textData == null) return null;
                    return AnalyzeText(language, textData);
                default:
                    return SentimentMood.Neutral;
            }
        }

        private SentimentMood? AnalyzeText(Language language, TextAnalysisData data)
        {
            if (data?.Data == null || data.Data.Count == 0) return null;

            var dictionary = SentimentStatisticFactory.Instance.GetDictionary(language);
            if (language == Language.Unknown) return null;

            double value = 0;
            var normalWords = 0;
            double positiveWeight = 0;
            double negativeWeight = 0;
            var positiveCount = 0;
            var negativeCount = 0;
            foreach (var word in data.Data)
            {
                var wordValue = dictionary[word] + 2;
                if (wordValue <= NeutralBottomBorder)
                {
                    negativeWeight += wordValue;
                    negativeCount++;
                }
                if (wordValue >= PositiveBottomBorder)
                {
                    positiveWeight += wordValue;
                    positiveCount++;
                }
                
            }


            double positivePer = 0.5;
            double negativePer = 0.5;
            if (positiveCount + negativeCount == 0) return SentimentMood.Neutral;
            if (positiveCount != 0 && negativeCount != 0)
            {
                positivePer = (double) positiveCount/(positiveCount + negativeCount);
                negativePer = (double) negativeCount/(positiveCount + negativeCount);
            }
            if (positiveCount == 0)
            {
                positivePer = (double)positiveCount / (positiveCount + negativeCount);
                negativePer = (double)negativeCount / (positiveCount + negativeCount);
            }
            if (negativeCount == 0)
            {
                positivePer = (double)positiveCount / (positiveCount + negativeCount);
                negativePer = (double)negativeCount / (positiveCount + negativeCount);
            }

            positivePer = (positivePer + positiveWeight / (4 * positiveCount)) / 2;
            negativePer = (negativePer + negativeWeight / (4 * negativeCount)) / 2;

            if (positivePer > 0.59) return SentimentMood.Positive;
            if (negativePer < 0.50) return SentimentMood.Negative;
            return SentimentMood.Neutral;
        }
    }
}