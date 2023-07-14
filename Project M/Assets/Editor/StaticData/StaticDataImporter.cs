using Mono.Cecil.Cil;
using ProjectW.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using UnityEditor;
using UnityEngine;

namespace ProjectM.Editor
{
    /// <summary>
    /// 엑셀파일을 임포트 했을 때 사용할 메서드들을 지닌 클래스
    /// </summary>
    public class StaticDataImporter : MonoBehaviour
    {
        /// <summary>
        /// 엑셀파일을 임포트 작업을 진행하는 메서드
        /// </summary>
        /// <param name="importedAssets"> 추가할 에셋 </param>
        /// <param name="deletedAssets"> 삭제할 에셋 </param>
        /// <param name="movedAssets"> 이동할 에셋 </param>
        /// <param name="movedAssetsPaths"> 이동한 에셋의 경로 </param>
        public static void Import(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedAssetsPaths)
        {
            ImportNewOrModifed(importedAssets);
            Delete(deletedAssets);
            Move(movedAssets, movedAssetsPaths);
        }

        private static void ImportNewOrModifed(string[] importedAssets)
        {
            ExcelToJson(importedAssets, false);
        }

        private static void Delete(string[] deleteAssets)
        {
            ExcelToJson(deleteAssets, true);
        }

        private static void Move(string[] movedAssets, string[] movedAssetsPaths)
        {
            Delete(movedAssetsPaths);
            ImportNewOrModifed(movedAssets);
        }

        /// <summary>
        /// 엑셀 파일을 제이슨 파일로 변형 시켜주는 메서드
        /// </summary>
        /// <param name="assets"> 변형할 엑셀 파일 </param>
        /// <param name="isDeleted"> 삭제할지 말지 </param>
        private static void ExcelToJson(string[] assets, bool isDeleted)
        {
            List<string> staticDataAssets = new List<string>();

            foreach (var asset in assets)
            {
                if (IsStaticData(asset, isDeleted))
                {
                    staticDataAssets.Add(asset);
                }
            }

            foreach (var staticDataAsset in staticDataAssets)
            {
                try
                {
                    var fileName = staticDataAsset.Substring(staticDataAsset.LastIndexOf('/') + 1);
                    fileName = fileName.Remove(fileName.LastIndexOf('.'));

                    var rootPath = Application.dataPath;
                    rootPath = rootPath.Remove(rootPath.LastIndexOf('/'));

                    var fileFullPath = $"{rootPath}/{staticDataAsset}";

                    // ExcelToJsonConvert를 이용해서 엑셀 파일을 제이슨 파일로 변형
                    var excelToJsonConvert =
                        new ExcelToJsonConvert(fileFullPath, $"{rootPath}/{Define.StaticData.SDJsonPath}");

                    // 변형이 성공했다면 에셋에 제이슨 파일을 저장합니다.
                    if (excelToJsonConvert.SaveJsonFiles() > 0)
                    {
                        AssetDatabase.ImportAsset($"{Define.StaticData.SDJsonPath}/{fileName}.json");
                        Debug.Log($"### StaticData {fileName} reimported");
                    }
                }
                catch (Exception c)
                {
                    Debug.LogError(c);
                    Debug.LogErrorFormat("Couldn't convert assets = {0}", staticDataAsset);
                    EditorUtility.DisplayDialog("Error Convert",
                        string.Format("Couldn' t convert assets = {0}", staticDataAsset), "ok");
                }
            }
        }

        private static bool IsStaticData(string path, bool isDeleted)
        {
            if (path.EndsWith(".xlsx") == false)
            {
                return false;
            }

            var absolutePath = Application.dataPath + path.Remove(0, "Assets".Length);

            return ((isDeleted || File.Exists(absolutePath)) &&
                (path.StartsWith(Define.StaticData.SDExcelPath)));
        }
    }
}