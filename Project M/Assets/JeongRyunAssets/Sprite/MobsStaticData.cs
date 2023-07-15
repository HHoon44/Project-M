using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Util;
using ProjectM.Define;

namespace ProjectM.InGame
{
    //@ 몬스터 종류 마다 고유의 능력치를 저장하는 자료형
    public struct MobInfo
    {
        //대중적인 몬스터 초기화
        public MobInfo(float _maxHP, float _speed, float _atkCool, float _detectArea, float _jumpForce, float _dashForce)
        {
            maxHP = _maxHP;
            speed = _speed;
            atkCool = _atkCool;
            detectArea = _detectArea;

            jumpForce = _jumpForce;
            dashForce = _dashForce;

            specialMob = false;
            staticMob = false;
        }
        //특수적인 몬스터 초기화
        public MobInfo(float _maxHP, float _speed, float _atkCool, float _detectArea, float _jumpForce, float _dashForce, bool _special, bool _static)
        {
            maxHP = _maxHP;
            speed = _speed;
            atkCool = _atkCool;
            detectArea = _detectArea;

            jumpForce = _jumpForce;
            dashForce = _dashForce;

            specialMob = _special;
            staticMob = _static;
        }

        readonly public float maxHP;
        readonly public float speed;
        readonly public float atkCool;
        readonly public float detectArea; //플레이어 감지거리

        readonly public float jumpForce;
        readonly public float dashForce;

        readonly public bool specialMob;   //이 몬스터가 보스 등 특별한 몬스터라면 true
        readonly public bool staticMob;  //공격을 받지 않는 장애물 같은 몬스터라면 true ex) 마리오의 파이프 꽃 같은 경우
    }


    public class MobsStaticData : Singleton<MobsStaticData>
    {
        private Dictionary<KindOfMob, MobInfo> mobReferenceInfo_Table = new Dictionary<KindOfMob, MobInfo>();

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

            mobReferenceInfo_Table.Clear();
            mobReferenceInfo_Table.Add(KindOfMob.HorizontalMob, new MobInfo(100, 3, 2, 8, 0, 0));
            mobReferenceInfo_Table.Add(KindOfMob.JumpMob, new MobInfo(100, 3, 2, 10, 7, 0));
            mobReferenceInfo_Table.Add(KindOfMob.DashMob, new MobInfo(100, 2, 3, 5, 0, 3));
            mobReferenceInfo_Table.Add(KindOfMob.JumpDashMob, new MobInfo(100, 4, 3, 10, 5, 5));
            mobReferenceInfo_Table.Add(KindOfMob.NoneMovementMob, new MobInfo(100, 0, 0, 0, 0, 0));

            mobMovementModule = Resources.Load("Prefabs/MobModule/MobMovementModule") as GameObject;
            mobJumpModule = Resources.Load("Prefabs/MobModule/MobJumpModule") as GameObject;
            mobDashModule = Resources.Load("Prefabs/MobModule/MobDashModule") as GameObject;
            mobAttackModule = Resources.Load("Prefabs/MobModule/MobAttackModule") as GameObject;
            
            //발사체의 주소와 몬스터의 타입을 같게하여 가지고 온다.
        }

        public MobInfo GetMobReferenceInfo(KindOfMob _mobType)
        {
            return mobReferenceInfo_Table[_mobType];
        }
    }
}
