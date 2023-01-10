using System;

namespace WebApp.Helper
{
    public static class StringinObj
    {
        public static string GetStringinObject(object obj)
        {
            try
            {
                if (obj == null)
                    return "Object is Empty";

                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            catch (Exception r)
            {
                return "Erorr: " + r.Message;
            }
        }
    }
}
