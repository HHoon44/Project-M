using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Define;

namespace ProjectM.InGame
{
    public class GameMobBase : MonoBehaviour
    {
        //������Ʈ
        private GameMobAutoMovement movement;
        public Animator renderAnim { get; private set; }

        //mobInfo
        public MobInfo mobInfo { get; private set; }
        public float nowHP { get; private set; }
        public bool detectPlayer { get; private set; }  //�÷��̾ ��ó�� �ִٸ�
        public bool discoverPlayer { get; private set; }  //�÷��̾ ���δٸ�

        public bool isLive { get; private set; }   //�ڽ��� �׾��ٸ�

        [Header("MobSetting")]
        public KindOfMob thisMobType;
        public GameObject renderObj;
        public Transform moveTip;
        public Transform atkTip;
        public GameObject detectMark;

        private void Start()
        {
            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("���� �� ���� ������Ʈ�� �ٸ� ��ü�� ������ �ֽ��ϴ�.");
                this.enabled = false;
            }

            renderAnim = renderObj.GetComponent<Animator>();
            if (renderAnim == null)
                Debug.LogWarning("�ɸ����� �ִϸ����Ͱ� �����ϴ�.");

            mobInfo = GameMobStaticData.Instance.GetMobReferenceInfo(thisMobType);

            Regen();
        }

        //act: ���Ͱ� �ٽ� �¾ �� Ȱ��ȭ ����
        private void Regen()
        {
            isLive = true;
            nowHP = mobInfo.maxHP;
            renderObj.SetActive(true);
        }

        private void FixedUpdate()
        {
            detectPlayer = DetectPlayer();
            discoverPlayer = DiscoverPlayer();

            if (detectPlayer)
            {
                Debug.DrawLine(atkTip.position, GamePlayer.GetPlayerTip(), Color.red);
                detectMark.SetActive(true);
            }
            else
                detectMark.SetActive(false);
        }

        //�÷��̾ ���� ������ ���� �� true
        private bool DetectPlayer() => Vector2.Distance(transform.position, GamePlayer.GetPlayerTip()) <= mobInfo.detectArea;
        //�÷��̾�� ���� ���̿� ��ֹ��� ���� �� true
        private bool DiscoverPlayer() => (detectPlayer && !Physics2D.Linecast(atkTip.position, GamePlayer.GetPlayerTip(), LayerMask.GetMask("Ground")));

        //@ ���� ������ ============================================================================================
        // act: ���Ͱ� ���ڰ� ��ŭ �������� ������, �� ��� �ش� ������� �޾ƿɴϴ�.
        public void SufferDemage(float _Demaged, DebuffType[] _types)
        {
            //�������� ���̶�� ������ ���� ����
            if (mobInfo.staticMob)
                return;

            if (0 >= nowHP - _Demaged)
            {
                nowHP = 0;
                MobDie();
                return;
            }
            else
            {
                //todo: ���� �޴� �ִϸ��̼�
                nowHP -= _Demaged;
            }
        }

        //act: ������ ����
        //todo: ���� ���� ��� ���
        private void MobDie()
        {
            Debug.Log("todo: ���� ����");

            isLive = false;

            //todo: ���� �״� �ִϸ��̼�
            Invoke(nameof(MobDead), 1f);
        }
        private void MobDead()
        {
            renderObj.SetActive(false);
        }
    }
}