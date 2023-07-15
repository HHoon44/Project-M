using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Define;

namespace ProjectM.InGame
{
    public class MobBase : MonoBehaviour
    {
        [Header("MobSetting")]
        public KindOfMob thisMobType = KindOfMob.NoneMovementMob;

        [Space(10f)]
        public GameObject myForm;   //�������� ���̴� ������ ��ü
        public Transform atkTip;  //������ �����ϴ� ��ġ

        [Space(10f)]
        [Header("MobEmotion")]
        public GameObject detectionMark;
        public GameObject discoveryMark;

        public Animator formAnim { get; private set; }
        private GameObject[] myModule = new GameObject[3];

        //���� ����
        public float nowHP { get; private set; }
        public bool detectionPlayer { get; private set; }  //�÷��̾ ��ó�� �ִٸ�
        public bool discoveryPlayer { get; private set; }  //�÷��̾ ���δٸ�
        public bool isLive { get; private set; }   //�ڽ��� �׾��ٸ�

        //���� ���
        public MobInfo myInfo { get; private set; }
        public Vector2 colSize { get; protected set; }


        private void Start()
        {
            myInfo = MobsStaticData.Instance.GetMobReferenceInfo(thisMobType);

            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("���� �� ���� ������Ʈ�� �ٸ� ��ü�� ������ �ֽ��ϴ�.");
                this.enabled = false;
            }

            formAnim = myForm.GetComponent<Animator>();
            colSize = myForm.GetComponent<CapsuleCollider2D>().size + myForm.GetComponent<CapsuleCollider2D>().offset;
            if (formAnim == null)
                Debug.LogWarning("�ɸ����� �ִϸ����� Ȥ�� �ݶ��̴��� �����ϴ�.");

            detectionMark.SetActive(false);
            discoveryMark.SetActive(false);

            SetModule();

            Regen();
            StartCoroutine(UpdateEmotion_co());
        }



        public void SetModule()
        {
            if (myInfo.jumpForce <= 0)
                myModule[0] = Instantiate(MobsStaticData.mobMovementModule, transform);
            else
                myModule[0] = Instantiate(MobsStaticData.mobJumpModule, transform);

            if (myInfo.dashForce >= 0)
                myModule[1] = Instantiate(MobsStaticData.mobDashModule, transform);

            if (myInfo.atkCool >= 0)
                myModule[2] = Instantiate(MobsStaticData.mobAttackModule, transform);

            foreach (GameObject item in myModule)
            {
                
                item.transform.position = Vector3.zero;
            }
        }

        private void FixedUpdate()
        {
            DetectUpdate();
        }




        //@ ���� ���̺� ===================================================================================================================

        //act: ���Ͱ� �ٽ� �¾ �� Ȱ��ȭ ����
        private void Regen()
        {
            isLive = true;
            nowHP = myInfo.maxHP;
            myForm.SetActive(true);
        }

        //act: �������� ���� �� ȣ��
        public void SufferDemage(float _Demaged, DebuffType[] _types)
        {
            //�������� ���̶�� ������ ���� ����
            if (myInfo.staticMob)
                return;

            if (0 >= nowHP - _Demaged)
            {
                nowHP = 0;
                MobDie();
                return;
            }
            else
            {
                //todo: ���� �޴� �ִϸ��̼�
                nowHP -= _Demaged;
            }
        }

        //act: ������ ����
        //todo: ���� ���� ��� ���
        private void MobDie()
        {
            Debug.Log("todo: ���� ����");

            isLive = false;

            //todo: ���� �״� �ִϸ��̼�
            Invoke(nameof(MobDead), 1f);
        }
        private void MobDead()
        {
            myForm.SetActive(false);
        }

        //@ �÷��̾� ���� ===================================================================================================================
        //act: �÷��̾ �������� �����Ѵ�.
        private void DetectUpdate()
        {
            if (myInfo.detectArea == 0)
                return;

            detectionPlayer = DetectPlayer();
            discoveryPlayer = DiscoverPlayer();

            if (detectionPlayer)
                Debug.DrawLine(atkTip.position, PlayerController.GetPlayerTip(), Color.red);
        }

        //act: ������ ������ ���� �� ���� �÷��̾ ���� ���θ� �� �� �ֵ��� ����ݴϴ�.
        private IEnumerator UpdateEmotion_co()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                if (discoveryPlayer)
                {
                    detectionMark.SetActive(false);
                    discoveryMark.SetActive(true);
                }
                else if (detectionPlayer)
                {
                    detectionMark.SetActive(true);
                    discoveryMark.SetActive(false);
                }
                else
                {
                    detectionMark.SetActive(false);
                    discoveryMark.SetActive(false);
                }
            }
        }

        //�÷��̾ ���� ������ ���� �� true
        private bool DetectPlayer() => Vector2.Distance(transform.position, PlayerController.GetPlayerTip()) <= myInfo.detectArea;
        //�÷��̾�� ���� ���̿� ��ֹ��� ���� �� true
        private bool DiscoverPlayer() => (detectionPlayer && !Physics2D.Linecast(atkTip.position, PlayerController.GetPlayerTip(), LayerMask.GetMask("Ground")));

    }
}