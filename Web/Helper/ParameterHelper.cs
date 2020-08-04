using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Gosol.CMS.Web
{
    public class Parameter
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Parameter(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }

    public static class ParameterHelper
    {
        public static string GenParameterString(List<Parameter> paramList)
        {
            string result = string.Empty;

            foreach (Parameter param in paramList)
            {
                result += param.Name + "=" + param.Value + "&";                
            }

            if(result != string.Empty)
                result = result.Substring(0, result.Length - 1);

            return result;
        }
    }
}