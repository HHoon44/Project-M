using ProjectM.Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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