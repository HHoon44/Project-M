using ProjectM.Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public void Initialize()
        {
            OnPhase(introPhase);
        }

        private void OnPhase(IntroPhase introPhase)
        {

        }

        private void NextPhase()
        { 
        
        }
    }
}