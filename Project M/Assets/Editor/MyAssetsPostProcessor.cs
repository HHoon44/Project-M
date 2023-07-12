using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * AssetsPostProcessor
 * - 에셋이 변경될 때 마다 콜백함수를 받을 수 있게 해준다.
 */

namespace ProjectM.Editor
{
    /// <summary>
    /// 액셀 파일 임포트 작업을 진행하는 클래스
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