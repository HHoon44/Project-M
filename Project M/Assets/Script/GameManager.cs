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
    /// ���ӿ� ���Ǵ� �����͸� �����ϴ� Ŭ����
    /// + ������ �� ����� ���� ������ ū �帧�� ��Ʈ�� �ϱ⵵ ��
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        public float loadProgress;      // �������� �󸶳� �غ� �Ǿ��ִ����� ���� ��

        protected override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// �ε� ���� �̿��Ͽ� ������ ���� �̵��ϴ� ��ó�� ���̰� ���ִ� �޼���
        /// �ε� ���� ����Ǵ� ���� �ʿ��� ���ҽ����� �ҷ����� �۾��� ��
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

                #region �ε��� ����ó��

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

                // �� ���� ������ Ȱ��ȭ�� ���·� ī�޶� �����ϱ� ������
                // �ε� �� ī�޶� ��Ȱ��ȭ
                uiLoading.cam.enabled = false;
                loadProgress = 1f;

                #endregion

                yield return new WaitForSeconds(.5f);

                #region �������� ��ȯ �� �ʿ��� �۾�

                if (loadCoroutine != null)
                {
                    yield return StartCoroutine(loadCoroutine);
                }

                #endregion

                yield return new WaitForSeconds(.5f);

                #region �������� ��ȯ �Ϸ� �� ������ �۾�

                yield return SceneManager.UnloadSceneAsync(SceneType.Loading.ToString());
                loadComplete?.Invoke();

                #endregion
            }
        }
    }
}