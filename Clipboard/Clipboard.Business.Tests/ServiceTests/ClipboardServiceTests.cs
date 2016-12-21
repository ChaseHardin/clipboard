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
        public void AddTextToDictionary_HaveTextFromClipboard_ShouldHaveNewItemInDictionary()
        {
            var clipboardService = new ClipboardService();
            var dictionary = clipboardService.ClipboardDirectory;
            
            System.Windows.Clipboard.SetText("Adding item to dictionary.");
            var firstCopy = clipboardService.AddTextToDictionary(clipboardService.GetTextFromClipboard(), dictionary);
            
            Assert.AreEqual(1, firstCopy.Count);
            Assert.AreEqual("Adding item to dictionary.", firstCopy.First().Value);
           
            System.Windows.Clipboard.SetText("Second item to dictionary.");
            var secondCopy = clipboardService.AddTextToDictionary(clipboardService.GetTextFromClipboard(), dictionary);

            Assert.AreEqual(2, secondCopy.Count);
            Assert.AreEqual("Second item to dictionary.", secondCopy.First(x => x.Key == 2).Value);
        }

        [TestMethod]
        public void SelectFromClipboard_HaveMultipleItemsInDictionary_ShouldSetClipboardWithSelectedItem()
        {
            var clipboardService = new ClipboardService();
            var dictionary = clipboardService.ClipboardDirectory;
            dictionary.Add(1, "first item");
            dictionary.Add(2, "second item");
            dictionary.Add(3, "third item");

            Assert.AreEqual(3, dictionary.Count, "PRE-CHECK: Verify 3 items are in dictionary.");
            Assert.AreEqual("second item",  clipboardService.SelectFromClipboard(2, dictionary));
            Assert.AreEqual("second item", clipboardService.GetTextFromClipboard());
        }
    }
} 