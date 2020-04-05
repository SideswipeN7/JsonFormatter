using Newtonsoft.Json;
using System.Globalization;

namespace JsonApp.JsonApplication.Formatter
{
    public class JsonFormatter
    {
        private JsonSerializerSettings jsonSettings;

        public JsonFormatter()
        {
            jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Culture = CultureInfo.InvariantCulture
            };
        }

        public FormatingResult Format(string jsonText)
        {
            try
            {
                object json = JsonConvert.DeserializeObject(jsonText, jsonSettings);


                return new FormatingResult
                {
                    FormattedText = JsonConvert.SerializeObject(json, jsonSettings)
                };
            }
            catch (JsonException ex)
            {
                return new FormatingResult
                {
                    Error = ex.Message
                };
            }
            catch
            {
                return new FormatingResult();
            }
        }
    }
}