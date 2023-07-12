using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    public class GameMobMovement : MonoBehaviour
    {
        [Header("BaseMove")]
        public float mobSize;
        [Range(0, 10)]
        public float moveTime;
        [Range(0, 10)]
        public float idleTime;

        [Space(10f)]
        [Header("JumpMove")]
        public float jumpFarce;
        [Range(1, 10)]
        public float jumpTiming; //������ 1�ʷ� �ؼ� ���� �������� �ʾҴµ� �ٽ� ������ �����Ѵ�.


        Rigidbody2D rigid;

        private KindOfMob mobType;
        private MobMovememt movememtState = MobMovememt.None;

        //�ʱ�ȭ ���
        private Vector3 startPos;
        private float speed;

        //������Ʈ ����
        private float nextStap;
        private bool flipX = false;
        private bool moveAble;

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

            speed = GameMobStaticData.Instance.GetMobReferenceInfo(mobType).speed;
            movememtState = GameMobStaticData.Instance.GetMobReferenceInfo(mobType).movement;

            StartCoroutine(IdleToMove());

            if (movememtState == MobMovememt.JumpMovement)
                StartCoroutine(JumpTimer());
        }

        private void FixedUpdate()
        {
            //���� ���� ��ġ���� ������ �ִٸ�, �ٽ� ��ġ�� �ʱ�ȭ �Ѵ�.
            if ((startPos.y - 1) >= transform.position.y)
                transform.position = startPos;

            if (moveAble)
            {
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
            }
            else
            {
                rigid.velocity = Vector2.up * rigid.velocity;
            }

            //������ �����ϸ� �ø��Ѵ�.
            if (movememtState != MobMovememt.None)
                if (GroundSense())
                    if (rigid.velocity.x > 0)
                        Turn(true);
                    else
                        Turn(false);
        }

        private IEnumerator IdleToMove()
        {
            while (true)
            {
                moveAble = true;
                RandomTurn();
                yield return new WaitForSeconds(Random.Range(moveTime / 2, moveTime));
                moveAble = false;
                yield return new WaitForSeconds(Random.Range(idleTime / 2, idleTime));
            }
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
            HorizontalMovement();
        }
        private IEnumerator JumpTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(jumpTiming);
                rigid.AddForce(Vector2.up * jumpFarce, ForceMode2D.Impulse);
                Debug.Log("Jump");
            }
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

            //���������� �ִ� �� Ȯ��.
            Debug.DrawRay(nextPos, Vector3.down * (transform.position.y - startPos.y + 1), Color.green);
            if (!Physics2D.Raycast(nextPos, Vector3.down, transform.position.y - startPos.y + 1, LayerMask.GetMask("Ground")))
            {
                return true;
            }

            //���� �ִ� �� Ȯ��.
            Debug.DrawLine(transform.position, nextPos, Color.red);
            if (Physics2D.Linecast(transform.position, nextPos, LayerMask.GetMask("Ground")))
            {
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

        private void RandomTurn()
        {
            bool flip = Random.Range(0, 2) == 1 ? true : false;
            Turn(flip);
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
                    nextStap = speed /2;
                    break;
                case MobMovememt.JumpMovement:
                    nextStap = speed / 4;
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

