using ProjectM.Define;
using ProjectM.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectM
{
    public class StartController : MonoBehaviour
    {
        private bool allLoaded;         // ��� ������ �ε尡 ��������
        private bool loadComplete;      // ���� ������ �ε尡 ��������

        private IntroPhase introPhase = IntroPhase.Start;   // ���� ������

        /// <summary>
        /// ���� ������ �ε� �� ���ǿ� ���� ���� �����
        /// �ҷ����� �۾��� �����ϴ� ������Ƽ
        /// </summary>
        public bool LoadComplete
        {
            get => loadComplete;

            set
            {
                loadComplete = value;

                // ���� ������ �ε尡 ��������
                // ��� ������ �ε尡 ������ �ʾҴٸ�
                if (loadComplete && !allLoaded)
                {
                    // ���� ����� �ҷ����� �۾��� �����մϴ�
                    NextPhase();
                }
            }
        }

        /// <summary>
        /// ù ����� �ҷ����� �۾��� �ϴ� �޼���
        /// </summary>
        public void Initialize()
        {
            OnPhase(introPhase);
        }

        /// <summary>
        /// ���� ����� ���� ������ �����ϴ� �޼���
        /// </summary>
        /// <param name="introPhase"> ���� ������ </param>
        private void OnPhase(IntroPhase introPhase)
        {
            switch (introPhase)
            {
                case IntroPhase.Start:
                    LoadComplete = true;
                    break;

                case IntroPhase.ApllicationSetting:
                    GameManager.Instance.OnApplicationSetting();
                    LoadComplete = true;
                    break;

                case IntroPhase.StaticData:
                    // GameManager.SD.Initialize();
                    LoadComplete = true;
                    break;

                case IntroPhase.Resource:
                    ResourceManager.Instance.Initialize();
                    LoadComplete = true;
                    break;

                case IntroPhase.UI:
                    // UIWindowManager.Instance.Initialize();
                    LoadComplete = true;
                    break;

                case IntroPhase.Complte:
                    SceneManager.LoadScene(SceneType.InGame.ToString());

                    allLoaded = true;
                    LoadComplete = true;
                    break;
            }
        }

        /// <summary>
        /// ����� ���� ������� �����ϴ� �޼���
        /// </summary>
        private void NextPhase()
        {
            StartCoroutine(WaitForSeconds());

            IEnumerator WaitForSeconds()
            {
                yield return new WaitForSeconds(.5f);
                LoadComplete = false;
                OnPhase(introPhase++);
            }
        }
    }
}