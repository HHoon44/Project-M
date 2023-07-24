using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.Util
{
    /// <summary>
    /// ������ ����ȭ�� ����� Ŭ����
    /// </summary>
    public static class SerializationUtil
    {
        /// <summary>
        /// �Ķ���ͷ� ���� ���̽��� ������ TŸ���� ������� ������ȭ �Ͽ� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <typeparam name="T"> ���� Ÿ�� </typeparam>
        /// <param name="json"> ������ȭ �Ϸ��� ���̽� </param>
        /// <returns></returns>
        public static List<T> FromJson<T>(string json)
        { 
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
        
        /// <summary>
        /// �Ķ���ͷ� ���� ���̽��� ������ TŸ������ ������ȭ �Ͽ� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <typeparam name="T"> ���� Ÿ�� </typeparam>
        /// <param name="json"> ������ȭ �Ϸ��� ���̽� </param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// T Ÿ�� �����͸� ���̽����� ����ȭ �Ͽ� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <typeparam name="T"> ���� Ÿ�� </typeparam>
        /// <param name="obj"> ����ȭ �Ϸ��� ���̽� </param>
        /// <returns></returns>
        public static string ToJson<T>(T obj)
        { 
            return JsonConvert.SerializeObject(obj);
        }
    }
}