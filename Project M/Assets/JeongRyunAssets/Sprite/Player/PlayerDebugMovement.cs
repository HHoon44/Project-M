using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    // active at debug mode
    public class PlayerDebugMovement : MonoBehaviour
    {
        private Rigidbody2D rigid;
        public float atDebugSpeed;

        void Start()
        {
            if (!Application.isEditor)
                enabled = false;

            rigid = GetComponent<Rigidbody2D>();
            rigid.isKinematic = true;
        }

        void FixedUpdatete()
        {
            rigid.velocity = new Vector2(transform.position.x + Input.GetAxisRaw("Horizontal"), transform.position.y + Input.GetAxisRaw("Vertical"))* atDebugSpeed;
        }

        // public void DontControll(float _time = 0)
        // {
        //     controllAble = false;
        //     if (_time == 0)
        //         return;
        //     StartCoroutine(RecoverControll_co(_time));
        // }

        // private IEnumerator RecoverControll_co(float _time)
        // {
        //     yield return new WaitForSeconds(_time);
        //     controllAble = true;
        // }
    }
}
