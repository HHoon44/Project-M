using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Util;



namespace ProjectM.InGame
{
    //tip: 대쉬 모둘의 매인 컴포너트 입니다
    public class MobDash : MonoBehaviour, IMobConsistModule
    {
        [SerializeField] private GameObject afterimagePrefab;
        private SpriteRenderer[] afterimageArr;  //폴딩 기술 사용

        private MobBase mob;
        private SpriteRenderer mobRender;

        [SerializeField] protected float dashForce;   //대쉬 거리
        [SerializeField] protected float dashCooltime;   //0초이면, 오토대쉬를 하지 않습니다.

        static private float decreaseMount = 80;
        static private float chargingTime = 1f;


        public bool isDash { get; private set; }

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
            StartCoroutine(DashStart_co());

            //이미지 세팅
            GameObject afterimage = Instantiate(afterimagePrefab, afterimageGroup);
            afterimageArr = new SpriteRenderer[afterimage.transform.childCount];
            for (int i = 0; i < afterimage.transform.childCount; i++)
                afterimageArr[i] = afterimage.transform.GetChild(i).GetComponent<SpriteRenderer>();
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

            StartCoroutine(Dash_co());
        }
        private IEnumerator Dash_co()
        {
            float remainingForce = dashForce;

            //차징
            mob.movementModule.Idle(chargingTime + (remainingForce / decreaseMount));
            yield return new WaitForSeconds(chargingTime);

            //데쉬
            StartCoroutine(DashAfterimage_co());
            while (remainingForce >= dashForce / 20f)
            {
                mob.nowVelocityX += remainingForce * (mob.movementModule.flipX ? -1 : 1);
                remainingForce -= Time.fixedDeltaTime * decreaseMount;

                if (mob.movementModule.groundSense)
                    break;
                if (Mathf.Abs(mob.transform.position.x - PlayerController.GetPlayerTip().x) <= .5f) //플레이어와 동일선상의 X축위에 있으면
                    break;

                yield return new WaitForFixedUpdate();
            }

            DashEnd();
        }

        private IEnumerator DashAfterimage_co()
        {
            int index = 0;
            while (isDash)
            {
                int i = index % afterimageArr.Length;

                afterimageArr[i].transform.position = mob.transform.position;
                afterimageArr[i].sprite = mobRender.sprite;

                afterimageArr[i].gameObject.SetActive(false);
                afterimageArr[i].gameObject.SetActive(true);

                Color32 color = afterimageArr[i].color;
                color.a = 100;
                afterimageArr[i].color = color;

                index++;
                StartCoroutine(OffAfterimage_co(afterimageArr[i].gameObject));
                yield return new WaitForSeconds(dashForce / (decreaseMount * 6));
            }
        }

        private IEnumerator OffAfterimage_co(GameObject _that)
        {
            yield return new WaitForSeconds(0.4f);
            _that.SetActive(false);
        }

        private void DashEnd()
        {
            //mob.movementModule.Move();
            isDash = false;
        }

    }
}
