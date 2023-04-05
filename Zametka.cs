using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAP_Ex4
{
    public class Zametka
    {
        public string Name { get; set; }
        public double sum { get; set; }
        public Boolean Tip { get; set; } //True - доход, False - расход
       
        public string vid { get; set; }
        public DateTime vrema { get; set; }

        public Zametka(string n, double s, Boolean t, string v, DateTime vr) //Сделали конструктор
        {
            Name= n; Tip = t; sum= s; vid = v; vrema = vr;
        }
       
        
    }
}
