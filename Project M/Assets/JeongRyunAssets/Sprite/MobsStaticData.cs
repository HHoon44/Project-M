using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Util;
using ProjectM.Define;

namespace ProjectM.InGame
{
    //@ ���� ���� ���� ������ �ɷ�ġ�� �����ϴ� �ڷ���
    public struct MobInfo
    {
        //�������� ���� �ʱ�ȭ
        public MobInfo(float _maxHP, float _atkCool, float _detectArea)
        {
            maxHP = _maxHP;
            atkCool = _atkCool;
            detectArea = _detectArea;
            specialMob = false;
            staticMob = false;
        }
        //Ư������ ���� �ʱ�ȭ
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
        readonly public bool specialMob;   //�� ���Ͱ� ���� �� Ư���� ���Ͷ�� true
        readonly public bool staticMob;  //������ ���� �ʴ� ��ֹ� ���� ���Ͷ�� true ex) �������� ������ �� ���� ���
    }


    public class MobsStaticData : Singleton<MobsStaticData>
    {
        private Dictionary<KindOfMob, MobInfo> mobReferenceInfo_Table = new Dictionary<KindOfMob, MobInfo>();

        protected override void Awake()
        {
            base.Awake();

            //�ڽ��� ��ü�� �̱��� �� ��ü�� �ٸ��ٸ� ���ϸ� �ּ�ȭ �ϱ����� 
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
