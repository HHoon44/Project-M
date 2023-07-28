using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    //! 발사체면 무조건 상속해야 합니다.
    public class Projectile : MonoBehaviour
    {
        protected float speed = 5; //! 항상 노멀라이즈를 해주고, 속도를 곱해주어야 합니다.

        protected bool destroyAtOrganism = true;
        protected bool destroyAtGround = true;

        protected bool alreadyDamaged = false;

        public int hasDamage{get; protected set; } = 1;
    }
}
