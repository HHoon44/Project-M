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


        //초기화 상수
        private Vector3 startPos;

        //오브젝트 변수
        private float nextStap;
        private bool flipX = false;
        private bool moveAble;

        protected virtual void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
            if (rigid == null)
                Debug.LogWarning("rigid없음" + thisOrder());

            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("현재 몹 전용 컴포넌트를 다른 객체가 가지고 있습니다. " + thisOrder());
                this.enabled = false;
            }

            if (!IsGround())
                Debug.LogWarning("몬스터가 공중에 뜬 상태로 시작되었습니다. " + thisOrder());

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

        //코루틴 활성화 및 초반 움직임 세팅
        //todo: 코루틴은 활성화 시 다시 켜주어야 한다.
        private void StartMoveState()
        {
            // 움직이지 않는 몬스터거나, 움직임 전환 속도가 0초일때
            if (speed != 0)
            {
                if (maxMoveTime > 0f && maxIdleTime > 0f)
                    StartCoroutine(SwitchIdleMoveTimer_co());
                else
                    Debug.LogWarning("몬스터의 움직이, 휴식, 플립 쿨타임 0초 입니다. " + thisOrder());
            }
        }



        private void FixedUpdate()
        {
            //만약 시작 위치보다 떨어져 있다면, 다시 위치를 초기화 한다.
            if ((startPos.y - 1) >= transform.position.y)
                transform.position = startPos;

            if (moveAble)
                HorizontalMovement();
            else
                rigid.velocity = Vector2.up * rigid.velocity;

            //지형을 계속해서 확인하고, 이상을 감지하면 플립실행
            if (speed != 0)
                if (GroundSense())
                    if (rigid.velocity.x > 0)
                        Turn(true);
                    else
                        Turn(false);
        }

        //act: 가만히 있음
        private void None()
        {

        }

        //act: 좌우 움직임
        protected void HorizontalMovement()
        {
            if (!flipX)
                rigid.velocity = new Vector2(speed, rigid.velocity.y);
            else
                rigid.velocity = new Vector2(-speed, rigid.velocity.y);
        }

        //act: 주기적으로 대쉬를 하는 움직임
        private void DashMovement()
        {

        }



        //@ 움직임 패턴 코루틴
        //@=================================================================================================================================================

        //자동으로 이동, 휴식을 변경합니다.
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



        //@ 몬스터 플립
        //@=================================================================================================================================================

        //act: 몬스터가 돌아야 할 때를 알려준다.
        //tip: 근처에 낭떠러지거나, 벽이면 ture를 리턴.
        private bool GroundSense()
        {
            //다음 위치가 어디일지 미리 검색한다.
            Vector2 nextPos = new Vector2(movetip.position.x + nextStap, movetip.position.y);

            //낭떠러지가 있는 지 확인.
            Debug.DrawRay(nextPos, Vector3.down * (movetip.position.y - startPos.y + .3f), Color.green);
            if (!Physics2D.Raycast(nextPos, Vector3.down, movetip.position.y - startPos.y + .3f, LayerMask.GetMask("Ground")))
            {
                return true;
            }

            //벽이 있는 지 확인.
            Debug.DrawLine(movetip.position, nextPos, Color.red);
            if (Physics2D.Linecast(movetip.position, nextPos, LayerMask.GetMask("Ground")))
            {
                return true;
            }

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
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                nextStap = -.2f * speed;
                transform.localScale = new Vector3(1, 1, -1);
            }
        }
        //act: 랜덤하게 돌린다.
        //tip: 맵에서 자연스럽게 돌아다니게 하기위해
        private void RandomTurn()
        {
            bool flip = Random.Range(0, 2) == 1 ? true : false;
            Turn(flip);
        }

        //@ 기타
        //@=================================================================================================================================================

        protected bool IsGround() => Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Ground"));
        private int thisOrder() => transform.GetSiblingIndex();
    }
}

