using System.Collections.Generic;
using System.Linq;
using Clipboard.Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clipboard.Business.Tests.ServiceTests
{
    [TestClass]
    public class ClipboardServiceTests
    {
        private readonly ClipboardService _clipboardService = new ClipboardService();
        
        [TestMethod]
        public void GetDataFromClipboard_ClipboardHasData_ShouldReturnDataOnClipboard()
        {
            SetText("Test data getting set.");
            Assert.AreEqual("Test data getting set.", _clipboardService.GetTextFromClipboard());
        }

        [TestMethod]
        public void AddTextToList_HaveTextFromClipboard_ShouldHaveNewItemInList()
        {
            var list = _clipboardService.ClipboardDirectory;
            
            SetText("Adding item to dictionary.");
            var firstCopy = _clipboardService.AddTextToList(_clipboardService.GetTextFromClipboard(), list);
            Assert.AreEqual(1, firstCopy.Count);
            Assert.AreEqual("Adding item to dictionary.", firstCopy.First());

            SetText("Second item to dictionary.");
            var secondCopy = _clipboardService.AddTextToList(_clipboardService.GetTextFromClipboard(), list);
            Assert.AreEqual(2, secondCopy.Count);
            Assert.AreEqual("Second item to dictionary.", secondCopy[1]);
        }

        [TestMethod]
        public void SelectFromClipboard_HaveMultipleItemsInList_ShouldSetClipboardWithSelectedItem()
        {
            var list = _clipboardService.ClipboardDirectory;
            AddItemsToList(list);

            Assert.AreEqual(3, list.Count, "PRE-CHECK: Verify 3 items are in dictionary.");
            Assert.AreEqual("second item", _clipboardService.SelectFromClipboard(1, list));
            Assert.AreEqual("second item", _clipboardService.GetTextFromClipboard());
        }

        [TestMethod]
        public void OrderList_AfterSelectingFromList_ShouldHaveSelectedAsIndexOne()
        {
            var list = _clipboardService.ClipboardDirectory;
            AddItemsToList(list);

            Assert.AreEqual(3, list.Count, "PRE-CHECK: Verify 3 items are in dictionary.");
            _clipboardService.SelectFromClipboard(2, list);
            
            Assert.AreEqual("third item", _clipboardService.GetTextFromClipboard(), "Verify selected item is added to clipboard.");
            Assert.AreEqual("third item", list[0]);
            Assert.AreEqual("first item", list[1]);
            Assert.AreEqual("second item", list[2]);
        }

        // helpers
        private static void SetText(string text)
        {
            System.Windows.Clipboard.SetText(text);
        }

        private static void AddItemsToList(ICollection<string> list)
        {
            list.Add("first item");
            list.Add("second item");
            list.Add("third item");
        }
    }
}