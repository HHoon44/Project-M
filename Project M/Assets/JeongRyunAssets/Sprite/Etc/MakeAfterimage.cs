using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Util;

namespace ProjectM.InGame
{
    public class MakeAfterimage : Singleton<MakeAfterimage>
    {
        [SerializeField] private GameObject normalAfterimage;

        //잔상을 생성합니다.
        public AfterimageController MakeNornal(GameObject _targetObj, SpriteRenderer _renderer)
        {
            AfterimageController afterimage = Instantiate(normalAfterimage, this.transform).GetComponent<AfterimageController>();
            afterimage.InitializeThis(_targetObj, _renderer);

            return afterimage;
        }
    }
}
