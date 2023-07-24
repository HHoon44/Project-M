using ProjectM.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// ��Ÿ�ӿ� �ʿ��� ���ҽ��� �ҷ����� ����� ���� Ŭ����
/// </summary>
public class ResourceManager : Singleton<ResourceManager>
{
    public void Initialize()
    {
        // LoadAllAtlas();
        // LoadAllPrefabs();
    }

    /// <summary>
    /// ��θ� �̿��ؼ� ���ҽ� ���� ���� ��������
    /// �������� �޼���
    /// </summary>
    /// <param name="path"> ����� ��� </param>
    /// <returns></returns>
    public GameObject LoadObject(string path)
    {
        // ���� ���� �ȿ� ���ҽ� ������ �����Ѵٸ�
        // �ش� ��η� ���� path�� �о�ɴϴ�.
        // �ش� ��ο� ������ GameObject ���·� �θ� �� �ִٸ�
        // �ҷ��´�.
        return Resources.Load<GameObject>(path);
    }

    /// <summary>
    /// ���ҽ� ���� ���� ��� ��Ʋ�󽺸� �ҷ���
    /// ��������Ʈ �δ��� ����ϴ� �޼���
    /// </summary>
    private void LoadAllAtlas()
    {
        var portraitAtlas = Resources.LoadAll<SpriteAtlas>("Atlase/PortraitAtlase");
        // SpriteLoader.SetAtlas(portraitAtlas);

        var uiAtlas = Resources.LoadAll<SpriteAtlas>("Atlase/UIAtlase");
        // SpriteLoader.SetAtlas(uiAtlas);
    }
}