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
        private bool allLoaded;         // 모든 페이즈 로드가 끝났는지
        private bool loadComplete;      // 현재 페이즈 로드가 끝났는지

        private IntroPhase introPhase = IntroPhase.Start;   // 현재 페이즈

        /// <summary>
        /// 현재 페이즈 로드 후 조건에 따라 다음 페이즈를
        /// 불러오는 작업을 실행하는 프로퍼티
        /// </summary>
        public bool LoadComplete
        {
            get => loadComplete;

            set
            {
                loadComplete = value;

                // 현재 페이즈 로드가 끝났지만
                // 모든 페이즈 로드가 끝나지 않았다면
                if (loadComplete && !allLoaded)
                {
                    // 다음 페이즈를 불러오는 작업을 실행합니다
                    NextPhase();
                }
            }
        }

        /// <summary>
        /// 첫 페이즈를 불러오는 작업을 하는 메서드
        /// </summary>
        public void Initialize()
        {
            OnPhase(introPhase);
        }

        /// <summary>
        /// 현재 페이즈에 대한 로직을 실행하는 메서드
        /// </summary>
        /// <param name="introPhase"> 현재 페이즈 </param>
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
        /// 페이즈를 다음 페이즈로 변경하는 메서드
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