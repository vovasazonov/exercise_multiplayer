using System;
using Newtonsoft.Json.Linq;

namespace Models
{
    public class JsonCastObject : ICustomCastObject
    {
        public TResult To<TResult>(object obj)
        {
            TResult result;
            
            if (obj is null)
            {
                result = default(TResult);
            }
            else if (obj.GetType().IsPrimitive)
            {
                result = (TResult) Convert.ChangeType(obj, typeof(TResult));
            }
            else if (obj is JObject jObject)
            {
                result = jObject.ToObject<TResult>();
            }
            else
            {
                result = (TResult) obj;
            }

            return result;
        }
    }
}