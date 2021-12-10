using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections;

namespace IBL.BO
{
   static class Tools
        
    {
        public static string ToStringProperty<T>(this T t, string suffix = ""){
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                var value = item.GetValue(t, null);
                if (value is IEnumerable)
                {
                    foreach (var item1 in (IEnumerable)value)
                        str += item1.ToStringProperty("  ");
                }
                else
                    str += "\n" + suffix + item.Name + ": " + value;
            }
            return str;
        }
    }
}
