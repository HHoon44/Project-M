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
        private GaugeType type;      // ���� ������ Ÿ��
        private Image bar;           

        void Start()
        {
            bar = GetComponent<Image>();

            // ��������Ʈ �δ��� ��Ʋ�� �����ͼ� �̹��� ����

            bar.type = Image.Type.Filled;
            bar.fillMethod = Image.FillMethod.Vertical;

            ChangeType(GaugeType.Mp);
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
                    bar.fillMethod = Image.FillMethod.Vertical;
                    break;

                case GaugeType.Curse:
                    this.type = GaugeType.Curse;
                    bar.fillMethod = Image.FillMethod.Horizontal;
                    break;
            }
        }

        /// <summary>
        /// ������ �� ���� �޼���
        /// </summary>
        /// <param name="value"> ������ �� </param>
        public void SetGauge(float value)
        {
            bar.fillAmount = value;
        }
    }
}