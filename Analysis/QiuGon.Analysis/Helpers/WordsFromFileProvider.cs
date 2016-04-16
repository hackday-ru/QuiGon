using System;
using System.Collections.Generic;
using System.IO;

namespace QuiGon.Analysis.Helpers
{
    /// <summary>
    /// Поставщик множества слов
    /// </summary>
    internal class WordsFromFileProvider
    {
        /// <summary>
        /// Возвращает множество слов из файла, разделенных строкой
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <returns></returns>
        public static HashSet<string> GetStopWordsFromFile(string filePath)
        {
            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    var fileContent = streamReader.ReadToEnd();
                    if (String.IsNullOrEmpty(fileContent)) return null;
                    var stopWords = fileContent.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None);
                    if (stopWords.Length == 0) return null;

                    return new HashSet<string>(stopWords);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}