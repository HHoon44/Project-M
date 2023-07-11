using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.Util
{
    /// <summary>
    /// ������Ʈ Ǯ���� ����ϴ� Ŭ�������� ���� �ؾ��ϴ� �������̽�
    /// ������Ʈ Ǯ���� ����� ��ü�� �ش� �������̽��� ��� �޾ƾ߸�
    /// ������Ʈ Ǯ���� ����� �� �ִ�.
    /// </summary>
    public interface IPoolableObject
    {
        /// <summary>
        /// ������Ʈ�� ���� �� �������� ��Ÿ���� ������Ƽ
        /// ������Ʈ Ǯ���� ������ ��� �� ���ִ� ���������� ��Ÿ���� �ʵ�
        /// </summary>
        bool CanReCycle { get; set; }
    }
}