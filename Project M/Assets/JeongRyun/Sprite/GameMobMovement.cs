using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    public class GameMobMovement : MonoBehaviour
    {
        Rigidbody2D rigid;

        private KindOfMob mobType;
        private MobMovememt movememtState = MobMovememt.None;

        private Vector3 startPos;
        private float speed;

        private float nextStap;
        private bool flipX = false;

        private void Start()
        {
            rigid = GetComponent<Rigidbody2D>();

            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("���� �� ���� ������Ʈ�� �ٸ� ��ü�� ������ �ֽ��ϴ�.");
                this.enabled = false;
            }

            mobType = GetComponent<GameMob>().thisMobType;

            startPos = transform.position;
            Turn(Random.Range(0, 2) == 1 ? true : false); //���� �� �յڸ� �����ϰ� �����.

            speed = GameMobStaticData.Instance.GetMobReferenceInfo(mobType).speed;
            movememtState = GameMobStaticData.Instance.GetMobReferenceInfo(mobType).movement;
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

            //������ �����ϸ� �ø��Ѵ�.
            if (movememtState != MobMovememt.None)
                if (GroundSense())
                    if (rigid.velocity.x > 0)
                        Turn(true);
                    else
                        Turn(false);
        }

        //act: ������ ����
        private void None()
        {

        }

        //act: �¿�� ������
        private void HorizontalMovement()
        {
            if (!flipX)
                rigid.velocity = new Vector2(speed, rigid.velocity.y);
            else
                rigid.velocity = new Vector2(-speed, rigid.velocity.y);
        }

        //act: ������ �ϸ鼭 ������
        private void JumpMovement()
        {

        }

        //act: ���� �ð����� �뽬�� �Ѵ�.
        private void DashMovement()
        {

        }

        //act: ���Ͱ� ���ƾ� �� ���� �˷��ش�.
        //tip: ��ó�� ���������ų�, ���̸� ture�� ����.
        private bool GroundSense()
        {
            //���� ��ġ�� ������� �̸� �˻��Ѵ�.
            Vector2 nextPos = new Vector2(transform.position.x + nextStap, transform.position.y);
            Debug.Log("Detect start");

            //���������� �ִ� �� Ȯ��.
            Debug.DrawRay(nextPos, Vector3.down * (transform.position.y - startPos.y + 1), Color.green);
            if (!Physics2D.Raycast(nextPos, Vector3.down, transform.position.y - startPos.y + 1, LayerMask.GetMask("Ground")))
            {
                Debug.Log("detect1");
                return true;
            }

            //���� �ִ� �� Ȯ��.
            Debug.DrawLine(transform.position, nextPos, Color.red);
            if (Physics2D.Linecast(transform.position, nextPos, LayerMask.GetMask("Ground")))
            {
                Debug.Log("detect2");
                return true;
            }

            return false;
        }

        private void Turn(bool _flip)
        {
            SetNextStap();

            //�ø����� ����
            flipX = _flip;

            if (!flipX)
            {
                nextStap = Mathf.Abs(nextStap);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                nextStap = -Mathf.Abs(nextStap);
                transform.localScale = new Vector3(1, 1, -1);
            }
        }

        /// <summary>
        ///act: ���Ͱ� ����ؼ� �̵��Ѵٸ�, ���� �ڸ��� ��� ���� �̸� ����Ѵ�.
        ///tip: ���Ͱ� �������� ���� ���� �� ����
        /// </summary>
        private void SetNextStap()
        {
            switch (movememtState)
            {
                case MobMovememt.None:
                    nextStap = 0;
                    break;
                case MobMovememt.HorizontalMovement:
                    nextStap = speed / 3;
                    break;
                case MobMovememt.JumpMovement:
                    nextStap = speed;
                    break;
                case MobMovememt.DashMovement:
                    //todo: �뽬 �Ÿ� ���
                    break;
                default:
                    Debug.Log("�� �� ���� ������");
                    movememtState = MobMovememt.None;
                    break;
            }
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

