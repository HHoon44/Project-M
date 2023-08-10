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
        private MpGauge mpGauge;     // �÷��̾��� MP������

        [SerializeField]
        private PlayerBase playerBase;      // �÷��̾� ����


        private void Start()
        {

        }

        private void Update()
        {
            GaugeUpdate();
        }

        /// <summary>
        /// �÷��̾��� MP �������� ������Ʈ �ϴ� �޼���
        /// </summary>
        private void GaugeUpdate()
        {
            // ���� �� �����ϸ� ����ؼ� �־������
            // �ϴ� ���ִ� ���߿� �����ϱ�

            var currentMp = playerBase.currentMp / playerBase.maxMp;

            mpGauge.SetGauge(currentMp);
        }
    }
}
