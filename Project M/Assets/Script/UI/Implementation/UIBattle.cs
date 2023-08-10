using ProjectM.InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace ProjectM.UI
{
    public class UIBattle : MonoBehaviour
    {
        [SerializeField]
        private MpGauge mpGauge;     // 플레이어의 MP게이지

        [SerializeField]
        private PlayerBase playerBase;      // 플레이어 정보


        private void Start()
        {

        }

        private void Update()
        {
            GaugeUpdate();
        }

        /// <summary>
        /// 플레이어의 MP 게이지를 업데이트 하는 메서드
        /// </summary>
        private void GaugeUpdate()
        {
            // 저주 값 존재하면 계산해서 넣어줘야함
            // 일단 저주는 나중에 생각하기

            var currentMp = playerBase.currentMp / playerBase.maxMp;

            mpGauge.SetGauge(currentMp);
        }
    }
}
