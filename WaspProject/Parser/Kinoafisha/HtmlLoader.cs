using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaspProject.Parser.Kinoafisha
{
    class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;

        public HtmlLoader(IParserSettings settings)
        {
            url = $"{settings.BaseUrl}/{settings.Prefix}/";
            client = new HttpClient();
        }
        public async Task<string> GetSourceByPageId(int id)
        {
            var currentUrl = url.Replace("{CurrentID}", id.ToString());
            var response = await client.GetAsync(currentUrl);
            string sourse = null;
            if(response != null && response.StatusCode == HttpStatusCode.OK)
            {
                sourse = await response.Content.ReadAsStringAsync();
            }
            return sourse;
        }
    }
}
