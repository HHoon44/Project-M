using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Mob
{
    public class GameMob : MonoBehaviour
    {
        private Animator anim;

        public bool thisSpecialMob = false;   //이 몬스터가 보스 등 특별한 몬스터라면 true


        private float maxHP;
        private float nowHP
        {
            get { return nowHP; }
            set
            {
                //만약 
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
                Debug.LogWarning("케릭터의 애니메이터가 없습니다.");
        }

        private void OnEnable()
        {
            

        }

        // act: 몬스터가 인자값 만큼 데미지를 입으며, 그 즉시 해당 디버프를 받아옵니다.
        public void SufferDemage(float _Demaged, DebuffType[] _types)
        {
            nowHP -= _Demaged;
            //todo: 공격 받는 애니메이션
        }


        //act: 몬스터의 죽음
        //todo: 몬스터 폴링 기술 사용
        private void MobDie()
        {
            Debug.Log("todo: 몬스터 죽음");

            //todo: 몬스터 죽는 애니메이션
            Invoke(nameof(MobDead), 2f);
        }

        private void MobDead()
        {
            gameObject.SetActive(false);
        }
    }
}