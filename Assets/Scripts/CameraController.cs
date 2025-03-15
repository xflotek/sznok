using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;
    public float CameraSpeed;
    public Camera TargetCamera;

    void Start()
    {
        TargetCamera = Camera.main;
        Target = gameObject;
    } 

    void Update()
    {
        TargetCamera.transform.position = new Vector3(
            Target.transform.position.x,
            Target.transform.position.y,
            TargetCamera.transform.position.z
        );
    }
}
