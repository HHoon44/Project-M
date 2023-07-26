using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Calculator;

namespace ProjectM.InGame
{
    public class MobProjectileBase : MonoBehaviour
    {
        protected bool destroyAtPlayer = true;
        protected bool destroyAtTilemap = true;

        protected bool alreadyDamaged = false;

        protected float speed = 5; //! 항상 노멀라이즈를 해주고, 속도를 곱해주어야 합니다.

        //생성시 호출
        public void Initialize(Vector2 _startPos, float _angle, float _scale = 1)
        {
            this.transform.position = _startPos;
            this.transform.rotation = Quaternion.Euler(0, 0,_angle);
            this.transform.localScale = Vector3.one * _scale;//new Vector3(_scale, Mathf.Abs(_scale), Mathf.Abs(_scale));

        }

        protected virtual void Start()
        {
                GetComponent<Rigidbody2D>().velocity = transform.right.normalized * speed;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            //* 추후 자식에서 다룸
            // if (other.gameObject.tag == "Player")
            // {
            //     other.gameObject.GetComponent<PlayerController>().TakeDamage();
            // }

            if (destroyAtPlayer)
                if (other.gameObject.tag == "Player")
                {
                    alreadyDamaged = true;
                    Destroy(this.gameObject);
                }

            if (destroyAtTilemap)
                if (other.gameObject.tag == "Map")
                    Destroy(this.gameObject);
        }
    }
}
