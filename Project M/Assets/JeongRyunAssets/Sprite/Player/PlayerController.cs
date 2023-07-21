using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{

    public class PlayerController : MonoBehaviour
    {
        private static GameObject playerObject = null;
        public static GameObject GetPlayerObject() => playerObject;

        public Transform mobDetectionTip;
        public static Vector2 GetPlayerTip() => playerObject.GetComponent<PlayerController>().mobDetectionTip.transform.position; //���Ͱ� �÷��̾ Ʈ��ŷ�� ��ġ�� �����մϴ�

        private void Awake()
        {
            if (playerObject == null)
                playerObject = this.gameObject;
            else
                Debug.LogWarning("플레이어가 2마리 있습니다.");
        } 

    }
}
