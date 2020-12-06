using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushpHennaAdmin.Model
{
    public class Category
    {
    }

    public class CategoryModel : CommanProperty
    {
        public int CATID { get; set; }
        public int MSTCATID { get; set; }
        public string SIZE_STR { get; set; }
        public string WEIGHT_STR { get; set; }
        public string MATERIAL_STR { get; set; }
    }
}
