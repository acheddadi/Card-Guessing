using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private float _fov, _currentVelocity = 0.0f, _smoothTime = 0.3f;
    private Vector3 _objPos;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _fov = _camera.fieldOfView;
    }
	
	private void Update()
	{
        _camera.fieldOfView = Mathf.SmoothDampAngle(_camera.fieldOfView, _fov, ref _currentVelocity, _smoothTime);
	}

    public void SetFov()
    {
        var theta = Mathf.Atan2(_camera.transform.position.z - _objPos.z, _camera.transform.position.y - _objPos.y) * Mathf.Rad2Deg;
        var camRot = _camera.transform.localEulerAngles.x;
        _fov = Mathf.DeltaAngle(-2 * (theta + camRot - 90.0f), 90.0f);
    }

    public void SetObjectPosition(Vector3 pos)
    {
        _objPos = pos;
    }
}
