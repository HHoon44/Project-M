using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Util;
using ProjectM.Define;

namespace ProjectM.InGame
{
    //@ 몬스터 종류 마다 고유의 능력치를 저장하는 클래스
    public class MobReferenceData
    {
        //대중적인 몬스터 초기화
        public MobReferenceData(
            float _maxHP, float _atkCool, float _detectArea)
        {
            maxHP = _maxHP;
            atkCool = _atkCool;
            detectArea = _detectArea;

            specialMob = false;
            staticMob = false;
        }
        //특수적인 몬스터 초기화
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
        readonly public float detectArea; //플레이어 감지거리

        readonly public bool specialMob;   //이 몬스터가 보스 등 특별한 몬스터라면 true
        readonly public bool staticMob;  //공격을 받지 않는 장애물 같은 몬스터라면 true ex) 마리오의 파이프 꽃 같은 경우
    }

    public class MobMovementData
    {
        public MobMovementData(
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
        private Dictionary<MobType, MobMovementData> mobMovementData_Table = new Dictionary<MobType, MobMovementData>();

        public static GameObject mobMovementModule { get; private set; }
        public static GameObject mobJumpModule { get; private set; }
        public static GameObject mobDashModule { get; private set; }
        public static GameObject mobAttackModule { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            //자신의 객체와 싱글톤 된 객체가 다르다면 부하를 최소화 하기위해 
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
            mobMovementData_Table.Add(MobType.HorizontalMob, new MobMovementData(4, 3, 6, 1, 3, true, 0, 0, 0, 0, 0, 0, 0));
            mobMovementData_Table.Add(MobType.JumpMob, new MobMovementData(4, 3, 6, 1, 3, true, 20, 4, 6, 0, 0, 0, 0));
            mobMovementData_Table.Add(MobType.DashMob, new MobMovementData(4, 3, 6, 1, 3, true, 0, 0, 0, 5, 4, 6, 1));
            mobMovementData_Table.Add(MobType.JumpDashMob, new MobMovementData(4, 3, 6, 1, 3, true, 0, 0, 0, 0, 0, 0, 0));
            mobMovementData_Table.Add(MobType.NoneMovementMob, new MobMovementData(0, 3, 6, 1, 3, false, 0, 0, 0, 0, 0, 0, 0));
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
        public MobMovementData GetMobMovemantData(MobType _mobType)
        {
            return mobMovementData_Table[_mobType];
        }
    }
}
