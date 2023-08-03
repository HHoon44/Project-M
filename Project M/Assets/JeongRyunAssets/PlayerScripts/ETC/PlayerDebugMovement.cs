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
        }

        void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Slash))
            {
                rigid.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * atDebugSpeed;
                rigid.isKinematic = true;
            }
            else
            {
                rigid.isKinematic = false;
            }
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
