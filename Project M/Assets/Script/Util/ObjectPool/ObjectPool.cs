using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.Util
{
    /// <summary>
    /// 오브젝트 풀링을 수행하는 클래스
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPool<T> where T : MonoBehaviour, IPoolableObject
    {
        /// <summary>
        /// 생성된 객체를 담아놓을 부모 오브젝트
        /// </summary>
        public Transform holder;

        /// <summary>
        /// 풀을 담아놓을 리스트
        /// </summary>
        public List<T> Pool { get; set; } = new List<T>();

        /// <summary>
        /// 풀에서 재사용 가능한 객체가 존재하는지
        /// </summary>
        public bool canRecycle => Pool.Find(obj => obj.CanReCycle) != null;

        /// <summary>
        /// 풀링할 새로운 오브젝트를 등록하는 메서드
        /// </summary>
        /// <param name="obj"> 풀에 등록할 오브젝트 </param>
        public void RegistPoolableObject(T obj) => Pool.Add(obj);

        /// <summary>
        /// 사용한 오브젝트를 풀에 반환하는 메서드
        /// </summary>
        /// <param name="obj"> 풀에 반환할 오브젝트 </param>
        public void ReturnPoolableObject(T obj)
        {
            obj.transform.SetParent(holder);
            obj.gameObject.SetActive(false);
            obj.CanReCycle = true;
        }

        /// <summary>
        /// 요청한 받은 오브젝트를 반환해주는 메서드
        /// </summary>
        /// <param name="pred"> 오브젝트를 찾기 위한 조건 </param>
        /// <returns></returns>
        public T GetPoolableObject(Func<T, bool> pred = null)
        {
            // 사용 가능한 오브젝트가 없다면
            if (!canRecycle)
            {
                // 풀에 오브젝트가 존재한다면 0번째 오브젝트의 정보를 가져옵니다.
                var protoObj = Pool.Count > 0 ? Pool[0] : null;

                // 프로토타입 오브젝트가 존재한다면
                if (protoObj != null)
                {
                    // 가져온 정보를 이용하여 새로운 오브젝트를 하나 생성합니다.
                    var newObj = GameObject.Instantiate(protoObj.gameObject, holder);
                    newObj.name = protoObj.name;
                    newObj.SetActive(false);

                    // 오브젝트를 풀에 등록합니다.
                    RegistPoolableObject(newObj.GetComponent<T>());
                }
                else
                {
                    return null;
                }
            }

            T recycleObj = (pred == null) ? (Pool.Count > 0 ? Pool[0] : null) : (Pool.Find(obj => pred(obj) && obj.CanReCycle));

            if (recycleObj == null)
            {
                return null;
            }

            // 이제 사용할거니깐 false로 표시합니다.
            recycleObj.CanReCycle = false;

            return recycleObj;
        }
    }
}
