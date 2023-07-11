using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    public class GameMobMovement : MonoBehaviour
    {
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

            startPos = transform.position;
            flipX = Random.Range(0, 2) == 1 ? true : false;

            // speed = GameMobStaticData.Instance.get
        }

        private void FixedUpdate()
        {
            //만약 시작 위치보다 떨어져 있다면, 다시 위치를 초기화 한다.
            if ((startPos.y - 1) >= transform.position.y)
                transform.position = startPos;

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
        }

        //act: 가만히 있음
        private void None()
        {

        }

        //act: 좌우로 움직임
        private void HorizontalMovement()
        {


            Vector2 frontVec = new Vector2(transform.position.x + nextStap, transform.position.y);

            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
            if (Physics2D.Raycast(frontVec, Vector3.down * 1, 1, 1 << LayerMask.NameToLayer("Ground")))
            {
                Debug.Log("낭떠러지 발견 뒤로 돎");
                Turn();
            }
        }

        //act: 점프를 하면서 움직임
        private void JumpMovement()
        {
            // if (Physics2D.Linecast(BottomRayTip.position, BottomRayTip.position, 1 << LayerMask.NameToLayer("Ground"))) //땅만 인식한다.
            // {
            //     Debug.Log("a");
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

