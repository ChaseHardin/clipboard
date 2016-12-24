using System.Collections.Generic;

namespace Clipboard.Business.Services
{
    public class ClipboardService
    {
        public List<string> ClipboardDirectory = new List<string>();
        public int MaxSizeOfDirectory;

        public string GetTextFromClipboard()
        {
            return System.Windows.Clipboard.GetText();
        }

        public List<string> AddTextToList(string getTextFromClipboard)
        {
            ClipboardDirectory.Insert(0, getTextFromClipboard);
            SetDirectoryMax(MaxSizeOfDirectory);
            return ClipboardDirectory;
        }

        public string SelectFromClipboard(int index)
        {
            var selected = ClipboardDirectory[index];
            System.Windows.Clipboard.SetText(selected);
            OrderList(index);
            return selected;
        }

        private void OrderList(int selectedIndex)
        {
            var current = ClipboardDirectory[selectedIndex];
            ClipboardDirectory.Remove(current);
            ClipboardDirectory.Insert(0, current);
        }

        private void SetDirectoryMax(int max)
        {
            if (ClipboardDirectory.Count > max)
            {
                ClipboardDirectory.Remove(ClipboardDirectory[ClipboardDirectory.Count - 1]);
            }
        }
    }
}