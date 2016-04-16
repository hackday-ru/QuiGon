using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QuiGon.Analysis.Helpers;

namespace QuiGon.Analysis.Text.Stemming.YandexStemmer
{
    public class YandexStemmer : IStemerFacade
    {

        private const string StemmedDataRegexPattern = @".*{([\w]+)\??[|].*}";
        private readonly Regex _regex;

        private readonly string _cmd = @"cmd.exe ";
        private readonly string _stemmerExecution;

        private string _successfullyResponse;
        private string _failureResponse;

        private readonly TimeSpan _sleepBetweenErrorCheck = TimeSpan.FromMilliseconds(1000);

        public YandexStemmer()
        {
            _regex = new Regex(StemmedDataRegexPattern);
            _stemmerExecution = @"D:\Projects\Applications\QiuGon\Sources\Analysis\QiuGon.Analysis\bin\Debug\Text\Stemming\YandexStemmer\mystem.exe";
        }


        public TextAnalysisRequest Stem(TextAnalysisRequest request)
        {
            if (request == null) return null;

            var textData = request.Data as TextAnalysisData;
            if (textData == null) return request;
            

            try
            {
                var allWords = String.Empty;

                var filteredText = new List<string>();
                foreach (var data in textData.Data)
                {
                    allWords += data + "\n";
                }

                var stemmer = GetStemmer();
                using (var stemSteamWriter = stemmer.StandardInput)
                {
                    stemSteamWriter.Write(allWords);
                }
                var isSuccessfully = GetNormalResponseAsync(stemmer.StandardOutput).Wait(_sleepBetweenErrorCheck);
                if (isSuccessfully)
                {
                    var stemmedWords = WordsSeparator.SeparateWords(_successfullyResponse);
                    foreach (var stemmedWordResponse in stemmedWords)
                    {

                        var stemmedWord = GetWordFromStemmerResponse(stemmedWordResponse);
                        if (String.IsNullOrEmpty(stemmedWord)) continue;
                        filteredText.Add(stemmedWord);
                    }
                    SafetyClose(stemmer);
                    return new TextAnalysisRequest(request.ActionId, request.Type, new TextAnalysisData(filteredText));
                }
                Console.WriteLine("Can not stem");
                SafetyClose(stemmer);
                return request;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return request;
            }
        }

        public string Stem(string text)
        {
            throw new NotImplementedException();
        }


        private Process GetStemmer()
        {
            var stemmer = new Process
            {
                StartInfo =
                {
                    FileName = _stemmerExecution,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = Encoding.UTF8
                }
            };
            stemmer.Start();
            return stemmer;
        }

        private async Task GetNormalResponseAsync(StreamReader stream)
        {
            _successfullyResponse = await stream.ReadToEndAsync().ConfigureAwait(false);
        }
        private string GetWordFromStemmerResponse(string stemmerResponse)
        {
            var stemedDataMatch = _regex.Match(stemmerResponse);
            if (!stemedDataMatch.Success) return null;

            return stemedDataMatch.Groups[0].Value;
        }


        private void SafetyClose(Process stemmer)
        {
            try
            {
                stemmer?.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}