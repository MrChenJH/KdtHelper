using System;
using System.Collections.Generic;
using System.Text;

namespace KdtHelper.Core
{
    /// <summary>
    /// KDT驱动器设置
    /// </summary>
    public class KdtDriver
    {
        public KdtDriver()
        { }

        public KdtDriver(string _prefix, string _script, bool _canautono)
        {
            this.auto = _canautono;
            this.prefix = _prefix;
            this.script = _script;
        }

        public string prefix { get; set; }

        public bool auto { get; set; }

        public string script { get; set; }
    }
}
