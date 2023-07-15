using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Util;



namespace ProjectM.InGame
{
    //tip: �뽬 ����� ���� ������Ʈ �Դϴ�
    public class MobDashMovement : MonoBehaviour, IMobConsistModule
    {
        public MobBase mob { get; set; }

        private GameObject mobForm;
        private Sprite nowSprite;

        [SerializeField] protected float dashCooltime;
        [SerializeField] protected float dashReadyTime;   //�뽬�� �ϱ��� ��� �����ִ� �ð�
        [SerializeField] protected float dashForce;   //�뽬 �Ÿ�

        private GameObject[] afterimageObj;
        private Vector2[] afterimageStartPos;

        public bool isDash{get; private set; }


        public MobDashMovement(GameObject _obj)
        {
            
        }

        void Start()
        {
            mobForm = GetComponent<MobBase>().myForm;
        }

        void Update()
        {
            if(isDash)
            {
                for (int i = 0; i < afterimageObj.Length; i++)
                {
                    afterimageObj[i].transform.position = afterimageStartPos[i];
                }
            }

        }

        private void DashStart()
        {
            if(isDash == true)
                return;

            isDash = true;
            nowSprite = mobForm.GetComponent<SpriteRenderer>().sprite;
            
        }
        private void DashEnd()
        {
            isDash = false;

        }

        public void StartForMob(MobBase _mob)
        {
            throw new System.NotImplementedException();
        }

        public void SetActineModule(bool _act)
        {
            throw new System.NotImplementedException();
        }
    }
}
