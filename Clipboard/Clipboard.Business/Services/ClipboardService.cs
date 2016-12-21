using System.Collections.Generic;

namespace Clipboard.Business.Services
{
    public class ClipboardService
    {
        public Dictionary<int, string> ClipboardDirectory = new Dictionary<int, string>();

        public string GetTextFromClipboard()
        {
            return System.Windows.Clipboard.GetText();
        }

        public Dictionary<int, string> AddTextToDictionary(string getTextFromClipboard, Dictionary<int, string> dictionary)
        {
            dictionary.Add(dictionary.Count + 1, getTextFromClipboard);
            return dictionary;
        }
    }
}