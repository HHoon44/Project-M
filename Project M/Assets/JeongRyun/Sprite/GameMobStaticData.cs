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

    //@ 몬스터 종류 마다 고유의 능력치를 가지고 있다.
    public struct MobInfo
    {
        /// <summary>
        /// 몬스터 기본정보 초기화 
        /// </summary>
        /// <param name="_maxHP">몬스터의 최초 HP</param>
        /// <param name="_attack">몬스터의 공격력</param>
        /// <param name="_speed">몬스터의 속도</param>
        public MobInfo(int _maxHP, int _attack, int _speed)
        {
            maxHP = _maxHP;
            attack = _attack;
            speed = _speed;
        }

        private int maxHP;
        private int attack;
        private int speed;
    }

    public class GameMobStaticData : Singleton<GameMobStaticData>
    {
        private Dictionary<KindOfMob, MobInfo> mobReferenceInfo_Table = new Dictionary<KindOfMob, MobInfo>();

        protected override void Awake()
        {
            base.Awake();

            //자신의 객체와 싱글톤된 객체가 다르다면 부하를 최소화 하기위해 
            if (this != GameMobStaticData.Instance)
                return;

            mobReferenceInfo_Table.Clear();
            mobReferenceInfo_Table.Add(KindOfMob.ReferenceMob, new MobInfo(100, 20, 2));
            mobReferenceInfo_Table.Add(KindOfMob.Tmp_1, new MobInfo(30, 34, 3));
            mobReferenceInfo_Table.Add(KindOfMob.Tmp_2, new MobInfo(30, 34, 3));
        }

        public MobInfo GetMobReferenceInfo(KindOfMob _mobType)
        {
            return mobReferenceInfo_Table[_mobType];

        }
    }
}
