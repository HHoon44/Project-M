using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{

    public class GamePlayer : MonoBehaviour
    {
        [SerializeField] private Transform mobDetectionTip;
        public Transform GetPlayerTip() => mobDetectionTip;


        
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
