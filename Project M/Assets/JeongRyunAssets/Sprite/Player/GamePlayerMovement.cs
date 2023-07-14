using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{

    public class GamePlayerMovement : MonoBehaviour
    {
        public float atDebugSpeed;
        void Update()
        {
            transform.position = new Vector2(transform.position.x + Input.GetAxisRaw("Horizontal") * atDebugSpeed * Time.deltaTime, transform.position.y + Input.GetAxisRaw("Vertical") * atDebugSpeed * Time.deltaTime);
        }
    }
}
