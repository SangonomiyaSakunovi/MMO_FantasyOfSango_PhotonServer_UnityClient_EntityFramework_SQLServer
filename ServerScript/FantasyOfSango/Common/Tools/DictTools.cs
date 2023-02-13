using Common.ServerCode;
using System;
using System.Collections.Generic;

//Developer : SangonomiyaSakunovi
//Discription:

namespace Common.Tools
{
    public class DictTools
    {
        /// <summary>
        /// Get DictValue by TKey, return the TValue, careful the Dict as firstPatameter, return is [T]
        /// </summary>
        /// <typeparam name="T_Key"></typeparam>
        /// <typeparam name="T_Value"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Get DictValue[string] by Key[byte], return is [string]
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetStringValue(Dictionary<byte, object> dict, byte key)
        {
            return GetDictValue<byte, object>(dict, key) as string;
        }
        /// <summary>
        /// Get DictValue[int] by Key[byte], return is [int]
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetIntValue(Dictionary<byte, object> dict, byte key)
        {
            return (int)GetDictValue<byte, object>(dict, key);
        }
    }
}
