using ProjectM.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// 런타임에 필요한 리소스를 불러오는 기능을 가진 클래스
/// </summary>
public class ResourceManager : Singleton<ResourceManager>
{
    public void Initialize()
    {
        // LoadAllAtlas();
        // LoadAllPrefabs();
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
        var portraitAtlas = Resources.LoadAll<SpriteAtlas>("Atlase/PortraitAtlase");
        // SpriteLoader.SetAtlas(portraitAtlas);

        var uiAtlas = Resources.LoadAll<SpriteAtlas>("Atlase/UIAtlase");
        // SpriteLoader.SetAtlas(uiAtlas);
    }
}