using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{

    public class PlayerBaseMovement : MonoBehaviour
    {
        public float atDebugSpeed; //@ 디버그 용

        private bool controllAble = true;

        void Update()
        {
            transform.position = new Vector2(transform.position.x + Input.GetAxisRaw("Horizontal") * atDebugSpeed * Time.deltaTime, transform.position.y + Input.GetAxisRaw("Vertical") * atDebugSpeed * Time.deltaTime);
        }

        public void DontControll(float _time = 0)
        {
            controllAble = false;
            if (_time == 0)
                return;
            StartCoroutine(RecoverControll_co(_time));
        }

        private IEnumerator RecoverControll_co(float _time){
            yield return new WaitForSeconds(_time);
            controllAble = true;
        }
    }
}
