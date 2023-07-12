using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.Util
{
    /// <summary>
    /// ������Ʈ Ǯ���� �����ϴ� Ŭ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPool<T> where T : MonoBehaviour, IPoolableObject
    {
        /// <summary>
        /// ������ ��ü�� ��Ƴ��� �θ� ������Ʈ
        /// </summary>
        public Transform holder;

        /// <summary>
        /// Ǯ�� ��Ƴ��� ����Ʈ
        /// </summary>
        public List<T> Pool { get; set; } = new List<T>();

        /// <summary>
        /// Ǯ���� ���� ������ ��ü�� �����ϴ���
        /// </summary>
        public bool canRecycle => Pool.Find(obj => obj.CanReCycle) != null;

        /// <summary>
        /// Ǯ���� ���ο� ������Ʈ�� ����ϴ� �޼���
        /// </summary>
        /// <param name="obj"> Ǯ�� ����� ������Ʈ </param>
        public void RegistPoolableObject(T obj) => Pool.Add(obj);

        /// <summary>
        /// ����� ������Ʈ�� Ǯ�� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <param name="obj"> Ǯ�� ��ȯ�� ������Ʈ </param>
        public void ReturnPoolableObject(T obj)
        {
            obj.transform.SetParent(holder);
            obj.gameObject.SetActive(false);
            obj.CanReCycle = true;
        }

        /// <summary>
        /// ��û�� ���� ������Ʈ�� ��ȯ���ִ� �޼���
        /// </summary>
        /// <param name="pred"> ������Ʈ�� ã�� ���� ���� </param>
        /// <returns></returns>
        public T GetPoolableObject(Func<T, bool> pred = null)
        {
            // ��� ������ ������Ʈ�� ���ٸ�
            if (!canRecycle)
            {
                // Ǯ�� ������Ʈ�� �����Ѵٸ� 0��° ������Ʈ�� ������ �����ɴϴ�.
                var protoObj = Pool.Count > 0 ? Pool[0] : null;

                // ������Ÿ�� ������Ʈ�� �����Ѵٸ�
                if (protoObj != null)
                {
                    // ������ ������ �̿��Ͽ� ���ο� ������Ʈ�� �ϳ� �����մϴ�.
                    var newObj = GameObject.Instantiate(protoObj.gameObject, holder);
                    newObj.name = protoObj.name;
                    newObj.SetActive(false);

                    // ������Ʈ�� Ǯ�� ����մϴ�.
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

            // ���� ����ҰŴϱ� false�� ǥ���մϴ�.
            recycleObj.CanReCycle = false;

            return recycleObj;
        }
    }
}
