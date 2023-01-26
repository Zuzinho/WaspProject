using AngleSharp.Html.Dom;
using System;


namespace WaspProject.Parser
{
    interface IParser<T> where T : class // обощенный интерфейс
    {
        T Parse(IHtmlDocument document);
    }
}
