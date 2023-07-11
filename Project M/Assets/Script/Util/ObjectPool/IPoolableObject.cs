using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.Util
{
    /// <summary>
    /// 오브젝트 풀링을 사용하는 클래스에서 구현 해야하는 인터페이스
    /// 오브젝트 풀링을 사용할 객체는 해당 인터페이스를 상속 받아야만
    /// 오브젝트 풀링을 사용할 수 있다.
    /// </summary>
    public interface IPoolableObject
    {
        /// <summary>
        /// 오브젝트가 재사용 될 수있음을 나타내는 프로퍼티
        /// 오브젝트 풀에서 꺼내서 사용 할 수있는 상태인지를 나타내는 필드
        /// </summary>
        bool CanReCycle { get; set; }
    }
}