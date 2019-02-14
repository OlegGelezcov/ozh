using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ozh.Tools.IoC
{
    /// <summary>
    /// How object create - every request new object or singleton object for whole lifecycle of app
    /// </summary>
    public enum ObjectLifecycle
    {
        Singleton,
        Transient
    }
}
