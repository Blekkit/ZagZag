using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] private float _baseMoveSpeed = 5f;

    private Rigidbody _rb;
    private Vector2 _moveInput;
    private float _currentMoveSpeed;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void SetMoveSpeedModifier(float speedModidfier)
    {
        _currentMoveSpeed *= speedModidfier;
    }

    public void SetTempSpeed(float currentSpeed)
    {
        _currentMoveSpeed = currentSpeed;
    }

    public void ResetSpeed()
    {
        _currentMoveSpeed = _baseMoveSpeed;
    }

    private void Update()
    {
        Vector3 move = new Vector3(_moveInput.x, 0f, _moveInput.y) * _currentMoveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _currentMoveSpeed = _baseMoveSpeed;
    }
}
