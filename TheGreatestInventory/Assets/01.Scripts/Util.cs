using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 편하게 쓰기위한 확장메소드 모아놓은 클래스
/// </summary>
public static class Util
{
    /// <summary>
    /// 원하는 자식을 찾고, 그 자식에게서 원하는 컴포넌트를 가져옴
    /// </summary>
    /// <param name="_this"></param>
    /// <param name="_childName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T TryGetChildComponent<T>(this MonoBehaviour _this, string _childName) where T : class
    {
        var child = TryFindChild(_this, _childName);
        if (child == null) return null;

        T component = null;

        if (child.TryGetComponent<T>(out var findComponent)) component = findComponent;
        else Log($"{child.name}에 {typeof(T).Name}이라는 컴포넌트는 존재하지 않음");

        return component;
    }
    
    /// <summary>
    /// FindChild()를 예외처리한 버전
    /// </summary>
    /// <param name="_parent"></param>
    /// <param name="_childName"></param>
    /// <returns></returns>
    public static GameObject TryFindChild(this MonoBehaviour _parent, string _childName)
    {
        var child = FindChild(_parent.transform, _childName);
        if (child == null) Log($"{_parent.name}에 {_childName}이라는 자식 오브젝트는 존재하지 않음");

        return child;
    }

    /// <summary>
    /// 자신의 자식들 중 원하는 이름의 자식을 찾아줌
    /// </summary>
    /// <param name="_parent"></param>
    /// <param name="_childName"></param>
    /// <returns></returns>
    private static GameObject FindChild(Transform _parent, string _childName)
    {
        GameObject findChild = null;

        for (int i = 0; i < _parent.transform.childCount; i++)
        {
            var child = _parent.transform.GetChild(i);
            findChild = child.name == _childName ? child.gameObject : FindChild(child, _childName);
            if (findChild != null) break;
        }

        return findChild;
    }
    
    /// <summary>
    /// Debug.Log()를 에디터에서만 작동하게함
    /// </summary>
    /// <param name="_log"></param>
    public static void Log(string _log)
    {
#if UNITY_EDITOR
        Debug.Log(_log);
#endif
    }
}
