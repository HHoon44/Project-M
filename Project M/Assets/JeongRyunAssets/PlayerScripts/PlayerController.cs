using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    public class PlayerController : MonoBehaviour
    {
        private int startHP = 5;
        private int nowHP;
        private int maxHP = 10;

        private float maxCurse;
        private float nowCurse;

        private int deathCount = 0;

        private void Awake()
        {
            if (playerObject == null)
                playerObject = this.gameObject;
            else
                Debug.LogWarning("플레이어가 2마리 있습니다.");
        }

        private void RegenPlayer()
        {
            nowHP = maxHP;
            nowCurse = maxCurse;

            //if (deathCount >= 1)//todo: regen anim
        }

        //죽음(부활할 수 있음)
        private void Die()
        {
            deathCount++;
        }

        //완전히 죽어서 게임오버
        private void Dead()
        {

        }

        public void TakeDamage(int _dmgAmount = 1, float _knockbackAmunt = 0f)
        {
            nowHP -= _dmgAmount;
            Debug.Log("PlayerHP = " + nowHP);
        }

        public void TakeHeal(int _healAmount = 1, bool _removeDebuffs = false)
        {

        }



        //@ 기타 ================================================================================================================================================
        private static GameObject playerObject = null;
        public static GameObject GetPlayerObject() => playerObject;

        public Transform mobDetectionTip;
        public static Vector3 GetPlayerTip() => playerObject.GetComponent<PlayerController>().mobDetectionTip.transform.position;

    }
}
