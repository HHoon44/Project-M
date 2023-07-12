using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Define;

namespace ProjectM.InGame
{
    public class GameMobMovement : MonoBehaviour
    {
        [Header("BaseMove")]
        public float speed;
        [Range(0.2f, 5)]
        public float detectArea;
        [Space(10f)]
        [Range(0, 10)]
        //todo: �ִ��ּ� �����
        public float moveCooltime;
        [Space(10f)]
        [Range(0, 10)]
        public float idleCooltime;

        [Space(10f)]
        [Header("JumpMove")]
        public float jumpFarce;
        [Range(0, 10)]
        public float jumpCooltime; //������ 1�ʷ� �ؼ� ���� �������� �ʾҴµ� �ٽ� ������ �����Ѵ�.

        readonly static float Deviation = .5f;   //������ �� ����

        Rigidbody2D rigid;

        private KindOfMob mobType;
        private MobMovememt movememtState = MobMovememt.None;

        //�ʱ�ȭ ���
        private Vector3 startPos;

        //������Ʈ ����
        private float nextStap;
        private bool flipX = false;
        private bool moveAble;

        private void Start()
        {
            rigid = GetComponent<Rigidbody2D>();

            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("���� �� ���� ������Ʈ�� �ٸ� ��ü�� ������ �ֽ��ϴ�. " + transform.GetSiblingIndex());
                this.enabled = false;
            }

            mobType = GetComponent<GameMob>().thisMobType;
            //���� �������� ������ ������ �����´�.
            movememtState = GameMobStaticData.Instance.GetMobReferenceInfo(mobType).movement;

            if (!IsGround())
                Debug.LogWarning("���Ͱ� ���߿� �� ���·� ���۵Ǿ����ϴ�. " + transform.GetSiblingIndex());

            startPos = transform.position;

            StartMoveState();
        }

        //�ڷ�ƾ Ȱ��ȭ �� �ʹ� ������ ����
        //todo: �ڷ�ƾ�� Ȱ��ȭ �� �ٽ� ���־�� �Ѵ�.
        private void StartMoveState()
        {
            switch (movememtState)
            {
                case MobMovememt.None:
                    break;
                case MobMovememt.HorizontalMovement:
                    break;
                case MobMovememt.JumpMovement:
                    StartCoroutine(JumpTimer_co());
                    break;
                case MobMovememt.DashMovement:
                    break;
            }

            // �������� �ʴ� ���Ͱų�, ������ ��ȯ �ӵ��� 0���϶�
            if (MobMovememt.None != movememtState)
            {
                if (moveCooltime > 0f && idleCooltime > 0f)
                {
                    StartCoroutine(SwitchIdleMoveTimer_co());
                }
                else
                    Debug.LogWarning("������ ������, �޽�, �ø� ��Ÿ�� 0�� �Դϴ�. " + transform.GetSiblingIndex());
            }
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

            //������ ����ؼ� Ȯ���ϰ�, �̻��� �����ϸ� �ø�����
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

        //act: �¿� ������
        private void HorizontalMovement()
        {
            if (!flipX)
                rigid.velocity = new Vector2(speed, rigid.velocity.y);
            else
                rigid.velocity = new Vector2(-speed, rigid.velocity.y);
        }

        //act: �¿�� ���� ������
        private void JumpMovement()
        {
            HorizontalMovement();
        }
        private void Jump() => rigid.AddForce(Vector2.up * jumpFarce, ForceMode2D.Impulse);


        //act: �ֱ������� �뽬�� �ϴ� ������
        private void DashMovement()
        {

        }



        //@ ������ ���� �ڷ�ƾ
        //@=================================================================================================================================================

        //�ڵ����� �̵�, �޽��� �����մϴ�.
        private IEnumerator SwitchIdleMoveTimer_co()
        {
            while (true)
            {
                moveAble = true;
                RandomTurn();
                yield return new WaitForSeconds(Random.Range(moveCooltime / 2f, moveCooltime * (3 / 4f)));
                moveAble = false;
                yield return new WaitForSeconds(Random.Range(idleCooltime / 2f, idleCooltime * (3 / 4f)));
            }
        }
        //�ڵ����� �ð��� ���߾� ������ �մϴ�.
        private IEnumerator JumpTimer_co()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(jumpCooltime / 2f, jumpCooltime * (3 / 4f)));
                while (!IsGround())
                    yield return new WaitForFixedUpdate();
                yield return new WaitForSeconds(0.5f);
                if (IsGround())
                    Jump();
            }
        }


        //@ ���� �ø�
        //@=================================================================================================================================================

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

        //act: (bool) flip�� ���� �ڽ��� �ø��Ѵ�.
        //tip: �̻� ������ �����Ǿ��� �� ȣ��
        private void Turn(bool _flip)
        {
            //�ø����� ����
            flipX = _flip;

            if (!flipX)
            {
                nextStap = Mathf.Abs(detectArea);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                nextStap = -Mathf.Abs(detectArea);
                transform.localScale = new Vector3(1, 1, -1);
            }
        }
        //act: �����ϰ� ������.
        //tip: �ʿ��� �ڿ������� ���ƴٴϰ� �ϱ�����
        private void RandomTurn()
        {
            bool flip = Random.Range(0, 2) == 1 ? true : false;
            Turn(flip);
        }

        //@ ��Ÿ
        //@=================================================================================================================================================

        private bool IsGround() => Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Ground"));
    }
}

