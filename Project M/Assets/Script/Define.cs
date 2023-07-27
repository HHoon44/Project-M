using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.Define
{
    public enum PoolType { }

    public enum SceneType { StartLoading, Loading, InGame }

    public enum IntroPhase { None, Start, ApllicationSetting, StaticData, Resource, UI, Complte }

    public class StaticData
    {
        public const string SDExcelPath = "Assets/StaticData/Excel";
        public const string SDJsonPath = "Assets/StaticData/Json";
    }

    public class Resource
    {
        public enum AtlasType
        {
            None
        }
    }























    //! InGame�� ���õ� Define ========================================================================================================================================

    //@ ������ ������ ����
    //tip: �������� ������ ���������� ��������   00## => 00 �������� ��ȣ, ## ���� ���� ��ȣ
    public enum MobType
    {
        HorizontalMob,
        JumpMob,
        DashMob,
        JumpDashMob,
        NoneMovementMob
    }

    //@ �������� ���� �� ���� �������� ����� ����
    public enum DebuffType
    {
        None,
        Slow
    }

    public class InGamePath
    {
    }

   // public static 
}
