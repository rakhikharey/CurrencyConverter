using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Cconvert
{
    public class Common
    {
        JavaScriptSerializer objJson = new JavaScriptSerializer();

        public string GetErrorMessage(string msg)
        {
            return objJson.Serialize(new Response
            {
                status = "error",
                val = msg
            });
        }

        public string GetResponseJson(object value)
        {
            return objJson.Serialize(new Response
            {
                status = "success",
                val = value
            });
        }      


    }
    public class Response
    {
        public string status { get; set; }
        public object val { get; set; }
    }
}