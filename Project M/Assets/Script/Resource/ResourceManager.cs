using ProjectM.Define;
using ProjectM.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

namespace ProjectM.Resource
{
    /// <summary>
    /// ��Ÿ�ӿ� �ʿ��� ���ҽ��� �ҷ����� ����� ���� Ŭ����
    /// </summary>
    public class ResourceManager : Singleton<ResourceManager>
    {
        public void Initialize()
        {
            LoadAllAtlas();
            LoadAllPrefabs();
        }

        /// <summary>
        /// ��θ� �̿��ؼ� ���ҽ� ���� ���� ��������
        /// �������� �޼���
        /// </summary>
        /// <param name="path"> ����� ��� </param>
        /// <returns></returns>
        public GameObject LoadObject(string path)
        {
            // ���� ���� �ȿ� ���ҽ� ������ �����Ѵٸ�
            // �ش� ��η� ���� path�� �о�ɴϴ�.
            // �ش� ��ο� ������ GameObject ���·� �θ� �� �ִٸ�
            // �ҷ��´�.
            return Resources.Load<GameObject>(path);
        }

        /// <summary>
        /// ���ҽ� ���� ���� ��� ��Ʋ�󽺸� �ҷ���
        /// ��������Ʈ �δ��� ����ϴ� �޼���
        /// </summary>
        private void LoadAllAtlas()
        {
            var portraitAtlas = Resources.LoadAll<SpriteAtlas>("Atlas/PortraitAtlas");
            SpriteLoader.SetAtlas(portraitAtlas);

            var uiAtlas = Resources.LoadAll<SpriteAtlas>("Atlas/UIAtlas");
            SpriteLoader.SetAtlas(uiAtlas);

            var itemAtlas = Resources.LoadAll<SpriteAtlas>("Atlas/ItemAtlas");
            SpriteLoader.SetAtlas(itemAtlas);
        }

        /// <summary>
        /// �ΰ��ӿ��� ����� �������� Ǯ�� ��� ��û�� ������ �޼���
        /// </summary>
        public void LoadAllPrefabs()
        {
            // ����� ���߿� ����
        }

        /// <summary>
        /// ������Ʈ Ǯ�� ����� �������� �����ϰ� Ǯ�� ����ϴ� �޼���
        /// </summary>
        /// <typeparam name="T"> �ε� �ϰ��� �ϴ� �������� ���� Ÿ�� </typeparam>
        /// <param name="poolType"> ����ϰ��� �ϴ� Ÿ�� </param>
        /// <param name="path"> �������� �����ϴ� ��� </param>
        /// <param name="poolCount"> Ǯ�� ����� ���� </param>
        /// <param name="loadComplete"> �۾��� ������ ������ �۾� </param>
        public void LoadPoolableObject<T>(PoolType poolType, string path, int poolCount = 1,
            Action loadComplete = null)
            where T : MonoBehaviour, IPoolableObject
        {
            // �������� �����´�
            var obj = LoadObject(path);

            // ������ �������� T Ÿ���� �����´�
            var tComponent = obj.GetComponent<T>();

            // Ǯ �Ŵ����� ������ �������� ����Ѵ�
            ObjectPoolManager.Instance.RegistPool<T>(poolType, tComponent, poolCount);

            // �� �۾��� ��� ���� �Ŀ� ���� ��ų �۾��� �ִٸ� �����Ѵ�
            loadComplete?.Invoke();
        }
    }
}