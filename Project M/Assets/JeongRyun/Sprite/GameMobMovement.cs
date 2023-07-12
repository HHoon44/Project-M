using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    public class GameMobMovement : MonoBehaviour
    {
        private KindOfMob mobType;
        private MobMovememt movememtState = MobMovememt.None;

        private Vector3 startPos;
        private float speed;

        private float nextStap;
        private bool flipX = false;

        private void Start()
        {
            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("���� �� ���� ������Ʈ�� �ٸ� ��ü�� ������ �ֽ��ϴ�.");
                this.enabled = false;
            }

            mobType = GetComponent<GameMob>().thisMobType;

            startPos = transform.position;
            flipX = Random.Range(0, 2) == 1 ? true : false; //���� �� �յڸ� �����ϰ� �����.

            speed = GameMobStaticData.Instance.GetMobReferenceInfo(mobType).speed;
            movememtState = GameMobStaticData.Instance.GetMobReferenceInfo(mobType).movement;
        }

        private void FixedUpdate()
        {
            //�ε��ǰ� ���� �ð����� �������� ����
            // if (startingTime + 0.01f >= Time.time)
            //     return;

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

            if (movememtState != MobMovememt.None)
                if (GroundSense())
                    Turn();
        }

        //act: ������ ����
        private void None()
        {

        }

        //act: �¿�� ������
        private void HorizontalMovement()
        {
        }

        //act: ������ �ϸ鼭 ������
        private void JumpMovement()
        {
            // if (Physics2D.Linecast(BottomRayTip.position, BottomRayTip.position, 1 << LayerMask.NameToLayer("Ground"))) //���� �ν��Ѵ�.
            // {
            //     Debug.Log("a");
        }

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

        private void Turn()
        {
            if (!flipX)
            {

                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, -1);
            }

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

