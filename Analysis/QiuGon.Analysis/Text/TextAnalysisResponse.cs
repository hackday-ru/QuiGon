using QuiGon.Analysis.Sentiment;

namespace QuiGon.Analysis.Text
{
    public class TextAnalysisResponse
    {
        public TextAnalysisResponse(long actionId, SentimentMood mood, double statistic)
        {
            ActionId = actionId;
            Mood = mood;
            Statistic = statistic;
        }

        public long ActionId { get; }
        
        public SentimentMood Mood { get; }

        public double Statistic { get; }
    }
}