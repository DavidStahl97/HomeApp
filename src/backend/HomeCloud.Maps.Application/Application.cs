using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application
{
    public static class Application
    {
        public static Assembly GetAssembly()
        {
            return typeof(Application).Assembly;
        }
    }
}
