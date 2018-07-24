using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance
    {

        get
        {
            if (instance == null)
            {
                new GameObject(typeof(T).ToString(), typeof(T));
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = (T)this;
        DontDestroyOnLoad(gameObject);
    }
}
