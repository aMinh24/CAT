using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> : MonoBehaviour where T:BaseManager<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (BaseManager<T>.instance == null)
            {
                BaseManager<T>.instance = FindAnyObjectByType<T>();
                if (BaseManager<T>.instance == null)
                {
                    Debug.Log($"No {typeof(T).Name} Singleton Instance");
                }
                
            }
            return BaseManager<T>.instance;
        }
    }
    protected virtual void Awake()
    {
        this.CheckInstance();
    }
    public static bool HasInstance
    {
        get
        {
            return BaseManager<T>.instance != null;
        }
    }
    protected bool CheckInstance()
    {
        if (BaseManager<T>.instance == null)
        {
            BaseManager<T>.instance = (T)((object)this);
            DontDestroyOnLoad(this);
            return true;
        }
        if (BaseManager<T>.instance == this)
        {
            DontDestroyOnLoad(this);
            return true;
        }
        Destroy(this.gameObject);
        return false;
    }
}
