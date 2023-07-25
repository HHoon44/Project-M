using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Util;



namespace ProjectM.InGame
{
    //tip: 대쉬 모둘의 매인 컴포너트 입니다
    public class MobDash : MonoBehaviour, IMobConsistModule
    {
        private MobBase mob;
        private SpriteRenderer mobRender;
        private AfterimageController afterimage;

        [SerializeField] private float dashForce;   //대쉬 거리
        [SerializeField] private float dashCooltime;   //0초이면, 오토대쉬를 하지 않습니다.
        [SerializeField] private bool dashAttack = false;

        static private float decreaseMount = 80;
        static private float chargingTime = 1f;

        public bool isDash { get; private set; } = false;

        private static Transform afterimageGroup = null;  //몬스터 잔상을 관리하는 tramsform

        public void Initialize(MobBase _mob)
        {
            mob = _mob;
            gameObject.tag = "Mob";
            gameObject.name = "DashModule";

            transform.localPosition = Vector3.zero;

            dashForce = _mob.myMovement.dashForce;
            dashCooltime = _mob.myMovement.dashCooltime;
        }

        public void SetActiveModule(bool _act)
        {
        }

        public GameObject thisObj()
        {
            return gameObject;
        }

        public object thisScript()
        {
            return this;
        }


        void Start()
        {
            if (afterimageGroup == null)
                afterimageGroup = GameObject.Find("EffectGroup").transform;

            mobRender = mob.myFormObj.GetComponent<SpriteRenderer>();

            //잔상세팅
            afterimage = EffectGroupManager.Instance.MakeNornal(mob.gameObject, mobRender);
            StartCoroutine(DashAuto_co());
            StartCoroutine(DashPlayer_co());
        }

        void Update()
        {

        }

        private void FixedUpdate()
        {

        }

        private void DashStart()
        {
            if (isDash == true)
                return;

            StartCoroutine(Dash_co());
        }

        private IEnumerator Dash_co()
        {
            isDash = true;
            float remainingForce = dashForce;

            //차징 (대기시간)
            if (mob.discoveryPlayer)
            {
                mob.movementModule.Idle(chargingTime + (remainingForce / decreaseMount));
                yield return new WaitForSeconds(chargingTime);
            }

            //데쉬
            afterimage.StartAfterimage(dashForce / decreaseMount);
            while (remainingForce >= dashForce / 20f)
            {
                mob.nowVelocityX += remainingForce * (mob.movementModule.flipX ? -1 : 1);
                remainingForce -= Time.fixedDeltaTime * decreaseMount;

                if (Mathf.Abs(mob.transform.position.x - PlayerController.GetPlayerTip().x) <= .2f || mob.movementModule.groundSense) //플레이어와 동일선상의 X축위에 있으면
                {
                    mob.movementModule.Idle();
                    afterimage.StopAfterimage();
                    break;
                }

                yield return new WaitForFixedUpdate();
            }
            isDash = false;
        }

        private IEnumerator DashAuto_co()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1f, 20f));
                if (mob.discoveryPlayer)
                    continue;
                DashStart();
            }
        }
        private IEnumerator DashPlayer_co()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();

                if (!mob.discoveryPlayer)
                    continue;

                DashStart();
                yield return new WaitForSeconds(dashCooltime);
            }
        }

    }
}
