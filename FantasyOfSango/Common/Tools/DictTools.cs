using Common.ServerCode;
using System;
using System.Collections.Generic;

//Developer : SangonomiyaSakunovi
//Discription:

namespace Common.Tools
{
    public class DictTools
    {
        //Get DictValue by TKey, return the TValue, careful the Dict as firstPatameter
        public static T_Value GetDictValue<T_Key, T_Value>(Dictionary<T_Key, T_Value> dict, T_Key key)
        {
            T_Value value;
            bool isGetValue = dict.TryGetValue(key, out value);
            if (isGetValue)
            {
                return value;
            }
            else
            {
                return default(T_Value);
            }
        }        
        //Get Info
        public static string GetStringValue(Dictionary<byte, object> dict, byte key)
        {
            return GetDictValue<byte, object>(dict, key) as string;
        }
        public static int GetIntValue(Dictionary<byte, object> dict, byte key)
        {
            return (int)GetDictValue<byte, object>(dict, key);
        }
    }
}
