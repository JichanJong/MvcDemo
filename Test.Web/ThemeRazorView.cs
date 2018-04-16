using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Test.Web
{
    public class ThemeRazorView : IView
    {
        public string ViewPath { get; private set; }
        public ThemeRazorView(string viewLocation)
        {
            ViewPath = viewLocation;
        }
        public void Render(ViewContext viewContext, System.IO.TextWriter writer)
        {
            Type viewType = BuildManager.GetCompiledType(this.ViewPath);
            object instance = Activator.CreateInstance(viewType);
            WebViewPage page = (WebViewPage)instance as WebViewPage;
            
            page.VirtualPath = this.ViewPath;
            page.ViewContext = viewContext;
            page.ViewData = viewContext.ViewData;
            page.InitHelpers();

            WebPageContext pageContext = new WebPageContext(viewContext.HttpContext, null, null);
            WebPageRenderingBase startPage = StartPage.GetStartPage(page, "_ViewStart", new string[] { "cshtml" });
            page.ExecutePageHierarchy(pageContext, writer, startPage);
        }
    }
}