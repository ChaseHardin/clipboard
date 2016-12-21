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

        public List<string> AddTextToList(string getTextFromClipboard, List<string> list)
        {
            list.Add(getTextFromClipboard);
            return list;
        }

        public string SelectFromClipboard(int index, List<string> list)
        {
            var selected = list[index];
            System.Windows.Clipboard.SetText(selected);
            OrderList(index, list);
            return selected;
        }

        private static void OrderList(int selectedIndex, IList<string> list)
        {
            var current = list[selectedIndex];
            list.Remove(current);
            list.Insert(0, current);
        }
    }
}