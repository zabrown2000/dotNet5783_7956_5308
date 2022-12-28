using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
                            //ADD DOCUMENTATION
namespace BO
{
    static class Tools
    {
        public static String ToStringProperty<T>(this T t, String suffix = "")
        {
            String str = "";
            foreach (PropertyInfo prop in t.GetType().GetProperties())
            {
                var value = prop.GetValue(t, null);
                if (value is String) //need to seperate string from IEnumerable otherwise value gets overwritten to ""
                {
                    str += $"{prop.Name}: {value}{suffix}\n";
                } else if (value is IEnumerable) //if value is a list, print each item in the list
                {
                    foreach (var item in (IEnumerable)value)
                        str += item.ToStringProperty("   ");
                }
                else //otherwise it just adds the field to the final string
                {
                    str += "\n" + suffix + prop.Name + ": " + value;
                }
            }
            return str;
        }
    }
}
