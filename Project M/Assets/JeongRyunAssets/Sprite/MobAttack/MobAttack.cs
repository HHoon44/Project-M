using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    //몬스터의 공격을 구현하는 모듈입니다.
    public class MobAttack : MonoBehaviour, IMobConsistModule
    {
        protected MobBase mob;
        private Transform tip;

        public void Initialize(MobBase _mob)
        {
            mob = _mob;
            gameObject.tag = "Mob";
            gameObject.name = "AttackModule";

            transform.localPosition = mob.atkTip.localPosition;
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
            tip = mob.atkTip;
        }

        void Update()
        {

        }
    }
}
