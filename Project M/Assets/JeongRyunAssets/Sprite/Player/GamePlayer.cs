using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{

    public class GamePlayer : MonoBehaviour
    {
        //플래이어는 직접 싱글톤을 만들어 사용
        private static GameObject playerObject = null;
        public static GameObject GetPlayerObject() => playerObject;

        public Transform mobDetectionTip;
        public static Vector2 GetPlayerTip() => playerObject.GetComponent<GamePlayer>().mobDetectionTip.transform.position; //몬스터가 플레이어를 트래킹할 위치를 리턴합니다

        private void Awake()
        {
            if (playerObject == null)
                playerObject = this.gameObject;
            else
                Debug.LogWarning("스테이지내에 플레이어가 2개 이상 존재합니다.");
        } 

    }
}
