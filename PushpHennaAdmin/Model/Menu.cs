using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class Menu
    {
    }

    public class MenuModel
    {
        public int menuid { get; set; }
        public int submenuid { get; set; }
        public string name { get; set; }
        public string pageurl { get; set; }
        public string actionstr { get; set; }
        public string arrowsubmenuclass { get; set; }
        public string iconclass { get; set; }
        public string submenuclass { get; set; }
        public int srno { get; set; }
        public bool isactive { get; set; }
        public string subpageurl { get; set; }
        public string createdby { get; set; }
        public string ipaddress { get; set; }
        public string resultmsg { get; set; }
        public string subpageurls { get; set; }
    }
}
