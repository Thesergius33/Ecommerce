using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Mobile.Services
{
    public static class ConfigService
    {
        public static string ApiBaseUrl
        {
            get
            {
#if DEBUG
#if ANDROID

                return "https://192.168.210.80:7061/api";
#elif IOS
                return "https://localhost:7061/api";
#else
                // Windows, MacCatalyst, etc.
                return "https://localhost:7061/api";
#endif
#else
                // Producción
                return "https://your-production-domain.com/api";
#endif
            }
        }
    }
}