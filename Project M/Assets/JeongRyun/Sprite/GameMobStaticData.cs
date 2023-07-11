using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Util;

namespace ProjectM.InGame
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


    public enum MobMovememt
    {
        None,
        HorizontalMovement,
        JumpMovement,
        DashMovement
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
        //�������� ���� �ʱ�ȭ
        public MobInfo(float _maxHP, float _speed, MobMovememt _movememt)
        {
            maxHP = _maxHP;
            speed = _speed;
            specialMob = false;
            staticMob = false;
            movement = _movememt;
        }
        //Ư������ ���� �ʱ�ȭ
        public MobInfo(float _maxHP, float _speed, bool _special, bool _static, MobMovememt _movememt)
        {
            maxHP = _maxHP;
            speed = _speed;
            specialMob = _special;
            staticMob = _static;
            movement = _movememt;
        }

        readonly public float maxHP;
        readonly public float speed;
        readonly public bool specialMob;   //�� ���Ͱ� ���� �� Ư���� ���Ͷ�� true
        readonly public bool staticMob;  //������ ���� �ʴ� ��ֹ� ���� ���Ͷ�� true ex) �������� ������ �� ���� ���
        readonly public MobMovememt movement;
    }

    
    public class GameMobStaticData : Singleton<GameMobStaticData>
    {
        private Dictionary<KindOfMob, MobInfo> mobReferenceInfo_Table = new Dictionary<KindOfMob, MobInfo>();

        protected override void Awake()
        {
            base.Awake();

            //�ڽ��� ��ü�� �̱��� �� ��ü�� �ٸ��ٸ� ���ϸ� �ּ�ȭ �ϱ����� 
            if (this != GameMobStaticData.Instance)
                return;

            mobReferenceInfo_Table.Clear();
            mobReferenceInfo_Table.Add(KindOfMob.ReferenceMob, new MobInfo(100, 2, MobMovememt.HorizontalMovement));
            mobReferenceInfo_Table.Add(KindOfMob.Tmp_1, new MobInfo(30, 3, MobMovememt.JumpMovement));
            mobReferenceInfo_Table.Add(KindOfMob.Tmp_2, new MobInfo(30, 3, MobMovememt.DashMovement));
        }

        public MobInfo GetMobReferenceInfo(KindOfMob _mobType)
        {
            return mobReferenceInfo_Table[_mobType];
        }
    }
}
