using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * AssetsPostProcessor
 * - ������ ����� �� ���� �ݹ��Լ��� ���� �� �ְ� ���ش�.
 */

namespace ProjectM.Editor
{
    /// <summary>
    /// �׼� ���� ����Ʈ �۾��� �����ϴ� Ŭ����
    /// </summary>
    public class MyAssetsPostProcessor : AssetPostprocessor
    {
        private static void OnPostProcessorAllAssets(string[] importedAssets, string[] deletedAssets,
         string[] movedAssets, string[] movedFromAssetsPath)
        {
            StaticDataImporter.Import(importedAssets, deletedAssets, movedAssets, movedFromAssetsPath);
        }
    }
}