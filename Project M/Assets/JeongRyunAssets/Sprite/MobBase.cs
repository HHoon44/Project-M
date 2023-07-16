using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectM.Define;

namespace ProjectM.InGame
{
    public class MobBase : MonoBehaviour
    {
        [Header("MobSetting")]
        public MobType thisMobType = MobType.NoneMovementMob;

        [Space(10f)]
        public GameObject myForm;   //�������� ���̴� ������ ��ü
        public Transform atkTip;  //������ �����ϴ� ��ġ

        [Space(10f)]
        [Header("MobEmotion")]
        public GameObject detectionMark;
        public GameObject discoveryMark;

        public Animator formAnim { get; private set; }
        private IMobConsistModule[] myModule = new IMobConsistModule[3];

        //���� ����
        public float nowHP { get; private set; }
        public bool detectionPlayer { get; private set; }  //�÷��̾ ��ó�� �ִٸ�
        public bool discoveryPlayer { get; private set; }  //�÷��̾ ���δٸ�
        public bool isLive { get; private set; }   //�ڽ��� �׾��ٸ�

        //���� ���
        public MobReferenceData myReference { get; private set; }
        public MobMovemantData myMovement { get; private set; }

        public Vector2 colPoint { get; protected set; }


        private void Start()
        {
            myReference = MobsStaticData.Instance.GetMobReferenceInfo(thisMobType);
            myMovement = MobsStaticData.Instance.GetMobMovemantData(thisMobType);

            if (gameObject.tag != "Mob")
            {
                Debug.LogWarning("���� �� ���� ������Ʈ�� �ٸ� ��ü�� ������ �ֽ��ϴ�.");
                this.enabled = false;
            }

            formAnim = myForm.GetComponent<Animator>();
            CapsuleCollider2D col = myForm.GetComponent<CapsuleCollider2D>();
            colPoint = new Vector2(col.size.x / 2 + col.offset.x, -col.size.y / 2 + col.offset.y);

            Debug.Log(colPoint);

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
            if (myMovement.jumpForce <= 0)
                myModule[0] = Instantiate(MobsStaticData.mobMovementModule, transform).GetComponent<MobMovement>();
            else
                myModule[0] = Instantiate(MobsStaticData.mobJumpModule, transform).GetComponent<MobJump>();

            if (myMovement.dashForce >= 0)
                myModule[1] = Instantiate(MobsStaticData.mobDashModule, transform).GetComponent<MobDash>(); ;

            if (myReference.atkCool >= 0)
                myModule[2] = Instantiate(MobsStaticData.mobAttackModule, transform).GetComponent<MobAttack>();

            foreach (IMobConsistModule item in myModule)
            {
                if (item == null)
                    continue;

                item.Initialize(this);
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
            nowHP = myReference.maxHP;
            myForm.SetActive(true);
        }

        //act: �������� ���� �� ȣ��
        public void SufferDemage(float _Demaged, DebuffType[] _types)
        {
            //�������� ���̶�� ������ ���� ����
            if (myReference.staticMob)
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
            if (myReference.detectArea == 0)
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
        private bool DetectPlayer() => Vector2.Distance(transform.position, PlayerController.GetPlayerTip()) <= myReference.detectArea;
        //�÷��̾�� ���� ���̿� ��ֹ��� ���� �� true
        private bool DiscoverPlayer() => (detectionPlayer && !Physics2D.Linecast(atkTip.position, PlayerController.GetPlayerTip(), LayerMask.GetMask("Ground")));

    }
}