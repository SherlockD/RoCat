using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<T>();
        }
    }
}
