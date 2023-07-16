using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Define;

namespace ProjectM.InGame
{
    //������ �������� �ڿ������� �ϱ� ���� ������ ���� �̷������ ������ �ν����Ϳ��� �ַ� �ٷ��.
    public class MobMovement : MonoBehaviour, IMobConsistModule
    {
        [Header("MoveInfo")]
        [SerializeField] protected float speed;

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

        [SerializeField] protected bool atDiscoverPlayerStop = false; //�÷��̾ ��ó�� �ִٴ� ���� �˾��� ���� ������

        protected Rigidbody2D rigid;
        protected MobBase mob;

        //�ʱ�ȭ ���
        private float groundDis;
        private Vector2 startPos;

        //������Ʈ ����
        private float nextStap;
        private bool flipX = false;
        private bool isGround;
        private bool moveAble;


        protected virtual void Start()
        {
            //����ó��
            rigid = mob.GetComponent<Rigidbody2D>();
            if (rigid == null)
                Debug.LogWarning("rigid����" + thisOrder());

            groundDis = Physics2D.Raycast(mob.transform.position, Vector2.down, 100, LayerMask.GetMask("Ground")).distance;
            //Debug.Log(Physics2D.Raycast(mob.transform.position, -Vector2.down, 100, LayerMask.GetMask("Ground")).transform.name);
            Debug.Log(groundDis);
            
            startPos = mob.transform.position;

            if (minMoveTime >= maxMoveTime)
                maxMoveTime = minMoveTime;
            if (minIdleTime >= maxIdleTime)
                maxIdleTime = minIdleTime;

            //������ ����
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
            isGround = IsGround();
            //���� ���� ��ġ���� ������ �ִٸ�, �ٽ� ��ġ�� �ʱ�ȭ �Ѵ�.
            if ((startPos.y - 1) >= transform.position.y)
                if (isGround)
                    transform.position = startPos;
                else
                    moveAble = false;

            if (atDiscoverPlayerStop && mob.discoveryPlayer)
                moveAble = false;

            //���� �����̴� ���� �߿� ���͸� ���� ���ߴ� ���Ϳ��� �÷��̾ �����Ǿ��� ���� �����.
            if (moveAble)
            {
                HorizontalMovement();
            }
            else
                rigid.velocity = Vector2.up * rigid.velocity;

            //������ ����ؼ� Ȯ���ϰ�, �̻��� �����ϸ� �ø�����
            if (speed != 0)
                if (GroundSense())
                    if (!flipX)
                        Turn(true);
                    else
                        Turn(false);
        }

        //act: �¿� ������
        private void HorizontalMovement()
        {
            if (!flipX)
                rigid.velocity = new Vector2(speed, rigid.velocity.y);
            else
                rigid.velocity = new Vector2(-speed, rigid.velocity.y);
        }

        //@ ������ ���� �ڷ�ƾ
        //@=================================================================================================================================================

        //act: �ڵ����� �̵�, �޽��� �����մϴ�.
        private IEnumerator SwitchIdleMoveTimer_co()
        {
            while (true)
            {
                while (!isGround || Mathf.Abs(rigid.velocity.y) >= 0.01f)
                    yield return new WaitForSeconds(0.1f);                //���߿� ������ �ð����� ����

                moveAble = true;
                RandomTurn();
                yield return new WaitForSeconds(Random.Range(minMoveTime, maxMoveTime));

                while (!isGround || Mathf.Abs(rigid.velocity.y) >= 0.01f)
                    yield return new WaitForSeconds(0.1f);

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
            Vector2 nextPos = new Vector2(transform.position.x + ((mob.colPoint.x + 0.1f) * (flipX ? -1 : 1)), transform.position.y);

            Debug.DrawLine(nextPos, new Vector2(nextPos.x, startPos.y - (groundDis + 0.1f)), Color.green);
            if (!Physics2D.Linecast(nextPos, new Vector2(nextPos.x, startPos.y - (groundDis + 0.1f)), LayerMask.GetMask("Ground")))
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
                nextStap = .2f * speed;
                mob.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                nextStap = -.2f * speed;
                mob.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        //act: �����ϰ� ������.
        //tip: �ʿ��� �ڿ������� ���ƴٴϰ� �ϱ�����
        private void RandomTurn()
        {
            if (atDiscoverPlayerStop && mob.discoveryPlayer)
                return;
            bool flip = Random.Range(0, 2) == 1 ? true : false;
            Turn(flip);
        }

        //@ ��Ÿ
        //@=================================================================================================================================================

        protected bool IsGround() => Physics2D.Raycast(transform.position, Vector2.down, groundDis + .1f, LayerMask.GetMask("Ground"));
        private int thisOrder() => transform.GetSiblingIndex();

        public void Initialize(MobBase _mob)
        {
            mob = _mob;
            gameObject.tag = "Mob";
            gameObject.name = "MovementModule";

            transform.localPosition = Vector3.zero;

            speed = mob.myMovement.speed;
        }

        public void SetActiveModule(bool _act)
        {
        }
    }
}

