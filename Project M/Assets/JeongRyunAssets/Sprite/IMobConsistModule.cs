using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    public interface IMobConsistModule
    {
        public void StartForMob(MobBase _mob);
        public void SetActineModule(bool _act);
    }
}
