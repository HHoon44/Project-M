using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Define;

namespace ProjectM.InGame
{
    public class GameAutoMovement : MonoBehaviour
    {
        [Header("MoveInfo")]
        [SerializeField] protected float speed;
        [SerializeField] protected Transform movetip;

        [Space(10f)]
        [Range(0, 30)]
        [SerializeField] protected float minMoveTime;
        [Range(0, 30)]
        [SerializeField] protected float maxMoveTime;

        [Space(10f)]
        [Range(0, 30)]
        [SerializeField] protected float minIdleTime;
        [Range(0, 30)]
        [SerializeField] protected float maxIdleTime;


        protected Rigidbody2D rigid;


        //�ʱ�ȭ ���
        private Vector3 startPos;

        //������Ʈ ����
        private float nextStap;
        private bool flipX = false;
        private bool moveAble;

        protected virtual void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
            if (rigid == null)
                Debug.LogWarning("rigid����" + thisOrder());

            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("���� �� ���� ������Ʈ�� �ٸ� ��ü�� ������ �ֽ��ϴ�. " + thisOrder());
                this.enabled = false;
            }

            if (!IsGround())
                Debug.LogWarning("���Ͱ� ���߿� �� ���·� ���۵Ǿ����ϴ�. " + thisOrder());

            startPos = movetip.position;

            if (minMoveTime >= maxMoveTime)
                maxMoveTime = minMoveTime;
            if (minIdleTime >= maxIdleTime)
                maxIdleTime = minIdleTime;

            StartMoveState();
        }

        private void OnEnable()
        {
            StartMoveState();
        }

        //�ڷ�ƾ Ȱ��ȭ �� �ʹ� ������ ����
        //todo: �ڷ�ƾ�� Ȱ��ȭ �� �ٽ� ���־�� �Ѵ�.
        private void StartMoveState()
        {
            // �������� �ʴ� ���Ͱų�, ������ ��ȯ �ӵ��� 0���϶�
            if (speed != 0)
            {
                if (maxMoveTime > 0f && maxIdleTime > 0f)
                    StartCoroutine(SwitchIdleMoveTimer_co());
                else
                    Debug.LogWarning("������ ������, �޽�, �ø� ��Ÿ�� 0�� �Դϴ�. " + thisOrder());
            }
        }



        private void FixedUpdate()
        {
            //���� ���� ��ġ���� ������ �ִٸ�, �ٽ� ��ġ�� �ʱ�ȭ �Ѵ�.
            if ((startPos.y - 1) >= transform.position.y)
                transform.position = startPos;

            if (moveAble)
                HorizontalMovement();
            else
                rigid.velocity = Vector2.up * rigid.velocity;

            //������ ����ؼ� Ȯ���ϰ�, �̻��� �����ϸ� �ø�����
            if (speed != 0)
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
        protected void HorizontalMovement()
        {
            if (!flipX)
                rigid.velocity = new Vector2(speed, rigid.velocity.y);
            else
                rigid.velocity = new Vector2(-speed, rigid.velocity.y);
        }

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
                yield return new WaitForSeconds(Random.Range(minMoveTime, maxMoveTime));
                moveAble = false;
                yield return new WaitForSeconds(Random.Range(minIdleTime, minIdleTime));
            }
        }



        //@ ���� �ø�
        //@=================================================================================================================================================

        //act: ���Ͱ� ���ƾ� �� ���� �˷��ش�.
        //tip: ��ó�� ���������ų�, ���̸� ture�� ����.
        private bool GroundSense()
        {
            //���� ��ġ�� ������� �̸� �˻��Ѵ�.
            Vector2 nextPos = new Vector2(movetip.position.x + nextStap, movetip.position.y);

            //���������� �ִ� �� Ȯ��.
            Debug.DrawRay(nextPos, Vector3.down * (movetip.position.y - startPos.y + .3f), Color.green);
            if (!Physics2D.Raycast(nextPos, Vector3.down, movetip.position.y - startPos.y + .3f, LayerMask.GetMask("Ground")))
            {
                return true;
            }

            //���� �ִ� �� Ȯ��.
            Debug.DrawLine(movetip.position, nextPos, Color.red);
            if (Physics2D.Linecast(movetip.position, nextPos, LayerMask.GetMask("Ground")))
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
                nextStap = .2f * speed;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                nextStap = -.2f * speed;
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

        protected bool IsGround() => Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Ground"));
        private int thisOrder() => transform.GetSiblingIndex();
    }
}

