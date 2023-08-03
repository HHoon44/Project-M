using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    public class MobProjectileBase : Projectile
    {
        public float playerKnockbackAmount { get; private set; } = 1;

        //생성시 호출
        public void Initialize(Vector2 _startPos, float _angle, float _scale = 1)
        {
            this.transform.position = _startPos;
            this.transform.rotation = Quaternion.Euler(0, 0, _angle);
            this.transform.localScale = new Vector3(Mathf.Abs(_scale), _scale, Mathf.Abs(_scale));

        }

        protected virtual void Start()
        {
            GetComponent<Rigidbody2D>().velocity = transform.right.normalized * speed;
            GetComponent<SpriteRenderer>().sortingLayerName = "FrontEffect";
            gameObject.tag = "MobProjectile";
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            //* 추후 자식에서 다룸
            // if (other.gameObject.tag == "Player")
            // {
            //     other.gameObject.GetComponent<PlayerController>().TakeDamage();
            // }

            if (other.gameObject == PlayerBase.GetPlayerObject())
            {
                PlayerBase player = PlayerBase.GetPlayerObject().GetComponent<PlayerBase>();
                if (player.invincibleTime <= 0)//무적시간이 아닐 때
                {
                    player.TakeDamage(hasDamage, playerKnockbackAmount);
                    
                    if (destroyAtOrganism)
                        Destroy(this.gameObject);
                }
            }

            if (destroyAtGround)
                if (other.gameObject.tag == "Tilemap")
                    Destroy(this.gameObject);
        }
    }
}
