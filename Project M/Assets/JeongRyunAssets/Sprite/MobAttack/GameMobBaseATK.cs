using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{
    //������ ���ϴ� ���͸� �����ϴ� �θ� �Դϴ�.
    public class GameMobBaseATK : MonoBehaviour
    {
        private GameMobBase mob;

        void Start()
        {
            mob = GetComponent<GameMobBase>();
        }

        void Update()
        {

        }
    }
}
