using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Mob
{
    public class GameMob : MonoBehaviour
    {
        private Animator anim;

        public bool thisSpecialMob = false;   //�� ���Ͱ� ���� �� Ư���� ���Ͷ�� true


        private float maxHP;
        private float nowHP
        {
            get { return nowHP; }
            set
            {
                //���� 
                if (nowHP <= -value)
                {
                    nowHP = 0;
                    MobDie();
                }
                else
                {
                    nowHP += value;
                }
            }
        }

        private void Start()
        {
            anim = GetComponent<Animator>();

            if (anim == null)
                Debug.LogWarning("�ɸ����� �ִϸ����Ͱ� �����ϴ�.");
        }

        private void OnEnable()
        {
            

        }

        // act: ���Ͱ� ���ڰ� ��ŭ �������� ������, �� ��� �ش� ������� �޾ƿɴϴ�.
        public void SufferDemage(float _Demaged, DebuffType[] _types)
        {
            nowHP -= _Demaged;
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