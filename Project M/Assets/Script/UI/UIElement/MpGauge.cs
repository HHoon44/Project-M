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
        [SerializeField]
        public GaugeType type;

        private Image image;

        void Start()
        {
            image = GetComponent<Image>();

            // 스프라이트 로더로 아틀라스 가져와서 이미지 설정

            image.type = Image.Type.Filled;
            image.fillMethod = Image.FillMethod.Vertical;
        }

        void Update()
        {

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
                    image.fillMethod = Image.FillMethod.Vertical;
                    break;

                case GaugeType.Curse:
                    this.type = GaugeType.Curse;
                    image.fillMethod = Image.FillMethod.Horizontal;
                    break;
            }
        }
    }
}