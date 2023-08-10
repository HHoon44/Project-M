using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.Object
{
    /// <summary>
    /// NPC의 베이스 클래스
    /// </summary>
    public class NPC : MonoBehaviour
    {
        private Collider2D coll;        // 상호작용 범위

        /// <summary>
        /// NPC 초기화 메서드
        /// </summary>
        public void Initialize()
        {
            coll ??= transform.Find("Area").GetComponent<Collider2D>();
        }

        /// <summary>
        /// NPC 업데이트 메서드
        /// </summary>
        public void NpcUpdate()
        {
            CheckInteraction();
        }

        /// <summary>
        /// Npc와 플레이어의 상호작용 작업을 관리하는 메서드
        /// </summary>
        private void CheckInteraction()
        {
            // Npc 주변에 플레이어가 존재하는지 확인하는 작업
            var colls = Physics.OverlapBox
                 (coll.bounds.center, coll.bounds.extents, transform.rotation, 1 << LayerMask.NameToLayer("Player"));


        }
    }
}