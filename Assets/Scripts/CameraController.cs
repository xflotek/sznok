using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;
    public float CameraSpeed;

    void Update()
    {
        Vector2 Distance = Target.transform.position - transform.position;

    }
}
