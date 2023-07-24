using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.Util
{
    /// <summary>
    /// 데이터 직렬화를 도우는 클래스
    /// </summary>
    public static class SerializationUtil
    {
        /// <summary>
        /// 파라미터로 받은 제이슨을 지정한 T타입의 목록으로 역직렬화 하여 반환하는 메서드
        /// </summary>
        /// <typeparam name="T"> 지정 타입 </typeparam>
        /// <param name="json"> 역직렬화 하려는 제이슨 </param>
        /// <returns></returns>
        public static List<T> FromJson<T>(string json)
        { 
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
        
        /// <summary>
        /// 파라미터로 받은 제이슨을 지정한 T타입으로 역직렬화 하여 반환하는 메서드
        /// </summary>
        /// <typeparam name="T"> 지정 타입 </typeparam>
        /// <param name="json"> 역직렬화 하려는 제이슨 </param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// T 타입 데이터를 제이슨으로 직렬화 하여 반환하는 메서드
        /// </summary>
        /// <typeparam name="T"> 지정 타입 </typeparam>
        /// <param name="obj"> 직렬화 하려는 제이슨 </param>
        /// <returns></returns>
        public static string ToJson<T>(T obj)
        { 
            return JsonConvert.SerializeObject(obj);
        }
    }
}