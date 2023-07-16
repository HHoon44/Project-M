using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    //공격을 가하는 몬스터를 구현하는 부모 입니다.
    public class MobAttack : MonoBehaviour, IMobConsistModule
    {
        protected MobBase mob;

        public void Initialize(MobBase _mob)
        {
            mob = _mob;
            gameObject.tag = "Mob";
            gameObject.name = "AttackModule";
        }

        public void SetActiveModule(bool _act)
        {
            throw new System.NotImplementedException();
        }


        void Start()
        {
        }

        void Update()
        {

        }
    }
}
