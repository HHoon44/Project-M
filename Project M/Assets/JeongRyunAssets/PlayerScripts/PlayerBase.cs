using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    //플레이어라면 당연히 가지고 있어야 하는 클래스
    public class PlayerBase : MonoBehaviour
    {
        [SerializeField] private PlayerRenderer pRenderer;

        //상태 변수
        private int startHP = 5;
        private int nowHP;
        private int maxHP = 10;

        private float maxCurse;
        private float nowCurse;

        private int deathCount = 0;
        private int lifeCount = 0;   //환생 횟수

        private float evasionPro = 0; //회피확룰 max100;

        //상황 변수
        public bool isLive { get; private set; }
        public float invincibleTime { get; private set; }

        private void Awake()
        {
            if (playerObject == null)
                playerObject = this.gameObject;
            else
                Debug.LogWarning("플레이어가 2마리 있습니다.");
        }

        private void Update()
        {
            if (invincibleTime > 0)
            {
                invincibleTime -= Time.deltaTime;
            }
            else
            {
                invincibleTime = 0;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
        }


        private void RegenPlayer()
        {
            nowHP = maxHP;
            nowCurse = maxCurse;
            isLive = true;
            //if (deathCount >= 1)//todo: regen anim
        }

        //죽음(부활할 수 있음)
        private void Die()
        {
            deathCount++;
        }

        //완전히 죽어서 완전한 게임오버
        private void Dead()
        {

        }

        public void TakeDamage(int _dmgAmount = 1, float _knockbackAmunt = 0f)
        {
            if (invincibleTime > 0)
                return;

            nowHP -= _dmgAmount;
            invincibleTime += 2f;

            pRenderer.FlickerPlayer(invincibleTime);
            Debug.Log("PlayerHP = " + nowHP);
        }

        public void TakeHeal(int _healAmount = 1, bool _removeDebuffs = false)
        {

        }



        //@ 기타 ================================================================================================================================================
        private static GameObject playerObject = null;
        public static GameObject GetPlayerObject() => playerObject;

        public Transform mobDetectionTip;
        public static Vector3 GetPlayerTip() => playerObject.GetComponent<PlayerBase>().mobDetectionTip.transform.position;

    }
}
