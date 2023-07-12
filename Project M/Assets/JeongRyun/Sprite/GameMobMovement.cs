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
        public float jumpTiming; //최저를 1초로 해서 땅에 떨어지지 않았는데 다시 점프를 금지한다.


        Rigidbody2D rigid;

        private KindOfMob mobType;
        private MobMovememt movememtState = MobMovememt.None;

        //초기화 상수
        private Vector3 startPos;
        private float speed;

        //오브젝트 변수
        private float nextStap;
        private bool flipX = false;
        private bool moveAble;

        private void Start()
        {
            rigid = GetComponent<Rigidbody2D>();

            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("현재 몹 전용 컴포넌트를 다른 객체가 가지고 있습니다.");
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

            //지형을 감지하면 플립한다.
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

        //act: 가만히 있음
        private void None()
        {

        }

        //act: 좌우로 움직임
        private void HorizontalMovement()
        {
            if (!flipX)
                rigid.velocity = new Vector2(speed, rigid.velocity.y);
            else
                rigid.velocity = new Vector2(-speed, rigid.velocity.y);
        }

        //act: 점프를 하면서 움직임
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

        //act: 랜덤 시간마다 대쉬를 한다.
        private void DashMovement()
        {

        }

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

        private void Turn(bool _flip)
        {
            SetNextStap();

            //플립정보 반전
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
        ///act: 몬스터가 계속해서 이동한다면, 다음 자리는 어디 일지 미리 계산한다.
        ///tip: 몬스터가 오른쪽을 보고 있을 때 기준
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
                    //todo: 대쉬 거리 계산
                    break;
                default:
                    Debug.Log("알 수 없는 움직임");
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

