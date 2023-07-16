using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Util;



namespace ProjectM.InGame
{
    //tip: �뽬 ����� ���� ������Ʈ �Դϴ�
    public class MobDash : MonoBehaviour, IMobConsistModule
    {
        public MobBase mob { get; set; }

        private GameObject mobForm;
        private Sprite nowSprite;

        [SerializeField] protected float dashForce;   //�뽬 �Ÿ�
        [SerializeField] protected float dashCooltime;   //0���̸�, ����뽬�� ���� �ʽ��ϴ�.
        [SerializeField] protected float dashReadyTime;  //�뽬�� �ϱ��� ��� �����ִ� �ð�

        private GameObject[] afterimageObj;
        private Vector2[] afterimageStartPos;

        public bool isDash{get; private set; }


        public MobDash(GameObject _obj)
        {
            
        }

        void Start()
        {
            mobForm = mob.myForm;
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

        public void Initialize(MobBase _mob)
        {
            mob = _mob;
            gameObject.tag = "Mob";
            gameObject.name = "DashModule";


        }

        public void SetActiveModule(bool _act)
        {
        }
    }
}
