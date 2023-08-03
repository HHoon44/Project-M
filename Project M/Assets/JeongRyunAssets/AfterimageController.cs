using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.InGame
{

    //잔상을 관리하는 컴포넌트 입니다.
    public class AfterimageController : MonoBehaviour
    {
        private SpriteRenderer[] afterimageArr;  //오브젝트 폴링 사용
        private GameObject targetObj;
        private SpriteRenderer targetRenderer;

        public float duration = 0.5f;  //잔상이 생성되고 꺼지는 시간
        public Color32 effectColor = new Color32(255, 255, 255, 50);

        private bool isActive = false;
        private bool stopAfterimage = false;

        public void InitializeThis(GameObject _obj, SpriteRenderer _renderer)
        {
            targetObj = _obj;
            targetRenderer = _renderer;
        }

        void Start()
        {
            afterimageArr = new SpriteRenderer[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                afterimageArr[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
                afterimageArr[i].gameObject.SetActive(false);
            }
        }

        public void StartAfterimage(float _time)
        {
            if (isActive)
                return;

            isActive = true;
            stopAfterimage = false;

            float frameTime = _time / transform.childCount;
            StartCoroutine(ShowAfterimage_co(frameTime));
            StartCoroutine(BlindAfterimage_co(frameTime));
        }

        public void StopAfterimage()
        {
            stopAfterimage = true;
        }

        private IEnumerator ShowAfterimage_co(float _frameTime)
        {
            foreach (var item in afterimageArr)
            {
                if (stopAfterimage)
                    break;

                item.gameObject.SetActive(true);

                item.sprite = targetRenderer.sprite;
                item.color = effectColor;

                item.transform.position = targetObj.transform.position;
                item.transform.rotation = targetObj.transform.rotation;
                item.transform.localScale = targetObj.transform.localScale;
                yield return new WaitForSeconds(_frameTime); //다음잔상까지 기다림
            }
        }
        private IEnumerator BlindAfterimage_co(float _frameTime)
        {
            yield return new WaitForSeconds(duration);
            foreach (var item in afterimageArr)
            {
                item.gameObject.SetActive(false);
                yield return new WaitForSeconds(_frameTime); //다음잔상까지 기다림
            }
            isActive = false;
        }
    }
}
