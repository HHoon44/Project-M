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
                Debug.LogWarning("���� �� ���� ������Ʈ�� �ٸ� ��ü�� ������ �ֽ��ϴ�.");
                this.enabled = false;
            }

            startPos = transform.position;
            flipX = Random.Range(0, 2) == 1 ? true : false;

            // speed = GameMobStaticData.Instance.get
        }

        private void FixedUpdate()
        {
            //���� ���� ��ġ���� ������ �ִٸ�, �ٽ� ��ġ�� �ʱ�ȭ �Ѵ�.
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

        //act: ������ ����
        private void None()
        {

        }

        //act: �¿�� ������
        private void HorizontalMovement()
        {


            Vector2 frontVec = new Vector2(transform.position.x + nextStap, transform.position.y);

            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
            if (Physics2D.Raycast(frontVec, Vector3.down * 1, 1, 1 << LayerMask.NameToLayer("Ground")))
            {
                Debug.Log("�������� �߰� �ڷ� ��");
                Turn();
            }
        }

        //act: ������ �ϸ鼭 ������
        private void JumpMovement()
        {
            // if (Physics2D.Linecast(BottomRayTip.position, BottomRayTip.position, 1 << LayerMask.NameToLayer("Ground"))) //���� �ν��Ѵ�.
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

