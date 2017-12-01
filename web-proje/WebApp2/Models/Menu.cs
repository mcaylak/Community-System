using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp2.Models
{
    public class Menu
    {
        public int MenuID { get; set; }
        public int MenuSira { get; set; }
        public string MenuAdi { get; set; }
        public bool MenuDurum { get; set; }
    }
}