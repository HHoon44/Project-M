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
        //Ư������ ���� �ʱ�ȭ
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
        readonly public float detectArea; //�÷��̾� �����Ÿ�

        readonly public float jumpForce;
        readonly public float dashForce;

        readonly public bool specialMob;   //�� ���Ͱ� ���� �� Ư���� ���Ͷ�� true
        readonly public bool staticMob;  //������ ���� �ʴ� ��ֹ� ���� ���Ͷ�� true ex) �������� ������ �� ���� ���
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

            //�ڽ��� ��ü�� �̱��� �� ��ü�� �ٸ��ٸ� ���ϸ� �ּ�ȭ �ϱ����� 
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
            
            //�߻�ü�� �ּҿ� ������ Ÿ���� �����Ͽ� ������ �´�.
        }

        public MobInfo GetMobReferenceInfo(KindOfMob _mobType)
        {
            return mobReferenceInfo_Table[_mobType];
        }
    }
}
