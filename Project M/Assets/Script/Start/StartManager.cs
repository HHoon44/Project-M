using ProjectM.Define;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace ProjectM.Start
{
    /// <summary>
    /// 시작 화면을 관리하는 클래스
    /// </summary>
    public class StartManager : MonoBehaviour // IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// 시작 화면의 버튼들의 기능을 담당하는 메서드
        /// </summary>
        public void ButtonFun()
        {
            switch (EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex())
            {
                case 0:
                    SceneManager.LoadScene(SceneType.StartLoading.ToString());
                    break;
            }
        }


        private void Update()
        {
            // Debug.Log(EventSystem.current.IsPointerOverGameObject());

            IsPointerOverUI();
        }

        private bool IsPointerOverUI()
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> result = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointerEventData, result);

            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].gameObject.layer == LayerMask.NameToLayer("Button"))
                {
                    return true;
                }
            }

            return false;
        }

        /*
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Enter");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Exit");
        }
        */









    }
}
