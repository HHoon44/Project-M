using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Define;

namespace ProjectM.InGame
{

    //점프를 하는 몬스터가의 움직임
    public class GameMobJump : GameAutoMovement
    {
        [Space(10f)]
        [Header("JumpInfo")]
        [SerializeField] protected float jumpFarce;
        [Range(0, 30)]
        [SerializeField] protected float minJumpCooltime;
        [Range(0, 30)]
        [SerializeField] protected float maxJumpCooltime;

        private void Jump() => rigid.AddForce(Vector2.up * jumpFarce, ForceMode2D.Impulse);

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
                while (Mathf.Abs(rigid.velocity.y) >= 0.01f)
                    yield return new WaitForFixedUpdate();
                if (IsGround())
                    Jump();
            }
        }


    }

}
