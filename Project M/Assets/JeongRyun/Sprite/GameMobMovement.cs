using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    public class GameMobMovement : MonoBehaviour
    {
        GameMob mob;
        private MobMovememt movememtState = MobMovememt.None;

        private Vector3 startPos;
        private float speed;

        private float nextStap;
        private bool flipX;

        private void Start()
        {
            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("현재 몹 전용 컴포넌트를 다른 객체가 가지고 있습니다.");
                this.enabled = false;
            }

            mob = GetComponent<GameMob>();
            if (mob == null)
                Debug.LogWarning("GameMob 컨포넌트가 없습니다.");

            startPos = transform.position;
            flipX = Random.Range(0, 2) == 1 ? true : false; //시작 시 앞뒤를 랜덤하게 만든다.

            Invoke(nameof(LateStart), 0);
        }

        private void LateStart()
        {
            speed = mob.mobInfo.speed;
            movememtState = mob.mobInfo.movement;
            Debug.Log(movememtState.ToString());
        }

        private void FixedUpdate()
        {
            //만약 시작 위치보다 떨어져 있다면, 다시 위치를 초기화 한다.
            if ((startPos.y - 1) >= transform.position.y)
                transform.position = startPos;

            //몬스터 마다 고유의 움직임
            switch (movememtState)
            {
                case MobMovememt.None:
                    None();
                    break;
                case MobMovememt.HorizontalMovement:
                    HorizontalMovement();
                    break;
                case MobMovememt.JumpMovement:
                    JumpMovement();
                    break;
                case MobMovememt.DashMovement:
                    JumpMovement();
                    break;
            }

            //if (movememtState != MobMovememt.None)
            GroundSense();
        }

        //act: 가만히 있음
        private void None()
        {

        }

        //act: 좌우로 움직임
        private void HorizontalMovement()
        {
            nextStap = speed/3;
        }

        //act: 점프를 하면서 움직임
        private void JumpMovement()
        {
            // if (Physics2D.Linecast(BottomRayTip.position, BottomRayTip.position, 1 << LayerMask.NameToLayer("Ground"))) //땅만 인식한다.
            // {
            //     Debug.Log("a");
        }

        private void DashMovement()
        {

        }

        //act: 몬스터가 돌아야 할 때를 알려준다.
        //tip: 근처에 낭떠러지거나, 벽이면 ture를 리턴.
        private bool GroundSense()
        {
            Vector2 nextPos = new Vector2(transform.position.x + nextStap, transform.position.y);
            Debug.Log("Detect start");

            //낭떠러지가 있는 지 확인.
            Debug.DrawRay(nextPos, Vector3.down, Color.green);
            if (Physics2D.Raycast(nextPos, Vector3.down * 1, 1, LayerMask.GetMask("Ground")))
            {
                //return true;
                Debug.Log("detect1");
            }
            //벽이 있는 지 확인/
            Debug.DrawLine(transform.position, nextPos, Color.red);
            if (Physics2D.Linecast(transform.position, nextPos, LayerMask.GetMask("Ground")))
            {
                Debug.Log("detect2");
                //return true;
            }

            return false;
        }

        private void Turn()
        {
            // if (!flipX)
            // {
            //     nextStap = 1;
            //     transform.localScale = new Vector3(1, 1, 1);
            // }
            // else
            // {
            //     nextStap = -1;
            //     transform.localScale = new Vector3(1, 1, -1);
            // }

            // CancelInvoke();
            // Invoke("Think", 5);
        }

        void Think()
        {
            // //Set Next Active
            // nextMove = Random.Range(-1, 2);

            // //Sprite Animation
            // animator.SetInteger("WalkSpeed", nextMove);

            // //Flip Sprite
            // if (nextMove != 0)
            // {
            //     renderer.flipX = nextMove == 1;
            // }

            // //Set Next Active
            // float nextThinkTime = Random.Range(2f, 5f);
            // Invoke("Think", nextThinkTime);
        }

    }
}

