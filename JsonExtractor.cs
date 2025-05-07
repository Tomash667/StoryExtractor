using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StoryExtractor
{
    public class JsonExtractor
    {
        public static List<string> ExtractModelContents(string jsonFilePath)
        {
            var result = new List<string>();

            var jsonText = File.ReadAllText(jsonFilePath);
            var jsonObj = JsonConvert.DeserializeObject<JObject>(jsonText);

            var chunks = jsonObj["chunkedPrompt"]["chunks"] as JArray;
            foreach (var item in chunks)
            {
                var role = item["role"]?.ToString();
                var isThought = item["isThought"]?.ToObject<bool?>() ?? false;

                if (role == "model" && !isThought)
                {
                    var content = item["text"]?.ToString();
                    if (!string.IsNullOrEmpty(content))
                    {
                        result.Add(content);
                    }
                }
            }

            return result;
        }
    }
}
