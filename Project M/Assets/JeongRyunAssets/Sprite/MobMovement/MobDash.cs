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

        [SerializeField] protected float dashForce;   //대쉬 거리
        [SerializeField] protected float dashCooltime;   //0초이면, 오토대쉬를 하지 않습니다.

        static private float decreaseMount = 100;
        static private float chargingTime = 0.7f;

        private GameObject[] afterimageObj;  //폴딩 기술 사용
        private Vector2[] afterimageStartPos;

        public bool isDash { get; private set; }

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
            mobRender = mob.myFormObj.GetComponent<SpriteRenderer>();
            StartCoroutine(DashStart_co());
        }

        void Update()
        {
            // if (isDash)
            // {
            //     for (int i = 0; i < afterimageObj.Length; i++)
            //     {
            //         afterimageObj[i].transform.position = afterimageStartPos[i];
            //     }
            // }
        }
        private void FixedUpdate()
        {
        }

        private IEnumerator DashStart_co()
        {
            while (true)
            {
                yield return new WaitForSeconds(dashCooltime);
                DashStart();
            }
        }

        private void DashStart()
        {
            if (isDash == true)
                return;

            isDash = true;

            StartCoroutine(ActDash_co());
        }
        private IEnumerator ActDash_co()
        {
            float remainingForce = dashForce;
            yield return new WaitForSeconds(chargingTime);
            while (remainingForce >= dashForce / 20f)
            {
                mob.nowVelocityX += remainingForce * mob.transform.localScale.x;
                remainingForce -= Time.fixedDeltaTime * decreaseMount;
                yield return new WaitForFixedUpdate();
            }
            DashEnd();
        }
        private void DashEnd()
        {
            isDash = false;

        }

    }
}
