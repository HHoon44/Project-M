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
    /// ���������� ����Ʈ ���� �� ����� �޼������ ���� Ŭ����
    /// </summary>
    public class StaticDataImporter : MonoBehaviour
    {
        /// <summary>
        /// ���������� ����Ʈ �۾��� �����ϴ� �޼���
        /// </summary>
        /// <param name="importedAssets"> �߰��� ���� </param>
        /// <param name="deletedAssets"> ������ ���� </param>
        /// <param name="movedAssets"> �̵��� ���� </param>
        /// <param name="movedAssetsPaths"> �̵��� ������ ��� </param>
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
        /// ���� ������ ���̽� ���Ϸ� ���� �����ִ� �޼���
        /// </summary>
        /// <param name="assets"> ������ ���� ���� </param>
        /// <param name="isDeleted"> �������� ���� </param>
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

                    // ExcelToJsonConvert�� �̿��ؼ� ���� ������ ���̽� ���Ϸ� ����
                    var excelToJsonConvert =
                        new ExcelToJsonConvert(fileFullPath, $"{rootPath}/{Define.StaticData.SDJsonPath}");

                    // ������ �����ߴٸ� ���¿� ���̽� ������ �����մϴ�.
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