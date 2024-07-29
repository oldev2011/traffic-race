using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float BorderPosX;
    public float Speed;
    public float MaxSpeed;
    public float MaxTurnAngle;
    public float TurnSpeed;
    public float Friction;
    public GameObject GasPrefab;
    public Transform GasSpawnPoint;

    private Quaternion _initialRotation;
    private Quaternion _targetRotation;
    private Vector3 _speed;

    private void Start()
    {
        _initialRotation = transform.rotation;
        StartCoroutine(SpawnGas());
    }
    private void FixedUpdate()
    {
        _speed *= 1 - Friction * Time.deltaTime;
        _speed += Input.GetAxis("Horizontal") * Vector3.right * Speed * Time.deltaTime;
        _speed = Vector3.ClampMagnitude(_speed, MaxSpeed);
        _speed = Vector3.Lerp(_speed.normalized, Vector3.right, Time.deltaTime * MaxSpeed) * _speed.magnitude;
        transform.position += _speed;
    }
    private void Update()
    {
        float clampX = Mathf.Clamp(transform.position.x, -BorderPosX, BorderPosX);
        transform.position = new Vector3(clampX, transform.position.y, transform.position.z);

        if (Input.GetKey(KeyCode.A))
        {
            _targetRotation = Quaternion.Euler(0, Mathf.Clamp(MaxTurnAngle, 0f, -MaxTurnAngle), 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _targetRotation = Quaternion.Euler(0f, Mathf.Clamp(MaxTurnAngle, 0f, MaxTurnAngle), 0);            
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            _targetRotation = _initialRotation;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            _targetRotation = _initialRotation;
        }

    }
    private void LateUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, TurnSpeed * Time.deltaTime);
    }
    private IEnumerator SpawnGas()
    {
        yield return new WaitForSeconds(0.01f);
        var gas = Instantiate(GasPrefab, GasSpawnPoint.position, Quaternion.Euler(0, GasPrefab.gameObject.transform.rotation.eulerAngles.y, 0));
        StartCoroutine(SpawnGas());
        Destroy(gas, 0.5f);
    }

}
