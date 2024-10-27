using System.Globalization;
using System.Resources;

namespace INFOPC.Resources
{
    public class Resources
    {
        private static ResourceManager resourceManager = new ResourceManager("INFOPC.Resources", typeof(Resources).Assembly);

        public static string GetString(string name)
        {
            return resourceManager.GetString(name, CultureInfo.CurrentUICulture);
        }
    }
}
