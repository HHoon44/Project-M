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
        public MobInfo(float _maxHP, float _atkCool, float _detectArea)
        {
            maxHP = _maxHP;
            atkCool = _atkCool;
            detectArea = _detectArea;
            specialMob = false;
            staticMob = false;
        }
        //특수적인 몬스터 초기화
        public MobInfo(float _maxHP, float _atkCool, float _detectArea, bool _special, bool _static)
        {
            maxHP = _maxHP;
            atkCool = _atkCool;
            detectArea = _detectArea;
            specialMob = _special;
            staticMob = _static;
        }

        readonly public float maxHP;
        readonly public float atkCool;
        readonly public float detectArea;
        readonly public bool specialMob;   //이 몬스터가 보스 등 특별한 몬스터라면 true
        readonly public bool staticMob;  //공격을 받지 않는 장애물 같은 몬스터라면 true ex) 마리오의 파이프 꽃 같은 경우
    }


    public class MobsStaticData : Singleton<MobsStaticData>
    {
        private Dictionary<KindOfMob, MobInfo> mobReferenceInfo_Table = new Dictionary<KindOfMob, MobInfo>();

        protected override void Awake()
        {
            base.Awake();

            //자신의 객체와 싱글톤 된 객체가 다르다면 부하를 최소화 하기위해 
            if (this != MobsStaticData.Instance)
                return;

            mobReferenceInfo_Table.Clear();
            mobReferenceInfo_Table.Add(KindOfMob.ReferenceMob, new MobInfo(100, 2, 10));
            mobReferenceInfo_Table.Add(KindOfMob.Tmp_1, new MobInfo(30, 3, 10));
            mobReferenceInfo_Table.Add(KindOfMob.Tmp_2, new MobInfo(30, 3, 10));
        }

        public MobInfo GetMobReferenceInfo(KindOfMob _mobType)
        {
            return mobReferenceInfo_Table[_mobType];
        }
    }
}
