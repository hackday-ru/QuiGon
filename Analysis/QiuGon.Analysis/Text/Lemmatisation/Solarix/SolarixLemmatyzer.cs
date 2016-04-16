using System;
using System.Collections.Generic;
using System.Text;
using QuiGon.Analysis.Text.Stemming;
using SolarixLemmatizatorEngineNET;

namespace QuiGon.Analysis.Text.Lemmatisation.Solarix
{
    public class SolarixLemmatyzer : ILemmatyzerFacade
    {
        #region Singletone

        private static SolarixLemmatyzer _instance;

        public static SolarixLemmatyzer Instance
        {
            get { return _instance ?? (_instance = new SolarixLemmatyzer()); }
        }

        #endregion

        private IntPtr _lemEngine;
        public const string DefaultLemmatizatorDbPath = @"Text\Lemmatisation\Solarix\lemmatizer.db";


        public SolarixLemmatyzer()
        {
            _lemEngine = LemmatizatorEngine.sol_LoadLemmatizatorW(DefaultLemmatizatorDbPath,
                LemmatizatorEngine.LEME_FASTEST);
        }

        public TextAnalysisRequest MakeLemmas(TextAnalysisRequest request)
        {
            var textData = request?.Data as TextAnalysisData;
            if (textData == null) return null;

            var filteredWords = new List<string>();
            foreach (var word in textData.Data)
            {
                var lemma = MakeLemmas(word);

                filteredWords.Add(lemma);
            }

            return new TextAnalysisRequest(request.ActionId, request.Type, new TextAnalysisData(filteredWords));
        }

        public string MakeLemmas(string text)
        {
            var lemma = new StringBuilder(256);
            int nlem = LemmatizatorEngine.sol_GetLemmaStringW(_lemEngine, 0, lemma, lemma.Length);
            return lemma.ToString();
        }

        public void Dispose()
        {
            try
            {
                LemmatizatorEngine.sol_DeleteLemmatizator(_lemEngine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}