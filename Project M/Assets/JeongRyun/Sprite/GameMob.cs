using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    public class GameMob : MonoBehaviour
    {
        private Animator anim;

        [Header("MobSetting")]
        public bool thisSpecialMob = false;   //�� ���Ͱ� ���� �� Ư���� ���Ͷ�� true
        public bool thisStaticMob = false;  //������ ���� �ʴ� ��ֹ� ���� ���Ͷ�� true
        public KindOfMob mobType;

        [Space(10f)]
        [Header("Linking")]
        public Transform BottomRayTip;

        private MobInfo mobStartInfo;
        public float nowHP { get; private set; }
        private float speed;

        private void Start()
        {
            anim = GetComponent<Animator>();

            if (anim == null)
                Debug.LogWarning("�ɸ����� �ִϸ����Ͱ� �����ϴ�.");

            GameMobStaticData.Instance.GetMobReferenceInfo(mobType);
            nowHP = mobStartInfo.maxHP;
            speed = mobStartInfo.speed;
        }

        private void OnEnable()
        {

        }

        private void FixedUpdate()
        {
            //RaycastHit2D hit;
            if (Physics2D.Linecast(BottomRayTip.position, BottomRayTip.position, 1 << LayerMask.NameToLayer("Ground"))) //���� �ν��Ѵ�.
            {
                Debug.Log("a");
            }
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