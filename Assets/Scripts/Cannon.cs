using UnityEngine;

namespace BallBlast
{
	public class Cannon : MonoBehaviour
	{
		[SerializeField] float _cannonSpeed = 3f;
		[SerializeField] float _motorSpeed = 150f;
		[SerializeField] float _screenBoundsOffset = 0.56f;
		[SerializeField] HingeJoint2D[] _wheels;
		
		private Camera _camera;
		private Rigidbody2D _rb;
		
		private JointMotor2D _motor;
		private bool _isMoving = false;
		private Vector2 _targetPos;
		private Vector2 _prevPos;
		private float _screenBounds;
		private float _velocityX;

		private void Start()
		{
			_camera = Camera.main;

			_rb = GetComponent<Rigidbody2D>();
			_targetPos = _rb.position;
			_prevPos = transform.position;

			_motor = _wheels[0].motor;

			_screenBounds = GameManager.Instance.ScreenWidth - _screenBoundsOffset;
		}

		private void Update()
		{
			_isMoving = Input.GetMouseButton(0);

			if (_isMoving)
			{
				_targetPos.x = _camera.ScreenToWorldPoint(Input.mousePosition).x;
			}
		}

		private void FixedUpdate()
		{
			MoveCannon();
			RotateWheels();
			_prevPos = transform.position;
		}

		private void MoveCannon()
        {
			if (_isMoving)
				_rb.MovePosition(Vector2.Lerp(_rb.position, _targetPos, _cannonSpeed * Time.fixedDeltaTime));
			else
				_rb.velocity = Vector2.zero;
		}

		private void RotateWheels()
        {
			_velocityX = (transform.position.x - _prevPos.x) / Time.fixedDeltaTime;
			if (Mathf.Abs(_velocityX) > 0.0f && Mathf.Abs(_rb.position.x) < _screenBounds)
			{
				_motor.motorSpeed = _velocityX * _motorSpeed;
				MotorActivate(true);
			}
			else
			{
				_motor.motorSpeed = 0f;
				MotorActivate(false);
			}
		}

		private void MotorActivate(bool isActive)
		{
			_wheels[0].useMotor = isActive;
			_wheels[1].useMotor = isActive;

			_wheels[0].motor = _motor;
			_wheels[1].motor = _motor;
		}
	}
}