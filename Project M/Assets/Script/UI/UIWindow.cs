using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.UI
{
    /// <summary>
    /// 모든 UI의 베이스 클래스
    /// 조그만한 팝업, UI내의 UIElement는 제외
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class UIWindow : MonoBehaviour
    {
        public bool isOpen;     // 해당 UI의 활성화 상태

        private CanvasGroup cachedCanvasGroup;

        /// <summary>
        /// 캔버스 그룹을 이용하여 UI를 비/활성화 하는 효과를 준다 (알파값을 설정)
        /// 비활성화 시 실제로 UI를 비활성화 하는 것이라 아니라
        /// UI 입력을 차단한다 (블록 레이캐스트)
        /// </summary>
        public CanvasGroup CachedCanvasGroup
        {
            get
            {
                if (cachedCanvasGroup == null)
                {
                    cachedCanvasGroup = GetComponent<CanvasGroup>();
                }

                return cachedCanvasGroup;
            }
        }

        public virtual void Start()
        {
            InitWindow();
        }

        /// <summary>
        /// UI를 매니저에 등록하고 초기화 하는 메서드
        /// </summary>
        public virtual void InitWindow()
        {
           // UIWindowManager.Instance.AddTotalWindow(this);

            if (isOpen)
            {
                Open(true);
            }
            else
            {
                Close(true);
            }
        }

        /// <summary>
        /// UI를 활성화 하고 
        /// 활성화 UI를 매니저에 캐싱 요청하는 메서드
        /// </summary>
        /// <param name="force"> 활성화 여부 </param>
        public virtual void Open(bool force = false)
        {
            if (!isOpen || force)
            {
                isOpen = true;
               // UIWindowManager.Instance.AddOpenWindow(this);
                SetCanvasGroup(true);
            }
        }

        /// <summary>
        /// UI를 비활성화 하고
        /// 비활성화 하고자 하는 UI를 캐싱 목록에서 제거 요청하는 메서드
        /// </summary>
        /// <param name="force"> 활성화 여부 </param>
        public virtual void Close(bool force = false)
        {
            if (isOpen || force)
            {
                isOpen = false;
               // UIWindowManager.Instance.RemoveOpenWindow(this);
                SetCanvasGroup(false);
            }
        }

        /// <summary>
        /// 활성화 여부에 따라 캔버스 그룹의 필드를 설정하는 메서드
        /// </summary>
        /// <param name="isActive"> 활성화 여부 </param>
        private void SetCanvasGroup(bool isActive)
        {
            // 불투명도를 설정
            CachedCanvasGroup.alpha = Convert.ToInt32(isActive);

            // 입력을 받을 지에 대한 여부
            cachedCanvasGroup.interactable = isActive;

            // 레이캐스트를 위한 콜라이더를 적용할지에 대한 여부
            cachedCanvasGroup.blocksRaycasts = isActive;
        }
    }
}