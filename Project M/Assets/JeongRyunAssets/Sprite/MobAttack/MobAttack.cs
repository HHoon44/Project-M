using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    //공격을 가하는 몬스터를 구현하는 부모 입니다.
    public class MobAttack : MonoBehaviour, IMobConsistModule
    {
        public void SetActineModule(bool _act)
        {
            throw new System.NotImplementedException();
        }

        public void StartForMob(MobBase _mob)
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
