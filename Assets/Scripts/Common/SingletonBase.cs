using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T: class
{
    public static T Instance;

    protected virtual void Awake()
    {
        if (Instance != null) Destroy(Instance as Object);

        Instance = this as T;
    }
}
