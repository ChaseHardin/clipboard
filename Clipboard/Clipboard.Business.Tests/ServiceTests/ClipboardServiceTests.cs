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
    }
} 