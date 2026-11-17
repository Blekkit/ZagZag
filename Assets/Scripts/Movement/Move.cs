using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _baseMoveSpeed = 5f;

    [Header("Skating Settings")]
    [SerializeField] public bool _isUsingScateMovement = false;
    [SerializeField] private float _timeBetweenScates = 0.3f;
    [SerializeField] private float _rotationAngleRounding = 0.1f;
    [SerializeField] private ScateData _scateData;

    [Header("References")]
    [SerializeField] private Rigidbody _playerRB;
    //[SerializeField] private Transform _legsTransform;
    [SerializeField] private CharacterRotation _charRotation;

    public bool IsUsingScateMovement => _isUsingScateMovement;

    private Vector2 _moveInput;
    private float _currentMoveSpeed;
    private bool _canMove = true;
    private bool _isMoving = false;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    #region setters
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

    public void StopScateMovement()
    {
        _isMoving = false;
        StopCoroutine(ScateMovementLoop());
    }
    #endregion

    public IEnumerator Scate(ScateData data)
    {
        //Debug.Log($"Scate started by {gameObject.name}");
        float scateTimer = 0;
        float turnAngle = 0;
        float scateSpeed = data.ScateSpeed;
        _canMove = false;
        while (scateTimer <= data.ScateTime)
        {
            scateTimer += Time.deltaTime;
            
            //apply force
            _playerRB.linearVelocity = _charRotation.GetBodyPartForward(EPlayerBodyPart.legs, out Vector3 forward) * scateSpeed;
            scateSpeed -= data.SpeedLossRate * Time.deltaTime;

            //rotate player
            Vector3 moveInput = new Vector3(_moveInput.x, 0, _moveInput.y);
            turnAngle = Vector3.SignedAngle(forward, moveInput, Vector3.up);
            if (turnAngle <= _rotationAngleRounding && turnAngle >= -_rotationAngleRounding) 
                turnAngle = 0;
            turnAngle *= data.TurnAngle;

            _charRotation.RotateBodyPart(turnAngle * Time.deltaTime, EPlayerBodyPart.legs);
            //_legsTransform.Rotate(Vector3.up, turnAngle * Time.deltaTime);
            
            //data.Forward = Quaternion.AngleAxis(data.TurnAngle, Vector3.up) * data.Forward;
            yield return null;
        }
        //Debug.Log($"Scate ended by {gameObject.name}");
    }

    public IEnumerator ScateMovementLoop()
    {
        while (_isMoving)
        {
            yield return StartCoroutine(Scate(_scateData));
            yield return new WaitForSeconds(_timeBetweenScates);
        }
    }

    public void StartScateLoop()
    {
        _isMoving = true;
        StartCoroutine(ScateMovementLoop());
    }

    private void Update()
    {
        if (!_isUsingScateMovement && _canMove)
        {
            Vector3 move = new Vector3(_moveInput.x, 0f, _moveInput.y) * _currentMoveSpeed * Time.deltaTime;
            transform.Translate(move, Space.World);
        }
    }

    private void Start()
    {
        _currentMoveSpeed = _baseMoveSpeed;
    }
}
