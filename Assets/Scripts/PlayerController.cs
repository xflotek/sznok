using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 speed = new Vector2(0f, 0f);
    InputAction move;
    void Start()
    {
        move = InputSystem.actions.FindAction("move");
    }

    void Update()
    {
        transform.position += (Vector3) (speed * move.ReadValue<Vector2>());
    }
}
