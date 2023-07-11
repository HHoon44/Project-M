using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    public class GameMob : MonoBehaviour
    {
        private Animator anim;

        [Header("MobSetting")]
        public KindOfMob thisMobType;

        // [Space(10f)]
        // [Header("Linking")]

        public MobInfo mobInfo { get; private set; }
        public float nowHP { get; private set; }

        private void Start()
        {
            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("���� �� ���� ������Ʈ�� �ٸ� ��ü�� ������ �ֽ��ϴ�.");
                this.enabled = false;
            }

            anim = GetComponent<Animator>();

            if (anim == null)
                Debug.LogWarning("�ɸ����� �ִϸ����Ͱ� �����ϴ�.");

            mobInfo = GameMobStaticData.Instance.GetMobReferenceInfo(thisMobType);
        }

        private void OnEnable()
        {
            //todo: �� ���� �ִϸ��̼� Ȱ��ȭ
            Invoke(nameof(Regen), 1f);
        }

        private void Regen()
        {
            nowHP = mobInfo.maxHP;
        }



        private void Movement()
        {

        }

        // act: ���Ͱ� ���ڰ� ��ŭ �������� ������, �� ��� �ش� ������� �޾ƿɴϴ�.
        public void SufferDemage(float _Demaged, DebuffType[] _types)
        {
            if (0 >= nowHP - _Demaged)
            {
                nowHP = 0;
                MobDie();
            }
            else
            {
                nowHP -= _Demaged;
            }

            //todo: ���� �޴� �ִϸ��̼�
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