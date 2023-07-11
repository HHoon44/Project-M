using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Util;

namespace InGame.Mob
{
    /// <summary>
    ///@ 해당 몬스터의 종류를 구분할때 쓰임
    /// </summary>
    public enum KindOfMob
    {
        ReferenceMob,
        Tmp_1,
        Tmp_2
    }

    /// <summary>
    ///@ 공격과 디버프 
    ///!임시
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
