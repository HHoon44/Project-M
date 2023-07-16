using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Define;

namespace ProjectM.InGame
{
    //몬스터의 움직임은 자연스럽게 하기 위한 수정이 많이 이루어지기 때문에 인스펙터에서 주로 다룬다.
    public class MobMovement : MonoBehaviour, IMobConsistModule
    {
        [Header("MoveInfo")]
        [SerializeField] protected float speed;

        [Space(10f)]
        [Range(0, 30)]
        [SerializeField] private float minMoveTime;
        [Range(0, 30)]
        [SerializeField] private float maxMoveTime;

        [Space(10f)]
        [Range(0, 30)]
        [SerializeField] private float minIdleTime;
        [Range(0, 30)]
        [SerializeField] private float maxIdleTime;

        [SerializeField] protected bool atDiscoverPlayerStop = false; //플레이어가 근처에 있다는 것을 알았을 때의 움직임

        protected Rigidbody2D rigid;
        protected MobBase mob;

        //초기화 상수
        private float groundDis;
        private Vector2 startPos;

        //오브젝트 변수
        private float nextStap;
        private bool flipX = false;
        public bool isGround { get; private set; }
        private bool moveAble;


        protected virtual void Start()
        {
            //예외처리
            rigid = mob.GetComponent<Rigidbody2D>();
            if (rigid == null)
                Debug.LogWarning("rigid없음" + thisOrder());

            //바닥으로 ray를 발사하여 몬스터의 피벗과 바닥의 거리차이를 기록한다
            groundDis = Physics2D.Raycast(mob.transform.position, Vector2.down, 100, LayerMask.GetMask("Ground")).distance;
            if (groundDis == 0)
                Debug.Log("몬스터가 공중에서 생성되었습니다.");

            startPos = mob.transform.position;

            MobMovementData m = mob.myMovement;
            speed = m.speed;
            minMoveTime = m.minMoveTime;
            maxMoveTime = m.maxMoveTime;
            minIdleTime = m.minIdleTime;
            maxIdleTime = m.maxIdleTime;
            atDiscoverPlayerStop = m.atDiscoverPlayerStop;

            if (minMoveTime >= maxMoveTime)
                maxMoveTime = minMoveTime;
            if (minIdleTime >= maxIdleTime)
                maxIdleTime = minIdleTime;

            //움직임 시작
            StartMoveState();
        }

        //코루틴 활성화 및 초반 움직임 세팅
        //todo: 코루틴은 활성화 시 다시 켜주어야 한다.
        private void StartMoveState()
        {
            // 움직이지 않는 몬스터거나, 움직임 전환 속도가 0초일때
            if (speed != 0)
            {
                if (maxMoveTime > 0f || maxIdleTime > 0f)
                    StartCoroutine(SwitchIdleMoveTimer_co());
                else
                    Debug.LogWarning("몬스터의 움직임, 휴식 쿨타임 0초 입니다. " + thisOrder());
            }
        }

        private void FixedUpdate()
        {
            isGround = IsGround();
            //만약 시작 위치보다 떨어져 있다면, 다시 위치를 초기화 한다.
            if ((startPos.y - 1) >= transform.position.y)
                if (isGround)
                    transform.position = startPos;
                else
                    moveAble = false;

            if (atDiscoverPlayerStop && mob.discoveryPlayer)
                moveAble = false;

            //원래 움직이는 몬스터 중에 몬스터를 보면 멈추는 몬스터에서 플레이어가 감지되었을 때만 멈춘다.
            if (moveAble)
                HorizontalMovement();
            else
                rigid.velocity = Vector2.up * rigid.velocity;

            //지형을 계속해서 확인하고, 이상을 감지하면 플립실행
            if (speed != 0)
                if (GroundSense())
                    if (!flipX)
                        Turn(true);
                    else
                        Turn(false);
        }

        //act: 좌우 움직임
        private void HorizontalMovement()
        {
            if (!flipX)
                rigid.velocity = new Vector2(speed, rigid.velocity.y);
            else
                rigid.velocity = new Vector2(-speed, rigid.velocity.y);
        }

        //@ 움직임 패턴 코루틴
        //@=================================================================================================================================================

        //act: 자동으로 이동, 휴식을 변경합니다.
        private IEnumerator SwitchIdleMoveTimer_co()
        {
            while (true)
            {
                while (!isGround || Mathf.Abs(rigid.velocity.y) >= 0.01f)
                    yield return new WaitForSeconds(0.1f);                //공중에 떠있을 시간동안 버퍼

                moveAble = true;
                RandomTurn();
                yield return new WaitForSeconds(Random.Range(minMoveTime, maxMoveTime));

                while (!isGround || Mathf.Abs(rigid.velocity.y) >= 0.01f)
                    yield return new WaitForSeconds(0.1f);

                moveAble = false;
                yield return new WaitForSeconds(Random.Range(minIdleTime, minIdleTime));
            }
        }

        //@ 몬스터 플립
        //@=================================================================================================================================================

        //act: 몬스터가 돌아야 할 때를 알려준다.
        //tip: 근처에 낭떠러지거나, 벽이면 ture를 리턴.
        private bool GroundSense()
        {
            //다음 위치가 어디일지 미리 검색한다.
            Vector2 nextPos = new Vector2(transform.position.x + ((mob.colPoint.x + 0.1f) * (flipX ? -1 : 1)), transform.position.y);

            Debug.DrawLine(nextPos, new Vector2(nextPos.x, startPos.y - (groundDis + 0.1f)), Color.green);
            if (!Physics2D.Linecast(nextPos, new Vector2(nextPos.x, startPos.y - (groundDis + 0.1f)), LayerMask.GetMask("Ground")))
                return true;

            //벽이 있는 지 확인.
            Debug.DrawLine(transform.position, nextPos, Color.red);
            if (Physics2D.Linecast(transform.position, nextPos, LayerMask.GetMask("Ground")))
                return true;

            return false;
        }

        //act: (bool) flip에 따라 자신을 플립한다.
        //tip: 이상 지형이 감지되었을 때 호출
        private void Turn(bool _flip)
        {
            //플립정보 반전
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

        //act: 랜덤하게 돌린다.
        //tip: 맵에서 자연스럽게 돌아다니게 하기위해
        private void RandomTurn()
        {
            //만약 
            if (atDiscoverPlayerStop && mob.discoveryPlayer)
                return;

            bool flip = Random.Range(0, 2) == 1 ? true : false;
            Turn(flip);
        }

        //@ 기타
        //@=================================================================================================================================================

        protected bool IsGround() => Physics2D.Raycast(transform.position, Vector2.down, groundDis + .1f, LayerMask.GetMask("Ground"));
        private int thisOrder() => transform.GetSiblingIndex();

        public void Initialize(MobBase _mob)
        {
            mob = _mob;
            gameObject.tag = "Mob";
            gameObject.name = "MovementModule";

            transform.localPosition = Vector3.zero;
        }

        public void SetActiveModule(bool _act)
        {
        }
    }
}

