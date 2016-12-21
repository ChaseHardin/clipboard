using System.Collections.Generic;
using System.Linq;
using Clipboard.Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clipboard.Business.Tests.ServiceTests
{
    [TestClass]
    public class ClipboardServiceTests
    {
        [TestMethod]
        public void GetDataFromClipboard_ClipboardHasData_ShouldReturnDataOnClipboard()
        {
            System.Windows.Clipboard.SetText("Test data getting set.");
            var clipboardService = new ClipboardService();

            Assert.AreEqual("Test data getting set.", clipboardService.GetTextFromClipboard());
        }

        [TestMethod]
        public void AddTextToList_HaveTextFromClipboard_ShouldHaveNewItemInList()
        {
            var clipboardService = new ClipboardService();
            var list = clipboardService.ClipboardDirectory;

            System.Windows.Clipboard.SetText("Adding item to dictionary.");
            var firstCopy = clipboardService.AddTextToList(clipboardService.GetTextFromClipboard(), list);

            Assert.AreEqual(1, firstCopy.Count);
            Assert.AreEqual("Adding item to dictionary.", firstCopy.First());

            System.Windows.Clipboard.SetText("Second item to dictionary.");
            var secondCopy = clipboardService.AddTextToList(clipboardService.GetTextFromClipboard(), list);

            Assert.AreEqual(2, secondCopy.Count);
            Assert.AreEqual("Second item to dictionary.", secondCopy[1]);
        }

        [TestMethod]
        public void SelectFromClipboard_HaveMultipleItemsInList_ShouldSetClipboardWithSelectedItem()
        {
            var clipboardService = new ClipboardService();
            var list = clipboardService.ClipboardDirectory;
            AddItemsToList(list);

            Assert.AreEqual(3, list.Count, "PRE-CHECK: Verify 3 items are in dictionary.");
            Assert.AreEqual("second item", clipboardService.SelectFromClipboard(1, list));
            Assert.AreEqual("second item", clipboardService.GetTextFromClipboard());
        }

        [TestMethod]
        public void OrderList_AfterSelectingFromList_ShouldHaveSelectedAsIndexOne()
        {
            var clipboardService = new ClipboardService();
            var list = clipboardService.ClipboardDirectory;
            AddItemsToList(list);

            Assert.AreEqual(3, list.Count, "PRE-CHECK: Verify 3 items are in dictionary.");
            clipboardService.SelectFromClipboard(2, list);
            clipboardService.OrderList(1, list);
            
            Assert.AreEqual("second item", list[0]);
            Assert.AreEqual("first item", list[1]);
            Assert.AreEqual("third item", list[2]);
        }

        // helpers
        private static void AddItemsToList(ICollection<string> list)
        {
            list.Add("first item");
            list.Add("second item");
            list.Add("third item");
        }
    }
}