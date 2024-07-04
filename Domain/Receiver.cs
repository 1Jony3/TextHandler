using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace Domain
{
    public class Receiver
    {
        public async Task ProcessFiles(
            string[] filePaths, 
            string[] outputFile, 
            int minWordLength, 
            bool removePunctuation
            )
        {
            for (int i = 0; i < filePaths.Length; i++)
            {
                await ProcessFile(filePaths[i], outputFile[i], minWordLength, removePunctuation);
            }
        }

        private async Task ProcessFile(
            string inputFilePath, 
            string outputFilePath, 
            int minWordLength, 
            bool removePunctuation
            )
        {
            var text = await File.ReadAllTextAsync(inputFilePath);
            var processedText = ProcessText(text, minWordLength, removePunctuation);
            await File.WriteAllTextAsync(outputFilePath, processedText);
        }

        private string ProcessText(string text, int minWordLength, bool removePunctuation)
        {
            if (removePunctuation)
            {
                text = RemovePunctuation(ref text);
            }
            text = RemoveMinWordLenght(ref text, minWordLength);
            MessageBox.Show(text);
            MessageBox.Show("Обработка завершена");
            return text;
        }
        private string RemoveMinWordLenght(ref string text, int minWordLength)
        {
            var words = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var filteredWords = words.Where(word => word.Length > minWordLength);
            
            return string.Join(" ", filteredWords);
        }
        private string RemovePunctuation(ref string text)
        {
            return new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
        }
    }
}
