using Doch.Models;
using Doch.Models.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Doch.Web.Code
{
    public static class Utils
    {
        public static async Task<IDictionary<string, string>> GetPositions()
        {
            var client = new ApiClient(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
            IEnumerable<Position> positions = await client.GetPositions();

            return new Dictionary<string, string>(
                positions
                    .OrderBy(p => p.PositionId)
                    .Select(p =>
                        new KeyValuePair<string, string>(p.PositionId.ToString(), p.PositionName))
                    );
        }

        public static void ParseRequestException(DataException ex, ModelStateDictionary modelStates)
        {
            if (ex.Message.StartsWith('{'))
            {
                ExceptionResponse? respEx = JsonConvert.DeserializeObject<ExceptionResponse>(ex.Message);
                if (respEx != null)
                {
                    if (!string.IsNullOrEmpty(respEx.Detail))
                    {
                        string[] items = respEx.Detail.Split(':');
                        if (items.Length == 2)
                        {
                            modelStates.AddModelError(items[0], items[1]);
                        }
                        else
                        {
                            modelStates.AddModelError("", respEx.Detail);
                        }
                    }
                    else
                    {
                        foreach (KeyValuePair<string, string[]> item in respEx.Errors)
                        {
                            foreach (string text in item.Value)
                            {
                                foreach (ModelStateEntry? modelState in modelStates.Values)
                                {
                                    if (!modelState.Errors.Contains(new ModelError(text)))
                                    {
                                        modelStates.AddModelError(item.Key, text);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                string[] items = ex.Message
                    .Replace("\"", "").Split(':');
                if (items.Length == 2)
                {
                    modelStates.AddModelError(items[0], items[1]);
                }
                else
                {
                    modelStates.AddModelError("", ex.Message);
                }
            }
        }
    }

}
