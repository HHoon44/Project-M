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

        private GameObject mobForm;
        private Sprite nowSprite;

        [SerializeField] protected float dashForce;   //대쉬 거리
        [SerializeField] protected float dashCooltime;   //0초이면, 오토대쉬를 하지 않습니다.
        [SerializeField] protected float dashReadyTime;  //대쉬를 하기전 잠깐 멈춰있는 시간

        private GameObject[] afterimageObj;
        private Vector2[] afterimageStartPos;

        public bool isDash { get; private set; }

        public void Initialize(MobBase _mob)
        {
            mob = _mob;
            gameObject.tag = "Mob";
            gameObject.name = "DashModule";

            transform.localPosition = Vector3.zero;
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
            mobForm = mob.myFormObj;
        }

        void Update()
        {
            if (isDash)
            {
                for (int i = 0; i < afterimageObj.Length; i++)
                {
                    afterimageObj[i].transform.position = afterimageStartPos[i];
                }
            }
        }
        private void FixedUpdate()
        {
// if(isDash)
//mob.
        }

        private void DashStart()
        {
            if (isDash == true)
                return;

            isDash = true;
            nowSprite = mobForm.GetComponent<SpriteRenderer>().sprite;
        }
        private void DashEnd()
        {
            isDash = false;

        }

    }
}
