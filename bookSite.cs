using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaywrightSharp;
using Shouldly;

namespace SeleniumStuff;

[TestClass]
public class MyPlaywrightTests : PageTest
{
    [TestMethod]
    public async Task Homepage_Should_Have_Title()
    {
        await Page.GotoAsync("https://bookcart.azurewebsites.net/");
        var title = await Page.TitleAsync();
        title.ShouldBe("Book Cart"); // Update this to the actual title
    }
}