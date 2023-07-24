using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.UI
{
    /// <summary>
    /// ��� UI�� ���̽� Ŭ����
    /// ���׸��� �˾�, UI���� UIElement�� ����
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class UIWindow : MonoBehaviour
    {
        public bool isOpen;     // �ش� UI�� Ȱ��ȭ ����

        private CanvasGroup cachedCanvasGroup;

        /// <summary>
        /// ĵ���� �׷��� �̿��Ͽ� UI�� ��/Ȱ��ȭ �ϴ� ȿ���� �ش� (���İ��� ����)
        /// ��Ȱ��ȭ �� ������ UI�� ��Ȱ��ȭ �ϴ� ���̶� �ƴ϶�
        /// UI �Է��� �����Ѵ� (��� ����ĳ��Ʈ)
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
        /// UI�� �Ŵ����� ����ϰ� �ʱ�ȭ �ϴ� �޼���
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
        /// UI�� Ȱ��ȭ �ϰ� 
        /// Ȱ��ȭ UI�� �Ŵ����� ĳ�� ��û�ϴ� �޼���
        /// </summary>
        /// <param name="force"> Ȱ��ȭ ���� </param>
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
        /// UI�� ��Ȱ��ȭ �ϰ�
        /// ��Ȱ��ȭ �ϰ��� �ϴ� UI�� ĳ�� ��Ͽ��� ���� ��û�ϴ� �޼���
        /// </summary>
        /// <param name="force"> Ȱ��ȭ ���� </param>
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
        /// Ȱ��ȭ ���ο� ���� ĵ���� �׷��� �ʵ带 �����ϴ� �޼���
        /// </summary>
        /// <param name="isActive"> Ȱ��ȭ ���� </param>
        private void SetCanvasGroup(bool isActive)
        {
            // �������� ����
            CachedCanvasGroup.alpha = Convert.ToInt32(isActive);

            // �Է��� ���� ���� ���� ����
            cachedCanvasGroup.interactable = isActive;

            // ����ĳ��Ʈ�� ���� �ݶ��̴��� ���������� ���� ����
            cachedCanvasGroup.blocksRaycasts = isActive;
        }
    }
}