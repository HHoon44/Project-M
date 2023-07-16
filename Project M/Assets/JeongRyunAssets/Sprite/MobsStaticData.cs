using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Util;
using ProjectM.Define;

namespace ProjectM.InGame
{
    //@ ���� ���� ���� ������ �ɷ�ġ�� �����ϴ� Ŭ����
    public class MobReferenceData
    {
        //�������� ���� �ʱ�ȭ
        public MobReferenceData(
            float _maxHP, float _atkCool, float _detectArea)
        {
            maxHP = _maxHP;
            atkCool = _atkCool;
            detectArea = _detectArea;

            specialMob = false;
            staticMob = false;
        }
        //Ư������ ���� �ʱ�ȭ
        public MobReferenceData(
            float _maxHP, float _atkCool, float _detectArea, bool _special, bool _static)
        {
            maxHP = _maxHP;
            atkCool = _atkCool;
            detectArea = _detectArea;

            specialMob = _special;
            staticMob = _static;
        }

        readonly public float maxHP;
        readonly public float atkCool;
        readonly public float detectArea; //�÷��̾� �����Ÿ�

        readonly public bool specialMob;   //�� ���Ͱ� ���� �� Ư���� ���Ͷ�� true
        readonly public bool staticMob;  //������ ���� �ʴ� ��ֹ� ���� ���Ͷ�� true ex) �������� ������ �� ���� ���
    }

    public class MobMovemantData
    {
        public MobMovemantData(
            float _speed, float _minMoveTime, float _maxMoveTime, float _minIdleTime, float _maxIdleTime, bool _atDiscoverStop,
            float _jumpForce, float _minJumpCooltime, float _maxJumpCooltime,
            float _dashForce, float _minDashCooltime, float _maxDashCooltime, float _dashReadyTime)
        {
            speed = _speed;
            minMoveTime = _minMoveTime;
            maxMoveTime = _maxMoveTime;
            minIdleTime = _minIdleTime;
            maxIdleTime = _maxIdleTime;
            atDiscoverPlayerStop = _atDiscoverStop;

            jumpForce = _jumpForce;
            minJumpCooltime = _minJumpCooltime;
            maxJumpCooltime = _maxJumpCooltime;

            dashForce = _dashForce;
            minDashCooltime = _minDashCooltime;
            maxDashCooltime = _maxDashCooltime;
            dashReadyTime = _dashReadyTime;
        }

        readonly public float speed;
        readonly public float minMoveTime;
        readonly public float maxMoveTime;
        readonly public float minIdleTime;
        readonly public float maxIdleTime;
        readonly public bool atDiscoverPlayerStop;

        readonly public float jumpForce;
        readonly public float minJumpCooltime;
        readonly public float maxJumpCooltime;

        readonly public float dashForce;
        readonly public float minDashCooltime;
        readonly public float maxDashCooltime;
        readonly public float dashReadyTime;
    }


    public class MobsStaticData : Singleton<MobsStaticData>
    {
        private Dictionary<MobType, MobReferenceData> mobReferenceData_Table = new Dictionary<MobType, MobReferenceData>();
        private Dictionary<MobType, MobMovemantData> mobMovementData_Table = new Dictionary<MobType, MobMovemantData>();

        public static GameObject mobMovementModule { get; private set; }
        public static GameObject mobJumpModule { get; private set; }
        public static GameObject mobDashModule { get; private set; }
        public static GameObject mobAttackModule { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            //�ڽ��� ��ü�� �̱��� �� ��ü�� �ٸ��ٸ� ���ϸ� �ּ�ȭ �ϱ����� 
            if (this != MobsStaticData.Instance)
                return;

            MakeUp_MobReferenceData();
            MakeUp_MobMovementData();
            SetModule();
        }

        private void MakeUp_MobReferenceData()
        {
            mobReferenceData_Table.Clear();
            mobReferenceData_Table.Add(MobType.HorizontalMob, new MobReferenceData(100, 2, 8));
            mobReferenceData_Table.Add(MobType.JumpMob, new MobReferenceData(100, 2, 10));
            mobReferenceData_Table.Add(MobType.DashMob, new MobReferenceData(100, 3, 5));
            mobReferenceData_Table.Add(MobType.JumpDashMob, new MobReferenceData(100, 3, 10));
            mobReferenceData_Table.Add(MobType.NoneMovementMob, new MobReferenceData(100, 0, 0));
        }

        private void MakeUp_MobMovementData()
        {
            mobMovementData_Table.Clear();
            mobMovementData_Table.Add(MobType.HorizontalMob, new MobMovemantData(4, 3, 6, 1, 3, true, 0, 0, 0, 0, 0, 0, 0));
            mobMovementData_Table.Add(MobType.JumpMob, new MobMovemantData(4, 3, 6, 1, 3, true, 6, 4, 6, 0, 0, 0, 0));
            mobMovementData_Table.Add(MobType.DashMob, new MobMovemantData(4, 3, 6, 1, 3, true, 0, 0, 0, 5, 4, 6, 1));
            mobMovementData_Table.Add(MobType.JumpDashMob, new MobMovemantData(4, 3, 6, 1, 3, true, 0, 0, 0, 0, 0, 0, 0));
            mobMovementData_Table.Add(MobType.NoneMovementMob, new MobMovemantData(0, 3, 6, 1, 3, false, 0, 0, 0, 0, 0, 0, 0));
        }

        private void SetModule()
        {
            mobMovementModule = Resources.Load("Prefabs/MobModule/MobMovementModule") as GameObject;
            mobJumpModule = Resources.Load("Prefabs/MobModule/MobJumpModule") as GameObject;
            mobDashModule = Resources.Load("Prefabs/MobModule/MobDashModule") as GameObject;
            mobAttackModule = Resources.Load("Prefabs/MobModule/MobAttackModule") as GameObject;
        }

        public MobReferenceData GetMobReferenceInfo(MobType _mobType)
        {
            return mobReferenceData_Table[_mobType];
        }
        public MobMovemantData GetMobMovemantData(MobType _mobType)
        {
            return mobMovementData_Table[_mobType];
        }
    }
}
