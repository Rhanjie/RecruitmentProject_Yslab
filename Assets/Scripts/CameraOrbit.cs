using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public float distance = 10f;
    public float rotationSpeed = 30f;

    private float _currentAngleX = 0f;
    private float _currentAngleY = 20f;
    private Vector3 _lastMousePosition;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("No target reference found!");
            return;
        }
        
        UpdateCameraPosition();
    }

    private void Update()
    {
        if (target == null)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePosition = Input.mousePosition;
        }

        if (!Input.GetMouseButton(0))
            return;
        
        var delta = Input.mousePosition - _lastMousePosition;
            
        _currentAngleX -= delta.x * rotationSpeed * Time.deltaTime;
        _currentAngleY -= delta.y * rotationSpeed * Time.deltaTime;
        
        _currentAngleY = Mathf.Clamp(_currentAngleY, -80f, 80f);
            
        _lastMousePosition = Input.mousePosition;
            
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        var radX = _currentAngleX * Mathf.Deg2Rad;
        var radY = _currentAngleY * Mathf.Deg2Rad;
        
        var x = target.position.x + distance * Mathf.Cos(radY) * Mathf.Cos(radX);
        var y = target.position.y + distance * Mathf.Sin(radY) + 6f;
        var z = target.position.z + distance * Mathf.Cos(radY) * Mathf.Sin(radX);
        
        transform.position = new Vector3(x, y, z);
        transform.LookAt(target);
    }
}