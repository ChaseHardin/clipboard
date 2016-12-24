using System.Collections.Generic;

namespace Clipboard.Business.Services
{
    public class ClipboardService
    {
        public List<string> ClipboardDirectory = new List<string>();

        public string GetTextFromClipboard()
        {
            return System.Windows.Clipboard.GetText();
        }

        public List<string> AddTextToList(string getTextFromClipboard)
        {
            ClipboardDirectory.Insert(0, getTextFromClipboard);
            SetDirectoryMax(3);
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
                var item = ClipboardDirectory[ClipboardDirectory.Count - 1];
                ClipboardDirectory.Remove(item);
            }
        }
    }
}