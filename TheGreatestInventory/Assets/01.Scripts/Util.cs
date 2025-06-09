using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static T TryGetChildComponent<T>(this MonoBehaviour _this, string _childName) where T : class
    {
        var child = TryFindChild(_this, _childName);
        if (child == null) return null;

        T component = null;

        if (child.TryGetComponent<T>(out var findComponent)) component = findComponent;
        else Log($"{child.name}에 {typeof(T).Name}이라는 컴포넌트는 존재하지 않음");

        return component;
    }
    
    public static GameObject TryFindChild(this MonoBehaviour _parent, string _childName)
    {
        var child = FindChild(_parent.transform, _childName);
        if (child == null) Log($"{_parent.name}에 {_childName}이라는 자식 오브젝트는 존재하지 않음");

        return child;
    }

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
    
    public static void Log(string _log)
    {
#if UNITY_EDITOR
        Debug.Log(_log);
#endif
    }
}
