using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    public class GameMob : MonoBehaviour
    {
        private Animator anim;

        [Header("MobSetting")]
        public bool thisSpecialMob = false;   //이 몬스터가 보스 등 특별한 몬스터라면 true
        public bool thisStaticMob = false;  //공격을 받지 않는 장애물 같은 몬스터라면 true
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
                Debug.LogWarning("케릭터의 애니메이터가 없습니다.");

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
            if (Physics2D.Linecast(BottomRayTip.position, BottomRayTip.position, 1 << LayerMask.NameToLayer("Ground"))) //땅만 인식한다.
            {
                Debug.Log("a");
            }
        }

        private void Movement()
        {

        }

        // act: 몬스터가 인자값 만큼 데미지를 입으며, 그 즉시 해당 디버프를 받아옵니다.
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