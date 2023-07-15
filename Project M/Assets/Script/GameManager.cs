using ProjectM.Define;
using ProjectM.UI;
using ProjectM.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectM
{
    /// <summary>
    /// 게임에 사용되는 데이터를 관리하는 클래스
    /// + 게임의 씬 변경과 같은 게임의 큰 흐름을 컨트롤 하기도 함
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        public float loadProgress;      // 다음씬이 얼마나 준비 되어있는지에 대한 값

        protected override void Awake()
        {
            base.Awake();

            if (gameObject == null)
            {
                return;
            }

            // 씬이 변경 되더라도 파괴되지 않도록
            DontDestroyOnLoad(this);

            // 게임의 초기 세팅을 실행합니다
            var startController = FindObjectOfType<StartController>();
            startController?.Initialize();
        }

        public void OnApplicationSetting()
        {
            // 수직 동기화 끄기
            QualitySettings.vSyncCount = 0;

            // 랜덤 프레임을 60으로 설정
            Application.targetFrameRate = 60;

            // 장시간 대기 시에도 화면이 꺼지지 않도록 설정
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        /// <summary>
        /// 씬 전환을 실행하는 메서드
        /// </summary>
        /// <param name="sceneName"> 불러올 씬의 이름 </param>
        /// <param name="loadCroutine"> 씬 준비 작업 </param>
        /// <param name="loadComplete"> 준비 작업을 마친 뒤 추가 작업 </param>
        public void LoadScene(SceneType sceneName, IEnumerator loadCroutine = null,
            Action loadComplete = null)
        {
            StartCoroutine(WaitForLoad());

            IEnumerator WaitForLoad()
            {
                // 현재 진행 상태
                loadProgress = 0;

                // 다음 씬을 준비하기 위한 작업을 하기전에 로딩씬을 띄워놓는다
                yield return SceneManager.LoadSceneAsync(SceneType.Loading.ToString());

                // 불러오고자 하는 씬을 가져온 뒤에 비 활성화 해놓는다
                var asyncOper = SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Additive);
                asyncOper.allowSceneActivation = false;

                // 다음 씬을 준비하는데 필요한 작업이 있다면
                if (loadCroutine != null)
                {
                    // 해당 작업이 완료 될 때까지 대기
                    yield return StartCoroutine(loadCroutine);
                }

                while (!asyncOper.isDone)
                {
                    // 작업이 다 끝났다면
                    if (loadProgress >= .9f)
                    {
                        loadProgress = 1;

                        // 1초 정도 텀을 준다
                        yield return new WaitForSeconds(1f);

                        // 불러온 씬을 활성화
                        asyncOper.allowSceneActivation = true;
                    }
                    else
                    {
                        loadProgress = asyncOper.progress;
                    }

                    // 코루틴 내에서 반복문 사용 시
                    // 로직을 한번 실행 후에 메인 로직을 사용할 수 있게
                    // yield return null을 해준다
                    yield return null;
                }

                // 다음 씬을 준비하는 작업을 마쳤으므로 로딩 씬을 비활성화 합니다
                yield return SceneManager.UnloadSceneAsync(SceneType.Loading.ToString());

                // 혹시 작업이 다 끝난 뒤에 할 작업이 있다면 실행합니다
                loadComplete?.Invoke();

                // 인 게임에서 사용할 UI를 활성화 합니다 (나중에 할거임)
            }
        }

        /// <summary>
        /// 로딩 씬을 이용하여 실제로 씬을 이동하는 것처럼 보이게 해주는 메서드
        /// 로딩 씬이 실행되는 동안 필요한 리소스들을 불러오는 작업을 함
        /// </summary>
        /// <param name="loadCoroutine"> 씬에 필요한 작업 </param>
        /// <param name="loadComplete">  작업이 마친 뒤 추가 작업 </param>
        public void OnAddtiveLoadingScene(IEnumerator loadCoroutine = null, Action loadComplete = null)
        {
            StartCoroutine(WaitForLoad());

            IEnumerator WaitForLoad()
            {
                loadProgress = 0;

                var asyncOper = SceneManager.LoadSceneAsync(SceneType.Loading.ToString(), 
                    LoadSceneMode.Additive);

                #region 로딩씬 진행처리

                while (!asyncOper.isDone)
                {
                    loadProgress = asyncOper.progress;
                    yield return null;
                }

                UILoading uiLoading = null;

                while (uiLoading == null)
                {
                    // uiLoading = UIWindowManager.Instance.GetWindow(UILoading);
                    yield return null;
                }

                // 인 게임 씬에서 활성화된 상태로 카메라가 존재하기 때문에
                // 로딩 씬 카메라를 비활성화
                uiLoading.cam.enabled = false;
                loadProgress = 1f;

                #endregion

                // 텀 주기
                yield return new WaitForSeconds(.5f);

                #region 스테이지 전환 씨 필요한 작업

                if (loadCoroutine != null)
                {
                    yield return StartCoroutine(loadCoroutine);
                }

                #endregion

                // 텀 주기
                yield return new WaitForSeconds(.5f);

                #region 스테이지 전환 완료 후 실행할 작업

                // 로딩 씬 비활성화
                yield return SceneManager.UnloadSceneAsync(SceneType.Loading.ToString());
                loadComplete?.Invoke();

                #endregion
            }
        }
    }
}