using System.Linq;
using Clipboard.Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clipboard.Business.Tests.ServiceTests
{
    [TestClass]
    public class ClipboardServiceTests
    {
        private ClipboardService _clipboardService;

        [TestInitialize]
        public void Initialize()
        {
            _clipboardService = new ClipboardService { MaxSizeOfDirectory = 3 };
        }

        [TestMethod]
        public void GetDataFromClipboard_ClipboardHasData_ShouldReturnDataOnClipboard()
        {
            SetText("Test data getting set.");
            Assert.AreEqual("Test data getting set.", _clipboardService.GetTextFromClipboard());
        }

        [TestMethod]
        public void AddTextToList_HaveTextFromClipboard_ShouldHaveNewItemInList()
        {
            SetText("Adding item to directory.");
            var firstCopy = _clipboardService.AddTextToList(_clipboardService.GetTextFromClipboard());
            Assert.AreEqual(1, firstCopy.Count);
            Assert.AreEqual("Adding item to directory.", firstCopy.First());

            SetText("Second item to directory.");
            var secondCopy = _clipboardService.AddTextToList(_clipboardService.GetTextFromClipboard());
            Assert.AreEqual(2, secondCopy.Count);
            Assert.AreEqual("Second item to directory.", secondCopy[0]);
        }

        [TestMethod]
        public void SelectFromClipboard_HaveMultipleItemsInList_ShouldSetClipboardWithSelectedItem()
        {
            AddItemsToList();
            Assert.AreEqual(3, _clipboardService.ClipboardDirectory.Count, "PRE-CHECK: Verify 3 items are in directory.");
            Assert.AreEqual("second item", _clipboardService.SelectFromClipboard(1));
            Assert.AreEqual("second item", _clipboardService.GetTextFromClipboard());
        }

        [TestMethod]
        public void OrderList_AfterSelectingFromList_ShouldHaveSelectedAsIndexOne()
        {
            var list = _clipboardService.ClipboardDirectory;
            AddItemsToList();

            Assert.AreEqual(3, list.Count, "PRE-CHECK: Verify 3 items are in directory.");
            _clipboardService.SelectFromClipboard(2);

            Assert.AreEqual("third item", _clipboardService.GetTextFromClipboard(), "Verify selected item is added to clipboard.");
            Assert.AreEqual("third item", list[0]);
            Assert.AreEqual("first item", list[1]);
            Assert.AreEqual("second item", list[2]);
        }

        [TestMethod]
        public void SetDirectoryMax_whenClipoardMaxIsHit_ShouldRemoveLastItemInDirectory()
        {
            Assert.AreEqual(0, _clipboardService.ClipboardDirectory.Count, "PRE-CHECK: Verify directory is emmpty.");

            _clipboardService.AddTextToList("first item added");
            _clipboardService.AddTextToList("second item added");
            _clipboardService.AddTextToList("third item added");

            Assert.AreEqual(3, _clipboardService.ClipboardDirectory.Count, "PRE-CHECK: Verify 3 items are in directory.");
            _clipboardService.AddTextToList("new item added");

            Assert.AreEqual(3, _clipboardService.ClipboardDirectory.Count, "PRE-CHECK: Verify 3 items are in directory.");
            Assert.AreEqual("new item added", _clipboardService.ClipboardDirectory[0]);
            Assert.AreEqual("third item added", _clipboardService.ClipboardDirectory[1]);
            Assert.AreEqual("second item added", _clipboardService.ClipboardDirectory[2]);
        }

        // helpers
        private static void SetText(string text)
        {
            System.Windows.Clipboard.SetText(text);
        }

        private void AddItemsToList()
        {
            _clipboardService.AddTextToList("third item");
            _clipboardService.AddTextToList("second item");
            _clipboardService.AddTextToList("first item");
        }
    }
}