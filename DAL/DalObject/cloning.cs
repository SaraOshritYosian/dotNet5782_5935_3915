﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalObject
{
    static class cloning
    {
        internal static T Clone<T>(this T original) where T : new()
        {
            T copyToObject = new T();
            foreach (PropertyInfo propertyInFo in typeof(T).GetProperties())
                propertyInFo.SetValue(copyToObject, propertyInFo.GetValue(original, null), null);

            return copyToObject;
        }
    }
}

