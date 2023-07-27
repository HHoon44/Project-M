using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    public interface IMobConsistModule
    {
        //tip: 몬스터가 모듈 생성시 바로 호출되는 함수 (Awake와 동일)
        public void Initialize(MobBase _mob);

        //tip: 해당 모듈을 제어할 수 있음
        public void SetActiveModule(bool _act);

        public GameObject thisObj();
    }
}
