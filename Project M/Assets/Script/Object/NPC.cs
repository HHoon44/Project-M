using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.Object
{
    /// <summary>
    /// NPC�� ���̽� Ŭ����
    /// </summary>
    public class NPC : MonoBehaviour
    {
        private Collider2D coll;        // ��ȣ�ۿ� ����

        /// <summary>
        /// NPC �ʱ�ȭ �޼���
        /// </summary>
        public void Initialize()
        {
            coll ??= transform.Find("Area").GetComponent<Collider2D>();
        }

        /// <summary>
        /// NPC ������Ʈ �޼���
        /// </summary>
        public void NpcUpdate()
        {
            CheckInteraction();
        }

        /// <summary>
        /// Npc�� �÷��̾��� ��ȣ�ۿ� �۾��� �����ϴ� �޼���
        /// </summary>
        private void CheckInteraction()
        {
            // Npc �ֺ��� �÷��̾ �����ϴ��� Ȯ���ϴ� �۾�
            var colls = Physics.OverlapBox
                 (coll.bounds.center, coll.bounds.extents, transform.rotation, 1 << LayerMask.NameToLayer("Player"));


        }
    }
}