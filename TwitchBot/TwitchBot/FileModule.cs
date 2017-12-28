using System.IO;
using System.Text;
using System.Web.Script.Serialization;
namespace TwitchBot
{
    public static class FileModule
    {
        public static JavaScriptSerializer json = new JavaScriptSerializer();
        public static T JsonToClass<T>(string path)
        {
            return json.Deserialize<T>(File.ReadAllText(path, Encoding.UTF8));
        }
    }
}
