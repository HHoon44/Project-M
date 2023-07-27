using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{

    public class PlayerRenderer : MonoBehaviour
    {
        private SpriteRenderer render;
        private float remainingTime = 0;

        private bool isFlicker = false;

        private void Start()
        {
            render = GetComponent<SpriteRenderer>();
        }

        public void FlickerPlayer(float _invincibleTime = 2)
        {
            remainingTime = _invincibleTime;

            if (isFlicker == false)
                StartCoroutine(Flicker_co());
        }

        private IEnumerator Flicker_co()
        {
            isFlicker = true;

            Color32 startColor = render.color;
            Color32 alphaColor = startColor;
            alphaColor.a /= 5;

            while (remainingTime >= .2f)
            {
                render.color = alphaColor;
                yield return new WaitForSeconds(0.2f);
                render.color = startColor;
                yield return new WaitForSeconds(0.2f);

                remainingTime -= 0.4f;
            }
            isFlicker = false;
        }
    }
}
