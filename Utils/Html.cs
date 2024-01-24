using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youtube369.Utils
{
    internal static class Html
    {
        private static readonly HtmlParser HtmlParser = new();

        public static IHtmlDocument Parse(string source) => HtmlParser.ParseDocument(source);
    }
}
