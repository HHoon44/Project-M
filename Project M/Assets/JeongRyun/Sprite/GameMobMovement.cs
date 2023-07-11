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
                Debug.LogWarning("���� �� ���� ������Ʈ�� �ٸ� ��ü�� ������ �ֽ��ϴ�.");
                this.enabled = false;
            }

            mob = GetComponent<GameMob>();
            if (mob == null)
                Debug.LogWarning("GameMob ������Ʈ�� �����ϴ�.");

            startPos = transform.position;
            flipX = Random.Range(0, 2) == 1 ? true : false; //���� �� �յڸ� �����ϰ� �����.

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
            //���� ���� ��ġ���� ������ �ִٸ�, �ٽ� ��ġ�� �ʱ�ȭ �Ѵ�.
            if ((startPos.y - 1) >= transform.position.y)
                transform.position = startPos;

            //���� ���� ������ ������
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

        //act: ������ ����
        private void None()
        {

        }

        //act: �¿�� ������
        private void HorizontalMovement()
        {
            nextStap = speed/3;
        }

        //act: ������ �ϸ鼭 ������
        private void JumpMovement()
        {
            // if (Physics2D.Linecast(BottomRayTip.position, BottomRayTip.position, 1 << LayerMask.NameToLayer("Ground"))) //���� �ν��Ѵ�.
            // {
            //     Debug.Log("a");
        }

        private void DashMovement()
        {

        }

        //act: ���Ͱ� ���ƾ� �� ���� �˷��ش�.
        //tip: ��ó�� ���������ų�, ���̸� ture�� ����.
        private bool GroundSense()
        {
            Vector2 nextPos = new Vector2(transform.position.x + nextStap, transform.position.y);
            Debug.Log("Detect start");

            //���������� �ִ� �� Ȯ��.
            Debug.DrawRay(nextPos, Vector3.down, Color.green);
            if (Physics2D.Raycast(nextPos, Vector3.down * 1, 1, LayerMask.GetMask("Ground")))
            {
                //return true;
                Debug.Log("detect1");
            }
            //���� �ִ� �� Ȯ��/
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

