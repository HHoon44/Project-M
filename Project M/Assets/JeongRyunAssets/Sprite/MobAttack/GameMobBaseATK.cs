using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    //공격을 가하는 몬스터를 구현하는 부모 입니다.
    public class GameMobBaseATK : MonoBehaviour
    {
        private MobBase mob;

        void Start()
        {
            mob = GetComponent<MobBase>();
        }

        void Update()
        {

        }
    }
}
