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
    /// ���� ȭ���� ��ư�� �����ϴ� Ŭ����
    /// </summary>
    public class StartBtnManager : MonoBehaviour
    {
        /// <summary>
        /// ���� ȭ���� ��ư���� ����� ����ϴ� �޼���
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
