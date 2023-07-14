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
        }

        /// <summary>
        /// 로딩 씬을 이용하여 실제로 씬을 이동하는 것처럼 보이게 해주는 메서드
        /// 로딩 씬이 실행되는 동안 필요한 리소스들을 불러오는 작업을 함
        /// </summary>
        /// <param name="loadCoroutine">  </param>
        /// <param name="loadComplete">  </param>
        public void OnAddtiveLoadingScene(IEnumerator loadCoroutine = null, Action loadComplete = null)
        {
            StartCoroutine(WaitForLoad());

            IEnumerator WaitForLoad()
            {
                loadProgress = 0;

                var asyncOper = SceneManager.LoadSceneAsync(SceneType.Loading.ToString(), LoadSceneMode.Additive);

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

                yield return new WaitForSeconds(.5f);

                #region 스테이지 전환 씨 필요한 작업

                if (loadCoroutine != null)
                {
                    yield return StartCoroutine(loadCoroutine);
                }

                #endregion

                yield return new WaitForSeconds(.5f);

                #region 스테이지 전환 완료 후 실행할 작업

                yield return SceneManager.UnloadSceneAsync(SceneType.Loading.ToString());
                loadComplete?.Invoke();

                #endregion
            }
        }
    }
}