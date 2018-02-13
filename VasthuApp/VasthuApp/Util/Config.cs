using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VasthuApp.Util
{
    public static class Config
    {
        public static string PrimaryColor
        {
            get
            {
                return "#009ef0";
            }
        }

        public static bool IsSecure { get; set; }
    }
}
