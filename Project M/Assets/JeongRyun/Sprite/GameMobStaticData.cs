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

    //@ ���� ���� ���� ������ �ɷ�ġ�� ������ �ִ�.
    public struct MobInfo
    {
        /// <summary>
        /// ���� �⺻���� �ʱ�ȭ 
        /// </summary>
        /// <param name="_maxHP">������ ���� HP</param>
        /// <param name="_attack">������ ���ݷ�</param>
        /// <param name="_speed">������ �ӵ�</param>
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

            //�ڽ��� ��ü�� �̱���� ��ü�� �ٸ��ٸ� ���ϸ� �ּ�ȭ �ϱ����� 
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
