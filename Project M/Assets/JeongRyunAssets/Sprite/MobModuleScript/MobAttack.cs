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

        [SerializeField] private GameObject myProjectile;
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
        private void FixedUpdate() {
            
            Attack();
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
            GameObject projectile = Instantiate(myProjectile, EffectGroupManager.Instance.transform);

            Vector2 dis = PlayerController.GetPlayerTip() - mob.atkTip.position;
            float angle = Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg;

            projectile.GetComponent<MobProjectileBase>().Initialize(mob.atkTip.position, angle, mob.transform.localScale.x);
        }

    }
}
