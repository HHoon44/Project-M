using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    //점프를 하는 몬스터가의 움직임
    public class MobJump : MobMovement
    {
        [Space(10f)]
        [Header("JumpInfo")]
        [SerializeField] protected float jumpFarce;
        [Range(0, 30)]
        [SerializeField] protected float minJumpCooltime;
        [Range(0, 30)]
        [SerializeField] protected float maxJumpCooltime;

        protected override void Start()
        {
            base.Start();

            if (minJumpCooltime >= maxJumpCooltime)
                maxJumpCooltime = minJumpCooltime;

            StartCoroutine(JumpTimer_co());
        }

        //자동으로 시간에 맞추어 점프를 합니다.
        private IEnumerator JumpTimer_co()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minJumpCooltime, maxJumpCooltime));
                if (!atDiscoverPlayerStop || !mob.discoveryPlayer) //플레이어가 감지 되면 점프는 하지 않는다.
                {
                    while (Mathf.Abs(rigid.velocity.y) >= 0.01f)
                        yield return new WaitForFixedUpdate();
                    if (IsGround())
                        Jump();
                }
            }
        }
        private void Jump() => rigid.AddForce(Vector2.up * jumpFarce, ForceMode2D.Impulse);

        public void InitMob(GameObject _obj)
        {
            throw new System.NotImplementedException();
        }
    }

}
