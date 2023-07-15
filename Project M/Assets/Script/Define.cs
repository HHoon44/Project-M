using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.Define
{
    public enum PoolType { }

    public enum SceneType { StartLoading, Loading }

    public class StaticData
    {
        public const string SDExcelPath = "Assets/StaticData/Excel";
        public const string SDJsonPath = "Assets/StaticData/Json";
    }

























    //! InGame에 관련된 Define ========================================================================================================================================

    //@ 몬스터의 종류를 나열
    //tip: 열거형의 정수는 스테이지와 관계있음   00## => 00 스테이지 번호, ## 몬스터 고유 번호
    public enum KindOfMob
    {
        ReferenceMob = 0100,
        Tmp_1,
        Tmp_2

    }

    //@ 데미지를 입을 때 같이 전해지는 디버프 종류
    public enum DebuffType
    {
        None,
        Slow
    }
}
