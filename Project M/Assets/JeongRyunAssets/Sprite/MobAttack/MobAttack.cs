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

        private GameObject myProjectile;
        private int attackCount;

        [SerializeField] private float attackCooltime;



        //! 인터페이스 구현
        public void Initialize(MobBase _mob)
        {
            mob = _mob;
            gameObject.tag = "Mob";
            gameObject.name = "AttackModule";

            transform.localPosition = mob.atkTip.localPosition;

            attackCooltime = mob.myReference.atkCooltime;
        }
        public void SetActiveModule(bool _act)
        {
        }
        public GameObject thisObj()
        {
            return gameObject;
        }

        void Start()
        {
            tip = mob.atkTip;
            myProjectile = MobsStaticData.Instance.GetMobProjectilePrefab(mob.thisMobType);
            StartCoroutine(AtkCooltime_co());
        }

        private IEnumerator AtkCooltime_co()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();

                if (!mob.discoveryPlayer)
                    continue;

                Attack();
                yield return new WaitForSeconds(attackCooltime);
            }
        }
        private void Attack()
        {
            attackCount++;
            Instantiate(myProjectile, EffectGroupManager.Instance.transform);
        }

    }
}
