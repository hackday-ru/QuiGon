using System;

namespace QuiGon.Analysis.Helpers
{
    public static class WordsSeparator
    {
        public static string[] SeparateWords(string text)
        {
            if (String.IsNullOrEmpty(text)) return null;

            return text.Split(' ', '\t', '\n', '\n', '\r');
        }
    }
}