using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace DiscordChatBotProject.config
{
    internal class JSONReader
    {
        public string token {get;set;}
        public string prefix { get;set; }
        public async Task ReadJSON()
        {
            using (StreamReader sr = new StreamReader("config.json"))
            {
                string json = await sr.ReadToEndAsync();
                JsonStructure data = JsonConvert.DeserializeObject<JsonStructure>(json);
                this.token = data.token;
                this.prefix = data.prefix;

            };
        }
    }
    internal sealed class JsonStructure 
    {
        public string token { get; set; }
        public string prefix { get; set; }
    }


}
