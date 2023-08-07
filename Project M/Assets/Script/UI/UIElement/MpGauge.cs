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

            // ��������Ʈ �δ��� ��Ʋ�� �����ͼ� �̹��� ����

            image.type = Image.Type.Filled;
            image.fillMethod = Image.FillMethod.Vertical;
        }

        void Update()
        {

        }

        /// <summary>
        /// ������ Ÿ���� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <param name="type"> ��ȯ�� Ÿ�� </param>
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