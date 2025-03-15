using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera TargetCamera;
    public Vector2[] CameraPositions = {new (0f,0f)};
    public float CameraSpeed = 4f;

    public Vector2 TargetPosition;
    
    public bool MoveTo(int index) {
        if (index < CameraPositions.Length) {
            TargetPosition = CameraPositions[index];
            return true;
        }
        return false;
    }

    void Start()
    {
        TargetPosition = CameraPositions[0];
        TargetCamera = Camera.main;
    }

    void Update()
    {
        Vector2 distance = new(
            TargetPosition.x - TargetCamera.transform.position.x,
            TargetPosition.y - TargetCamera.transform.position.y
        );

        TargetCamera.transform.position += new Vector3(
            distance.x * Time.deltaTime * CameraSpeed,
            distance.y * Time.deltaTime * CameraSpeed,
            0
        );
    }
}
