using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.Util
{
    /// <summary>
    /// �̱����� ���̽� Ŭ����
    /// </summary>
    /// <typeparam name="T"> �̱����� �Ļ� Ŭ���� Ÿ�� </typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        /// <summary>
        /// �̱��� �ν��Ͻ��� ã�ų� ���� ��, �ٸ� �����忡�� ��� ������ �Ǵ��� ��ü
        /// </summary>
        private static object syncObject = new object();

        private static T instance;

        /// <summary>
        /// �ܺο��� �ν��Ͻ��� �����ϱ� ���� ������Ƽ
        /// </summary>
        public static T Instance
        {
            get
            {
                // �ν��Ͻ��� ���ٸ�
                if (instance == null)
                {
                    // �ν��Ͻ��� ã���ϴ�.
                    lock (syncObject)
                    {
                        instance = FindObjectOfType<T>();
                    }

                    // ã�Ƶ� �ν��Ͻ��� ���ٸ�
                    if (instance == null)
                    {
                        // �ν��Ͻ��� ���� �����ؼ� �־��ݴϴ�.
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }

                // �ν��Ͻ��� ��ȯ ���ݴϴ�.
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Destroy(instance);
            }
        }

        private void OnDestroy()
        {
            if (instance != null)
            {
                return;
            }

            instance = null;
        }

        public static bool HasInstance()
        {
            return instance ? true : false;
        }
    }
}