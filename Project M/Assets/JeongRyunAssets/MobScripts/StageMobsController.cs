using System.Collections;
using System.Collections.Generic;
using ProjectM.Util;
using UnityEngine;

namespace ProjectM.InGame
{
    //오브젝트 폴링을 이용하여 몬스터 활성화 구현
    public class StageMobsController : MonoBehaviour
    {
        [SerializeField] private GameObject[] inStageMob;  //스테이지 안의 몬스터를 모두 넣어준다.
       // ObjectPool<GameObject> a = new ObjectPool<GameObject>();
        private void Awake()
        {

        }


        void Start()
        {

        }

        void Update()
        {

        }
    }

}
