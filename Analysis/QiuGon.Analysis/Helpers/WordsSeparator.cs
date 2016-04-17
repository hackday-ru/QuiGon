using System;
using System.Collections.Generic;

namespace QuiGon.Analysis.Helpers
{
    public static class WordsSeparator
    {
        public static string[] SeparateWords(string text)
        {
            if (String.IsNullOrEmpty(text)) return null;

            var returnedValues = new List<string>();
            foreach (var term in text.Split(' ', '\t', '\n', '\n', '\r'))
            {
                if (String.IsNullOrWhiteSpace(term)) continue;
                returnedValues.Add(term);
            }
            return returnedValues.ToArray();
        }
    }
}