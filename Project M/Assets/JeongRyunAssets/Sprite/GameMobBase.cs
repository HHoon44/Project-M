using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Define;

namespace ProjectM.InGame
{
    public class GameMobBase : MonoBehaviour
    {
        private Animator anim;
        private GameMobAutoMovement movement;
        private GameMobBaseATK attack;

        [Header("MobSetting")]
        public KindOfMob thisMobType;

        //mobInfo
        public MobInfo mobInfo { get; private set; }
        public float nowHP { get; private set; }

        public bool detectPlayer;
        public bool discoverPlayer;

        private void Start()
        {
            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("���� �� ���� ������Ʈ�� �ٸ� ��ü�� ������ �ֽ��ϴ�.");
                this.enabled = false;
            }

            anim = GetComponent<Animator>();
            attack = GetComponent<GameMobBaseATK>();

            if (anim == null)
                Debug.LogWarning("�ɸ����� �ִϸ����Ͱ� �����ϴ�.");

            mobInfo = GameMobStaticData.Instance.GetMobReferenceInfo(thisMobType);

            Regen();
        }

        private void OnEnable()
        {
            //todo: �� ���� �ִϸ��̼� Ȱ��ȭ
            //Invoke(nameof(Regen), 1f);
            Regen();
        }

        private void FixedUpdate()
        {
            detectPlayer = DetectPlayer();
            discoverPlayer = DiscoverPlayer();

            Debug.DrawLine(attack.atkTip.position, GamePlayer.GetPlayerTip(), Color.red);
        }

        //�÷��̾ ���� ������ ���� �� true
        private bool DetectPlayer() => Vector2.Distance(transform.position, GamePlayer.GetPlayerTip()) <= mobInfo.detectArea;
        //�÷��̾�� ���� ���̿� ��ֹ��� ���� �� true
        private bool DiscoverPlayer() => (detectPlayer && !Physics2D.Linecast(attack.atkTip.position, GamePlayer.GetPlayerTip(), LayerMask.GetMask("Ground")));

        //@ ���� ������ ============================================================================================

        //act: ���Ͱ� �ٽ� �¾ �� Ȱ��ȭ ����
        private void Regen()
        {
            nowHP = mobInfo.maxHP;
        }

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

            //todo: ���� �״� �ִϸ��̼�
            Invoke(nameof(MobDead), 2f);
        }
        private void MobDead()
        {
            gameObject.SetActive(false);
        }
    }
}