using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    public interface IMobConsistModule
    {
        public void Initialize(MobBase _mob);
        public void SetActiveModule(bool _act);
    }
}
