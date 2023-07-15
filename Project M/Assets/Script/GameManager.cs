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

            if (gameObject == null)
            {
                return;
            }

            // ���� ���� �Ǵ��� �ı����� �ʵ���
            DontDestroyOnLoad(this);

            // ������ �ʱ� ������ �����մϴ�
            var startController = FindObjectOfType<StartController>();
            startController?.Initialize();
        }

        public void OnApplicationSetting()
        {
            // ���� ����ȭ ����
            QualitySettings.vSyncCount = 0;

            // ���� �������� 60���� ����
            Application.targetFrameRate = 60;

            // ��ð� ��� �ÿ��� ȭ���� ������ �ʵ��� ����
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        /// <summary>
        /// �� ��ȯ�� �����ϴ� �޼���
        /// </summary>
        /// <param name="sceneName"> �ҷ��� ���� �̸� </param>
        /// <param name="loadCroutine"> �� �غ� �۾� </param>
        /// <param name="loadComplete"> �غ� �۾��� ��ģ �� �߰� �۾� </param>
        public void LoadScene(SceneType sceneName, IEnumerator loadCroutine = null,
            Action loadComplete = null)
        {
            StartCoroutine(WaitForLoad());

            IEnumerator WaitForLoad()
            {
                // ���� ���� ����
                loadProgress = 0;

                // ���� ���� �غ��ϱ� ���� �۾��� �ϱ����� �ε����� ������´�
                yield return SceneManager.LoadSceneAsync(SceneType.Loading.ToString());

                // �ҷ������� �ϴ� ���� ������ �ڿ� �� Ȱ��ȭ �س��´�
                var asyncOper = SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Additive);
                asyncOper.allowSceneActivation = false;

                // ���� ���� �غ��ϴµ� �ʿ��� �۾��� �ִٸ�
                if (loadCroutine != null)
                {
                    // �ش� �۾��� �Ϸ� �� ������ ���
                    yield return StartCoroutine(loadCroutine);
                }

                while (!asyncOper.isDone)
                {
                    // �۾��� �� �����ٸ�
                    if (loadProgress >= .9f)
                    {
                        loadProgress = 1;

                        // 1�� ���� ���� �ش�
                        yield return new WaitForSeconds(1f);

                        // �ҷ��� ���� Ȱ��ȭ
                        asyncOper.allowSceneActivation = true;
                    }
                    else
                    {
                        loadProgress = asyncOper.progress;
                    }

                    // �ڷ�ƾ ������ �ݺ��� ��� ��
                    // ������ �ѹ� ���� �Ŀ� ���� ������ ����� �� �ְ�
                    // yield return null�� ���ش�
                    yield return null;
                }

                // ���� ���� �غ��ϴ� �۾��� �������Ƿ� �ε� ���� ��Ȱ��ȭ �մϴ�
                yield return SceneManager.UnloadSceneAsync(SceneType.Loading.ToString());

                // Ȥ�� �۾��� �� ���� �ڿ� �� �۾��� �ִٸ� �����մϴ�
                loadComplete?.Invoke();

                // �� ���ӿ��� ����� UI�� Ȱ��ȭ �մϴ� (���߿� �Ұ���)
            }
        }

        /// <summary>
        /// �ε� ���� �̿��Ͽ� ������ ���� �̵��ϴ� ��ó�� ���̰� ���ִ� �޼���
        /// �ε� ���� ����Ǵ� ���� �ʿ��� ���ҽ����� �ҷ����� �۾��� ��
        /// </summary>
        /// <param name="loadCoroutine"> ���� �ʿ��� �۾� </param>
        /// <param name="loadComplete">  �۾��� ��ģ �� �߰� �۾� </param>
        public void OnAddtiveLoadingScene(IEnumerator loadCoroutine = null, Action loadComplete = null)
        {
            StartCoroutine(WaitForLoad());

            IEnumerator WaitForLoad()
            {
                loadProgress = 0;

                var asyncOper = SceneManager.LoadSceneAsync(SceneType.Loading.ToString(), 
                    LoadSceneMode.Additive);

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

                // �� �ֱ�
                yield return new WaitForSeconds(.5f);

                #region �������� ��ȯ �� �ʿ��� �۾�

                if (loadCoroutine != null)
                {
                    yield return StartCoroutine(loadCoroutine);
                }

                #endregion

                // �� �ֱ�
                yield return new WaitForSeconds(.5f);

                #region �������� ��ȯ �Ϸ� �� ������ �۾�

                // �ε� �� ��Ȱ��ȭ
                yield return SceneManager.UnloadSceneAsync(SceneType.Loading.ToString());
                loadComplete?.Invoke();

                #endregion
            }
        }
    }
}