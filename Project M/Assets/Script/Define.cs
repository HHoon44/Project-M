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

























    //! InGame�� ���õ� Define ========================================================================================================================================

    //@ ������ ������ ����
    //tip: �������� ������ ���������� ��������   00## => 00 �������� ��ȣ, ## ���� ���� ��ȣ
    public enum KindOfMob
    {
        ReferenceMob = 0100,
        Tmp_1,
        Tmp_2

    }

    //@ �������� ���� �� ���� �������� ����� ����
    public enum DebuffType
    {
        None,
        Slow
    }
}
