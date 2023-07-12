using ProjectM.Define;
using ProjectM.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

namespace ProjectM.Util
{
    /// <summary>
    /// 오브젝트 풀을 관리할 매니저 클래스
    /// </summary>
    public class ObjectPoolManager : Singleton<ObjectPoolManager>
    {
        /// <summary>
        /// 모든 풀을 담아놓을 딕셔너리
        /// </summary>
        private Dictionary<PoolType, object> poolDic = new Dictionary<PoolType, object>();

        public void RegistPool<T>(PoolType type, T obj, int poolCount = 1)
            where T : MonoBehaviour, IPoolableObject
        {
            // 오브젝트를 등록할 풀
            ObjectPool<T> pool = null;

            if (poolDic.ContainsKey(type))
            {
                pool = poolDic[type] as ObjectPool<T>;
            }
            else
            {
                pool = new ObjectPool<T>();
                poolDic.Add(type, pool);
            }

            if (pool.holder == null)
            {
                pool.holder = new GameObject($"{type.ToString()}Holder").transform;
                pool.holder.parent = transform;
                pool.holder.position = Vector3.zero;
            }

            for (int i = 0; i < poolCount; i++)
            {
                var poolableObject = Instantiate(obj);
                poolableObject.name = obj.name;
                poolableObject.transform.SetParent(pool.holder);
                poolableObject.gameObject.SetActive(false);

                pool.RegistPoolableObject(poolableObject);
            }
        }

        public ObjectPool<T> GetPool<T>(PoolType type)
            where T : MonoBehaviour, IPoolableObject
        {
            if (!poolDic.ContainsKey(type))
            {
                return null;
            }

            return poolDic[type] as ObjectPool<T>;
        }

        public void ClearPool<T>(PoolType type)
            where T : MonoBehaviour, IPoolableObject
        {
            var pool = GetPool<T>(type)?.Pool;

            if (pool == null)
            {
                return;
            }

            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i] != null)
                {
                    Destroy(pool[i].gameObject);
                }
            }

            pool.Clear();
        }
    }
}