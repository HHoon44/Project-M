using ProjectM.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.UI
{
    /// <summary>
    /// 모든 UIWindow클래스를 관리하는 매니저 클래스
    /// </summary>
    public class UIWindowManager : Singleton<UIWindowManager>
    {
        /// <summary>
        /// 활성화 상태의 UI 목록
        /// </summary>
        private List<UIWindow> totalOpenWindow = new List<UIWindow>();

        /// <summary>
        /// 매니저에 등록된 UI 목록
        /// </summary>
        private List<UIWindow> totalUIWindow = new List<UIWindow>();

        /// <summary>
        /// 매니저에 등록 시 캐싱한 모든 UI가 담긴 목록
        /// </summary>
        private Dictionary<string, UIWindow> cachedTotalUIWindowDic = new Dictionary<string, UIWindow>();

        /// <summary>
        /// 매니저에 등록된 UI에 인스턴스 접근 시
        /// 해당 인스턴스를 캐싱하여 담아둘 딕셔너리
        /// 매니저를 이용해서 특정 인스턴스 접근 메서드를 사용할 시에 내가 코드로 직접 접근한 UI들만 담긴다
        /// </summary>
        private Dictionary<string, UIWindow> cachedInstanceDic = new Dictionary<string, UIWindow>();

        public void Initialize()
        {
            InitAllWindow();
        }

        /// <summary>
        /// 매니저에 등록된 모든 UI를 초기화 하는 메서드
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
        /// 새로운 UI를 매니저에 등록하는 메서드
        /// </summary>
        /// <param name="uiWindow"> 등록하려는 UI </param>
        public void AddTotalWindow(UIWindow uiWindow)
        {
            // UI의 이름을 가져온다
            var key = uiWindow.GetType().Name;

            bool hasKey = false;

            // UI 목록과 UI 캐싱 목록 또는 등록하려는 UI가 존재하는지 확인
            if (totalUIWindow.Contains(uiWindow) || cachedTotalUIWindowDic.ContainsKey(key))
            {
                if (cachedTotalUIWindowDic[key] != null)
                {
                    // 캐싱 되어 있으므로 return
                    return;
                }
                else
                {
                    hasKey = true;

                    // 키 값은 존재하나
                    // 참조하고 있는 인스턴스가 없으므로 리스트에서 제거
                    for (int i = 0; i < totalUIWindow.Count; i++)
                    {
                        if (totalUIWindow[i] == null)
                        {
                            totalUIWindow.RemoveAt(i);
                        }
                    }
                }
            }

            // UI를 등록
            totalUIWindow.Add(uiWindow);

            if (hasKey)
            {
                // 키가 있다면 UI의 인스턴스를 캐싱한다
                cachedInstanceDic[key] = uiWindow;
            }
            else
            {
                cachedTotalUIWindowDic.Add(key, uiWindow);
            }
        }

        /// <summary>
        /// 매니저에 등록된 UI를 반환하는 메서드
        /// </summary>
        /// <typeparam name="T"> 반환하고자 하는 UI 타입 </typeparam>
        /// <returns></returns>
        public T GetWindow<T>() where T : UIWindow
        {
            // T 타입으로 key를 설정
            string key = typeof(T).Name;


            // key 값이 캐싱 전체목록에 없다면
            if (!cachedTotalUIWindowDic.ContainsKey(key))
            {
                return null;
            }

            // 현재 메서드를 통해 접근하는 UI는 모두 최종적으로 인스턴스 딕셔너리에 등록된다
            if (!cachedInstanceDic.ContainsKey(key))
            {
                // 인스턴스 딕셔너리에 요청한 UI가 없다면
                // 전체 UI 목록에서 요청한 UI를 가져와 등록해준다
                cachedInstanceDic.Add(key, (T)Convert.ChangeType(cachedTotalUIWindowDic[key], typeof(T)));
            }
            else if (cachedTotalUIWindowDic[key].Equals(null))
            {
                // 인스턴스 딕셔너리에 요청한 key 값은 존재하나 인스턴스가 없다면
                // 전체 UI 목록에서 요청한 UI를 가져와 등록해준다
                cachedInstanceDic[key] = (T)Convert.ChangeType(cachedTotalUIWindowDic[key], typeof(T));
            }

            // 반환할땐 파생클래스 타입으로 변환하여 반화
            return (T)cachedInstanceDic[key];
        }

        /// <summary>
        /// 활성화 한 UI를 활성화 목록에 추가하는 메서드
        /// </summary>
        /// <param name="uiWindow"> 활성화 된 UI </param>
        public void AddOpenWindow(UIWindow uiWindow)
        {
            if (!totalOpenWindow.Contains(uiWindow))
            {
                totalUIWindow.Add(uiWindow);
            }
        }

        /// <summary>
        /// 비활성화 한 UI를 활성화 목록에서 제거하는 메서드
        /// </summary>
        /// <param name="uiWindow"> 비활성화 된 UI </param>
        public void RemoveOpenWindow(UIWindow uiWindow)
        {
            if (totalOpenWindow.Contains(uiWindow))
            {
                totalOpenWindow.Remove(uiWindow);
            }
        }
    }
}