using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    //������ ���ϴ� ���͸� �����ϴ� �θ� �Դϴ�.
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
