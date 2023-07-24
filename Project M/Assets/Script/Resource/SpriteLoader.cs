using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static ProjectM.Define.Resource;

namespace ProjectM.Resource
{
    /// <summary>
    /// ���ӿ� ���Ǵ� ��� ��Ʋ�󽺸� �����ϴ� Ŭ����
    /// ��Ÿ�ӿ� ���Ǵ� ��� ��������Ʈ�� ��� �ش� Ŭ������ ���ؼ� �����´�
    /// </summary>
    public static class SpriteLoader
    {
        /// <summary>
        /// ��� ��Ʋ�󽺸� �����ϴ� ��ųʸ�
        /// </summary>
        private static Dictionary<AtlasType, SpriteAtlas> atlasDic = 
            new Dictionary<AtlasType, SpriteAtlas>();

        /// <summary>
        /// ����ϰ��� �ϴ� ��Ʋ�� ����� ��Ʋ�󽺵��� ��ųʸ��� ����ϴ� �޼���
        /// </summary>
        /// <param name="atlases"> ����ϰ��� �ϴ� ��Ʋ�� ��� </param>
        public static void SetAtlas(SpriteAtlas[] atlases)
        {
            for (int i = 0; i < atlases.Length; i++)
            {
                var key = (AtlasType)Enum.Parse(typeof(AtlasType), atlases[i].name);

                atlasDic.Add(key, atlases[i]);
            }
        }

        /// <summary>
        /// Ư�� ��Ʋ�󽺿��� ���ϴ� ��������Ʈ�� ã�Ƽ� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <param name="type"> ���ϴ� ��Ʋ���� Ű �� </param>
        /// <param name="spriteName"> ���ϴ� ��Ʋ���� �̸� </param>
        /// <returns></returns>
        public static Sprite GetSprite(AtlasType type, string spriteName)
        {
            if (!atlasDic.ContainsKey(type))
            {
                // ���ϴ� ��Ʈ����Ʈ�� ���ٸ�
                return null;
            }

            // Key ���� ���� ������ Dictionary�� ����
            // Key �� �ȿ� �ִ� SpriteAtlas�� �����Ͽ� SpriteAtlas�ȿ� �ִ� GetSprite�� ����
            // GetSprite�� ���ڷ� spriteName�� �����Ͽ� ���ϴ� Sprite�� ��ȯ
            return atlasDic[type].GetSprite(spriteName);
        }
    }
}
