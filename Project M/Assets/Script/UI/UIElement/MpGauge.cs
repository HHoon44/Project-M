using ProjectM.Define;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectM.UI
{
    public class MpGauge : MonoBehaviour
    {
        private GaugeType type;      // 현재 게이지 타입
        private Image bar;           

        void Start()
        {
            bar = GetComponent<Image>();

            // 스프라이트 로더로 아틀라스 가져와서 이미지 설정

            bar.type = Image.Type.Filled;
            bar.fillMethod = Image.FillMethod.Vertical;

            ChangeType(GaugeType.Mp);
        }

        /// <summary>
        /// 게이지 타입을 변환하는 메서드
        /// </summary>
        /// <param name="type"> 변환할 타입 </param>
        public void ChangeType(GaugeType type)
        {
            switch (type)
            {
                case GaugeType.Mp:
                    this.type = GaugeType.Mp;
                    bar.fillMethod = Image.FillMethod.Vertical;
                    break;

                case GaugeType.Curse:
                    this.type = GaugeType.Curse;
                    bar.fillMethod = Image.FillMethod.Horizontal;
                    break;
            }
        }

        /// <summary>
        /// 게이지 값 설정 메서드
        /// </summary>
        /// <param name="value"> 설정할 값 </param>
        public void SetGauge(float value)
        {
            bar.fillAmount = value;
        }
    }
}