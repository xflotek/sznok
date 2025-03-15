using UnityEngine;

public class StaticData : MonoBehaviour
{
    public static bool[] shards = { false, false, false };

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
