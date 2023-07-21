using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    //점프를 하는 몬스터가의 움직임
    public class MobJump : MonoBehaviour, IMobConsistModule
    {
        private MobBase mob;
        [Space(10f)]
        [Header("JumpInfo")]
        [SerializeField] private float jumpFarce;
        [Range(0, 30)]
        [SerializeField] private float minJumpCooltime;
        [Range(0, 30)]
        [SerializeField] private float maxJumpCooltime;

        public void Initialize(MobBase _mob)
        {
            mob = _mob;
            gameObject.tag = "Mob";
            gameObject.name = "JumpModule";

            transform.position = Vector3.zero;

            jumpFarce = _mob.myMovement.jumpForce;
            minJumpCooltime = _mob.myMovement.minJumpCooltime;
            maxJumpCooltime = _mob.myMovement.maxJumpCooltime;
        }

        public void SetActiveModule(bool _act)
        {

        }

        public GameObject thisObj()
        {
            return this.gameObject;
        }

        private void Start()
        {
            StartCoroutine(JumpTimer_co());
        }

        //자동으로 시간에 맞추어 점프를 합니다.
        private IEnumerator JumpTimer_co()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minJumpCooltime, maxJumpCooltime));
                if (!mob.discoveryPlayer) //플레이어가 감지 되면 점프는 하지 않는다.
                {
                    if (mob.isGround)
                    {
                        Debug.Log("점프 실행");
                        Jump();
                    }
                }
            }
        }

        private void Jump() => mob.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpFarce, ForceMode2D.Impulse);
    }

}
