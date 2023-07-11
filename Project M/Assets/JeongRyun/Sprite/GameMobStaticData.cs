using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Util;

namespace InGame.Mob
{
    /// <summary>
    ///@ �ش� ������ ������ �����Ҷ� ����
    /// </summary>
    public enum KindOfMob
    {
        ReferenceMob,
        Tmp_1,
        Tmp_2
    }

    /// <summary>
    ///@ ���ݰ� ����� 
    ///!�ӽ�
    /// </summary>
    public enum DebuffType
    {
        None,
        Slow
    }

    public struct MobInfo
    {
        public MobInfo(int _maxHP, int _attack, int _speed)
        {
            maxHP = _maxHP;
            attack = _attack;
            speed = _speed;
        }

        readonly public int maxHP;
        readonly public int attack;
        readonly public int speed;
    }

    public class GameMobStaticData : Singleton<GameMobStaticData>
    {
        private Dictionary<KindOfMob, MobInfo> mobReferenceInfo_Table = new Dictionary<KindOfMob, MobInfo>();

        protected override void Awake()
        {

        }
    }
}
