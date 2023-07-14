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
    /// 시작 화면의 버튼을 관리하는 클래스
    /// </summary>
    public class StartBtnManager : MonoBehaviour
    {
        /// <summary>
        /// 시작 화면의 버튼들의 기능을 담당하는 메서드
        /// </summary>
        public void ButtonOption()
        {
            switch (EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex())
            {
                case 0:
                    SceneManager.LoadScene(SceneType.StartLoading.ToString());
                    break;
            }
        }
    }
}
