using ProjectM.Define;
using ProjectM.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

namespace ProjectM.Resource
{
    /// <summary>
    /// 런타임에 필요한 리소스를 불러오는 기능을 가진 클래스
    /// </summary>
    public class ResourceManager : Singleton<ResourceManager>
    {
        public void Initialize()
        {
            LoadAllAtlas();
            LoadAllPrefabs();
        }

        /// <summary>
        /// 경로를 이용해서 리소스 폴더 안의 프리팹을
        /// 가져오는 메서드
        /// </summary>
        /// <param name="path"> 사용할 경로 </param>
        /// <returns></returns>
        public GameObject LoadObject(string path)
        {
            // 에셋 폴더 안에 리소스 폴더가 존재한다면
            // 해당 경로로 부터 path를 읽어옵니다.
            // 해당 경로에 파일이 GameObject 형태로 부를 수 있다면
            // 불러온다.
            return Resources.Load<GameObject>(path);
        }

        /// <summary>
        /// 리소스 폴더 안의 모든 아틀라스를 불러와
        /// 스프라이트 로더에 등록하는 메서드
        /// </summary>
        private void LoadAllAtlas()
        {
            var portraitAtlas = Resources.LoadAll<SpriteAtlas>("Atlas/PortraitAtlas");
            SpriteLoader.SetAtlas(portraitAtlas);

            var uiAtlas = Resources.LoadAll<SpriteAtlas>("Atlas/UIAtlas");
            SpriteLoader.SetAtlas(uiAtlas);

            var itemAtlas = Resources.LoadAll<SpriteAtlas>("Atlas/ItemAtlas");
            SpriteLoader.SetAtlas(itemAtlas);
        }

        /// <summary>
        /// 인게임에서 사용할 프리팹을 풀에 등록 요청을 보내는 메서드
        /// </summary>
        public void LoadAllPrefabs()
        {
            // 여기는 나중에 구현
        }

        /// <summary>
        /// 오브젝트 풀로 사용할 프리팹을 생성하고 풀에 등록하는 메서드
        /// </summary>
        /// <typeparam name="T"> 로드 하고자 하는 프리팹이 갖는 타입 </typeparam>
        /// <param name="poolType"> 등록하고자 하는 타입 </param>
        /// <param name="path"> 프리팹이 존재하는 경로 </param>
        /// <param name="poolCount"> 풀에 등록할 갯수 </param>
        /// <param name="loadComplete"> 작업을 끝내고 실행할 작업 </param>
        public void LoadPoolableObject<T>(PoolType poolType, string path, int poolCount = 1,
            Action loadComplete = null)
            where T : MonoBehaviour, IPoolableObject
        {
            // 프리팹을 가져온다
            var obj = LoadObject(path);

            // 가져온 프리팹의 T 타입을 가져온다
            var tComponent = obj.GetComponent<T>();

            // 풀 매니저에 가져온 프리팹을 등록한다
            ObjectPoolManager.Instance.RegistPool<T>(poolType, tComponent, poolCount);

            // 위 작업이 모두 끝난 후에 실행 시킬 작업이 있다면 실행한다
            loadComplete?.Invoke();
        }
    }
}