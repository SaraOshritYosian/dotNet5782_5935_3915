using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DalObject
{
   static class cloning
    {
        internal static T Clone<T>(this T original) where T : new()
        {
            T copyToObject =new T();
            foreach(propertyInFo propertyInFo in typeof(T).ge()//צריךpropertyInFo 

        }
    }
}
