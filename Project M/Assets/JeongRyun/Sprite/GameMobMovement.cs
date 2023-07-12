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
        //todo: 최대최소 만들기
        public float moveCooltime;
        [Space(10f)]
        [Range(0, 10)]
        public float idleCooltime;

        [Space(10f)]
        [Header("JumpMove")]
        public float jumpFarce;
        [Range(0, 10)]
        public float jumpCooltime; //최저를 1초로 해서 땅에 떨어지지 않았는데 다시 점프를 금지한다.

        readonly static float Deviation = .5f;   //움직임 쿨 오차

        Rigidbody2D rigid;

        private KindOfMob mobType;
        private MobMovememt movememtState = MobMovememt.None;

        //초기화 상수
        private Vector3 startPos;

        //오브젝트 변수
        private float nextStap;
        private bool flipX = false;
        private bool moveAble;

        private void Start()
        {
            rigid = GetComponent<Rigidbody2D>();

            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("현재 몹 전용 컴포넌트를 다른 객체가 가지고 있습니다. " + transform.GetSiblingIndex());
                this.enabled = false;
            }

            mobType = GetComponent<GameMob>().thisMobType;
            //몬스터 정보에서 움직임 종류를 가져온다.
            movememtState = GameMobStaticData.Instance.GetMobReferenceInfo(mobType).movement;

            if (!IsGround())
                Debug.LogWarning("몬스터가 공중에 뜬 상태로 시작되었습니다. " + transform.GetSiblingIndex());

            startPos = transform.position;

            StartMoveState();
        }

        //코루틴 활성화 및 초반 움직임 세팅
        //todo: 코루틴은 활성화 시 다시 켜주어야 한다.
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

            // 움직이지 않는 몬스터거나, 움직임 전환 속도가 0초일때
            if (MobMovememt.None != movememtState)
            {
                if (moveCooltime > 0f && idleCooltime > 0f)
                {
                    StartCoroutine(SwitchIdleMoveTimer_co());
                }
                else
                    Debug.LogWarning("몬스터의 움직이, 휴식, 플립 쿨타임 0초 입니다. " + transform.GetSiblingIndex());
            }
        }



        private void FixedUpdate()
        {
            //만약 시작 위치보다 떨어져 있다면, 다시 위치를 초기화 한다.
            if ((startPos.y - 1) >= transform.position.y)
                transform.position = startPos;

            if (moveAble)
            {
                //몬스터 마다 고유의 움직임
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

            //지형을 계속해서 확인하고, 이상을 감지하면 플립실행
            if (movememtState != MobMovememt.None)
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
        private void HorizontalMovement()
        {
            if (!flipX)
                rigid.velocity = new Vector2(speed, rigid.velocity.y);
            else
                rigid.velocity = new Vector2(-speed, rigid.velocity.y);
        }

        //act: 좌우와 점프 움직임
        private void JumpMovement()
        {
            HorizontalMovement();
        }
        private void Jump() => rigid.AddForce(Vector2.up * jumpFarce, ForceMode2D.Impulse);


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
                yield return new WaitForSeconds(Random.Range(moveCooltime / 2f, moveCooltime * (3 / 4f)));
                moveAble = false;
                yield return new WaitForSeconds(Random.Range(idleCooltime / 2f, idleCooltime * (3 / 4f)));
            }
        }
        //자동으로 시간에 맞추어 점프를 합니다.
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


        //@ 몬스터 플립
        //@=================================================================================================================================================

        //act: 몬스터가 돌아야 할 때를 알려준다.
        //tip: 근처에 낭떠러지거나, 벽이면 ture를 리턴.
        private bool GroundSense()
        {
            //다음 위치가 어디일지 미리 검색한다.
            Vector2 nextPos = new Vector2(transform.position.x + nextStap, transform.position.y);

            //낭떠러지가 있는 지 확인.
            Debug.DrawRay(nextPos, Vector3.down * (transform.position.y - startPos.y + 1), Color.green);
            if (!Physics2D.Raycast(nextPos, Vector3.down, transform.position.y - startPos.y + 1, LayerMask.GetMask("Ground")))
            {
                return true;
            }

            //벽이 있는 지 확인.
            Debug.DrawLine(transform.position, nextPos, Color.red);
            if (Physics2D.Linecast(transform.position, nextPos, LayerMask.GetMask("Ground")))
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
                nextStap = Mathf.Abs(detectArea);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                nextStap = -Mathf.Abs(detectArea);
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

        private bool IsGround() => Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Ground"));
    }
}

