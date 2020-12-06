using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class DashBoardModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string WorkFor { get; set; }
        public string WorkForIdStr { get; set; }        
        public int CreatedBy { get; set; }
    }
}
