using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    //������ ���ϴ� ���͸� �����ϴ� �θ� �Դϴ�.
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

        void Start()
        {
            tip = mob.atkTip;
        }

        void Update()
        {

        }
    }
}
