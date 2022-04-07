using System.Collections;
using UnityEngine;
using TMPro;

namespace BallBlast
{
	public class Meteor : MonoBehaviour
	{
		private const string TAG_CANNON = "cannon";
		private const string TAG_MISSILE = "missile";
		private const string TAG_WALL = "wall";
		private const string TAG_GROUND = "ground";

		[SerializeField] protected int _health;
		[SerializeField] protected float _jumpForce;
		[SerializeField] protected LayerMask _wallLayer;
		[Header("Components")]
		[SerializeField] protected Rigidbody2D _rb;
		[SerializeField] protected TMP_Text _textHealth;

		[HideInInspector] public bool IsResultOfFission = true;

		protected float[] _leftAndRight = new float[2] { -1f, 1f };
		protected bool _isShowing;

		private void Start()
		{
			UpdateHealthUI();

			_isShowing = true;
			_rb.gravityScale = 0f;

			if (IsResultOfFission)
			{
				FallDown();
			}
			else
			{
				float direction = _leftAndRight[Random.Range(0, 2)];
				float screenOffset = GameManager.Instance.ScreenWidth * 1.3f;
				transform.position = new Vector2(screenOffset * direction, transform.position.y);

				_rb.velocity = new Vector2(-direction, 0f);
				StartCoroutine(PushMeteorCoroutine());
			}
		}

		private IEnumerator PushMeteorCoroutine()
        {
			yield return new WaitForSeconds(0.1f);
			while (IsCollideWall())
            {
				yield return null;
            }
			FallDown();
        }

		private bool IsCollideWall()
        {
			var collider = GetComponent<CircleCollider2D>();
			return collider.IsTouchingLayers(_wallLayer);
        }

		private void FallDown()
		{
			_isShowing = false;
			_rb.gravityScale = 1f;
			_rb.AddTorque(Random.Range(-20f, 20f));
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(TAG_CANNON))
			{
				Debug.Log("Game Over");
				GameManager.Instance.GameOver();
			}

			if (other.CompareTag(TAG_MISSILE))
			{
				TakeDamage(1);
				MissilesContoroller.Instance.DestroyMissile(other.gameObject);
			}

			if (!_isShowing && other.CompareTag(TAG_WALL))
			{
			 //hit wall
				float posX = transform.position.x;
				if (posX > 0)
				{
					//hit right wall
					_rb.AddForce(Vector2.left * 150f);
				}
				else
				{
					//hit left wall
					_rb.AddForce(Vector2.right * 150f);
				}

				_rb.AddTorque(posX * 4f);
			}

			if (other.CompareTag(TAG_GROUND))
			{
				_rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
				_rb.AddTorque(-_rb.angularVelocity * 4f);
			}
		}

		public void TakeDamage(int damage)
		{
			if (_health > 1)
				_health -= damage;
			else
				Die();

			UpdateHealthUI();
		}

		protected virtual void Die()
		{
			Destroy(gameObject);
		}

		protected void UpdateHealthUI()
		{
			_textHealth.text = _health.ToString();
		}
	}
}