using ProjectM.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.UI
{
    /// <summary>
    /// ��� UIWindowŬ������ �����ϴ� �Ŵ��� Ŭ����
    /// </summary>
    public class UIWindowManager : Singleton<UIWindowManager>
    {
        /// <summary>
        /// Ȱ��ȭ ������ UI ���
        /// </summary>
        private List<UIWindow> totalOpenWindow = new List<UIWindow>();

        /// <summary>
        /// �Ŵ����� ��ϵ� UI ���
        /// </summary>
        private List<UIWindow> totalUIWindow = new List<UIWindow>();

        /// <summary>
        /// �Ŵ����� ��� �� ĳ���� ��� UI�� ��� ���
        /// </summary>
        private Dictionary<string, UIWindow> cachedTotalUIWindowDic = new Dictionary<string, UIWindow>();

        /// <summary>
        /// �Ŵ����� ��ϵ� UI�� �ν��Ͻ� ���� ��
        /// �ش� �ν��Ͻ��� ĳ���Ͽ� ��Ƶ� ��ųʸ�
        /// �Ŵ����� �̿��ؼ� Ư�� �ν��Ͻ� ���� �޼��带 ����� �ÿ� ���� �ڵ�� ���� ������ UI�鸸 ����
        /// </summary>
        private Dictionary<string, UIWindow> cachedInstanceDic = new Dictionary<string, UIWindow>();

        public void Initialize()
        {
            InitAllWindow();
        }

        /// <summary>
        /// �Ŵ����� ��ϵ� ��� UI�� �ʱ�ȭ �ϴ� �޼���
        /// </summary>
        public void InitAllWindow()
        {
            for (int i = 0; i < totalUIWindow.Count; i++)
            {
                if (totalUIWindow[i] != null)
                {
                    totalUIWindow[i].InitWindow();
                }
            }
        }

        /// <summary>
        /// ���ο� UI�� �Ŵ����� ����ϴ� �޼���
        /// </summary>
        /// <param name="uiWindow"> ����Ϸ��� UI </param>
        public void AddTotalWindow(UIWindow uiWindow)
        {
            // UI�� �̸��� �����´�
            var key = uiWindow.GetType().Name;

            bool hasKey = false;

            // UI ��ϰ� UI ĳ�� ��� �Ǵ� ����Ϸ��� UI�� �����ϴ��� Ȯ��
            if (totalUIWindow.Contains(uiWindow) || cachedTotalUIWindowDic.ContainsKey(key))
            {
                if (cachedTotalUIWindowDic[key] != null)
                {
                    // ĳ�� �Ǿ� �����Ƿ� return
                    return;
                }
                else
                {
                    hasKey = true;

                    // Ű ���� �����ϳ�
                    // �����ϰ� �ִ� �ν��Ͻ��� �����Ƿ� ����Ʈ���� ����
                    for (int i = 0; i < totalUIWindow.Count; i++)
                    {
                        if (totalUIWindow[i] == null)
                        {
                            totalUIWindow.RemoveAt(i);
                        }
                    }
                }
            }

            // UI�� ���
            totalUIWindow.Add(uiWindow);

            if (hasKey)
            {
                // Ű�� �ִٸ� UI�� �ν��Ͻ��� ĳ���Ѵ�
                cachedInstanceDic[key] = uiWindow;
            }
            else
            {
                cachedTotalUIWindowDic.Add(key, uiWindow);
            }
        }

        /// <summary>
        /// �Ŵ����� ��ϵ� UI�� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <typeparam name="T"> ��ȯ�ϰ��� �ϴ� UI Ÿ�� </typeparam>
        /// <returns></returns>
        public T GetWindow<T>() where T : UIWindow
        {
            // T Ÿ������ key�� ����
            string key = typeof(T).Name;


            // key ���� ĳ�� ��ü��Ͽ� ���ٸ�
            if (!cachedTotalUIWindowDic.ContainsKey(key))
            {
                return null;
            }

            // ���� �޼��带 ���� �����ϴ� UI�� ��� ���������� �ν��Ͻ� ��ųʸ��� ��ϵȴ�
            if (!cachedInstanceDic.ContainsKey(key))
            {
                // �ν��Ͻ� ��ųʸ��� ��û�� UI�� ���ٸ�
                // ��ü UI ��Ͽ��� ��û�� UI�� ������ ������ش�
                cachedInstanceDic.Add(key, (T)Convert.ChangeType(cachedTotalUIWindowDic[key], typeof(T)));
            }
            else if (cachedTotalUIWindowDic[key].Equals(null))
            {
                // �ν��Ͻ� ��ųʸ��� ��û�� key ���� �����ϳ� �ν��Ͻ��� ���ٸ�
                // ��ü UI ��Ͽ��� ��û�� UI�� ������ ������ش�
                cachedInstanceDic[key] = (T)Convert.ChangeType(cachedTotalUIWindowDic[key], typeof(T));
            }

            // ��ȯ�Ҷ� �Ļ�Ŭ���� Ÿ������ ��ȯ�Ͽ� ��ȭ
            return (T)cachedInstanceDic[key];
        }

        /// <summary>
        /// Ȱ��ȭ �� UI�� Ȱ��ȭ ��Ͽ� �߰��ϴ� �޼���
        /// </summary>
        /// <param name="uiWindow"> Ȱ��ȭ �� UI </param>
        public void AddOpenWindow(UIWindow uiWindow)
        {
            if (!totalOpenWindow.Contains(uiWindow))
            {
                totalUIWindow.Add(uiWindow);
            }
        }

        /// <summary>
        /// ��Ȱ��ȭ �� UI�� Ȱ��ȭ ��Ͽ��� �����ϴ� �޼���
        /// </summary>
        /// <param name="uiWindow"> ��Ȱ��ȭ �� UI </param>
        public void RemoveOpenWindow(UIWindow uiWindow)
        {
            if (totalOpenWindow.Contains(uiWindow))
            {
                totalOpenWindow.Remove(uiWindow);
            }
        }
    }
}