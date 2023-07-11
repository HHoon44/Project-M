using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.Util
{
    /// <summary>
    /// 싱글턴의 베이스 클래스
    /// </summary>
    /// <typeparam name="T"> 싱글턴의 파생 클래스 타입 </typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        /// <summary>
        /// 싱글턴 인스턴스를 찾거나 과정 중, 다른 스레드에서 사용 중인지 판단할 객체
        /// </summary>
        private static object syncObject = new object();

        private static T instance;

        /// <summary>
        /// 외부에서 인스턴스에 접근하기 위한 프로퍼티
        /// </summary>
        public static T Instance
        {
            get
            {
                // 인스턴스가 없다면
                if (instance == null)
                {
                    // 인스턴스를 찾습니다.
                    lock (syncObject)
                    {
                        instance = FindObjectOfType<T>();
                    }

                    // 찾아도 인스턴스가 없다면
                    if (instance == null)
                    {
                        // 인스턴스를 새로 생성해서 넣어줍니다.
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }

                // 인스턴스를 반환 해줍니다.
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