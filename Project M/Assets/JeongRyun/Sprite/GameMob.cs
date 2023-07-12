using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Define;

namespace ProjectM.InGame
{
    public class GameMob : MonoBehaviour
    {
        private Animator anim;
        private GameMobMovement movement;

        [Header("MobSetting")]
        public KindOfMob thisMobType;

        public MobInfo mobInfo { get; private set; }
        public float nowHP { get; private set; }

        private void Start()
        {
            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("현재 몹 전용 컴포넌트를 다른 객체가 가지고 있습니다.");
                this.enabled = false;
            }

            anim = GetComponent<Animator>();

            if (anim == null)
                Debug.LogWarning("케릭터의 애니메이터가 없습니다.");

            mobInfo = GameMobStaticData.Instance.GetMobReferenceInfo(thisMobType);
        }

        private void OnEnable()
        {
            //todo: 몹 생성 애니메이션 활성화
            Invoke(nameof(Regen), 1f);
        }

        private void Regen()
        {
            nowHP = mobInfo.maxHP;
        }



        private void Movement()
        {

        }

        // act: 몬스터가 인자값 만큼 데미지를 입으며, 그 즉시 해당 디버프를 받아옵니다.
        public void SufferDemage(float _Demaged, DebuffType[] _types)
        {
            //절대적인 몹이라면 공격을 받지 않음
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
                //todo: 공격 받는 애니메이션
                nowHP -= _Demaged;
            }
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