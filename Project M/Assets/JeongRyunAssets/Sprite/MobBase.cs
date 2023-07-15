using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Define;

namespace ProjectM.InGame
{
    public class MobBase : MonoBehaviour
    {
        [Header("MobSetting")]
        public KindOfMob thisMobType = KindOfMob.NoneMovementMob;

        [Space(10f)]
        public GameObject myForm;   //게임으로 보이는 몬스터의 실체
        public Transform atkTip;  //공격을 시작하는 위치

        [Space(10f)]
        [Header("MobEmotion")]
        public GameObject detectionMark;
        public GameObject discoveryMark;

        public Animator formAnim { get; private set; }
        private GameObject[] myModule = new GameObject[3];

        //몬스터 변수
        public float nowHP { get; private set; }
        public bool detectionPlayer { get; private set; }  //플레이어가 근처에 있다면
        public bool discoveryPlayer { get; private set; }  //플레이어가 보인다면
        public bool isLive { get; private set; }   //자신이 죽었다면

        //몬스터 상수
        public MobInfo myInfo { get; private set; }
        public Vector2 colSize { get; protected set; }


        private void Start()
        {
            myInfo = MobsStaticData.Instance.GetMobReferenceInfo(thisMobType);

            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("현재 몹 전용 컴포넌트를 다른 객체가 가지고 있습니다.");
                this.enabled = false;
            }

            formAnim = myForm.GetComponent<Animator>();
            colSize = myForm.GetComponent<CapsuleCollider2D>().size + myForm.GetComponent<CapsuleCollider2D>().offset;
            if (formAnim == null)
                Debug.LogWarning("케릭터의 애니메이터 혹은 콜라이더가 없습니다.");

            detectionMark.SetActive(false);
            discoveryMark.SetActive(false);

            SetModule();

            Regen();
            StartCoroutine(UpdateEmotion_co());
        }



        public void SetModule()
        {
            if (myInfo.jumpForce <= 0)
                myModule[0] = Instantiate(MobsStaticData.mobMovementModule, transform);
            else
                myModule[0] = Instantiate(MobsStaticData.mobJumpModule, transform);

            if (myInfo.dashForce >= 0)
                myModule[1] = Instantiate(MobsStaticData.mobDashModule, transform);

            if (myInfo.atkCool >= 0)
                myModule[2] = Instantiate(MobsStaticData.mobAttackModule, transform);

            foreach (GameObject item in myModule)
            {
                
                item.transform.position = Vector3.zero;
            }
        }

        private void FixedUpdate()
        {
            DetectUpdate();
        }




        //@ 몬스터 라이브 ===================================================================================================================

        //act: 몬스터가 다시 태어날 때 활성화 로직
        private void Regen()
        {
            isLive = true;
            nowHP = myInfo.maxHP;
            myForm.SetActive(true);
        }

        //act: 데미지를 입힐 때 호출
        public void SufferDemage(float _Demaged, DebuffType[] _types)
        {
            //절대적인 몹이라면 공격을 받지 않음
            if (myInfo.staticMob)
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

            isLive = false;

            //todo: 몬스터 죽는 애니메이션
            Invoke(nameof(MobDead), 1f);
        }
        private void MobDead()
        {
            myForm.SetActive(false);
        }

        //@ 플레이어 감지 ===================================================================================================================
        //act: 플래이어를 기준으로 감지한다.
        private void DetectUpdate()
        {
            if (myInfo.detectArea == 0)
                return;

            detectionPlayer = DetectPlayer();
            discoveryPlayer = DiscoverPlayer();

            if (detectionPlayer)
                Debug.DrawLine(atkTip.position, PlayerController.GetPlayerTip(), Color.red);
        }

        //act: 감지된 정보를 토대로 몹 위에 플레이어가 감지 여부를 알 수 있도록 띄어줍니다.
        private IEnumerator UpdateEmotion_co()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                if (discoveryPlayer)
                {
                    detectionMark.SetActive(false);
                    discoveryMark.SetActive(true);
                }
                else if (detectionPlayer)
                {
                    detectionMark.SetActive(true);
                    discoveryMark.SetActive(false);
                }
                else
                {
                    detectionMark.SetActive(false);
                    discoveryMark.SetActive(false);
                }
            }
        }

        //플레이어가 몬스터 근차에 갔을 때 true
        private bool DetectPlayer() => Vector2.Distance(transform.position, PlayerController.GetPlayerTip()) <= myInfo.detectArea;
        //플레이어와 몬스터 사이에 장애물이 없을 때 true
        private bool DiscoverPlayer() => (detectionPlayer && !Physics2D.Linecast(atkTip.position, PlayerController.GetPlayerTip(), LayerMask.GetMask("Ground")));

    }
}