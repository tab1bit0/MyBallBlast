using System.Collections.Generic;
using UnityEngine;

namespace BallBlast
{
	public class MissilesContoroller : MonoBehaviour
	{
		public static MissilesContoroller Instance { get; private set; }

		private const string TAG_MISSILE = "missile";

		[SerializeField] GameObject _missilePrefab;
		[SerializeField] int _missilesCount = 10;
		[Header("Fire props")]
		[SerializeField] float _fireDelay = 0.2f;
		[SerializeField] float _flySpeed = 8f;

		private Queue<GameObject> _missilesQueue;
		private GameObject _missileObject;
		private float _timer = 0f;

        private void Awake() => Instance = this;

        private void Start() => PrepareMissiles();

        private void Update()
		{
			_timer += Time.deltaTime;
			if (_timer >= _fireDelay && Input.GetMouseButton(0))
			{
				_timer = 0f;
				_missileObject = SpawnMissile(transform.position);
				if (_missileObject != null)
					_missileObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * _flySpeed;
			}
		}

		private void PrepareMissiles()
		{
			_missilesQueue = new Queue<GameObject>();
			for (int i = 0; i < _missilesCount; i++)
			{
				_missileObject = Instantiate(_missilePrefab, transform.position, Quaternion.identity, transform);
				_missileObject.SetActive(false);
				_missilesQueue.Enqueue(_missileObject);
			}
		}

		public GameObject SpawnMissile(Vector2 position)
		{
			if (_missilesQueue.Count > 0)
			{
				_missileObject = _missilesQueue.Dequeue();
				_missileObject.transform.position = position;
				_missileObject.SetActive(true);
				return _missileObject;
			}

			return null;
		}

		public void DestroyMissile(GameObject missile)
		{
			_missilesQueue.Enqueue(missile);
			missile.SetActive(false);
		}

		//missile collision with top collider
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(TAG_MISSILE))
			{
				DestroyMissile(other.gameObject);
			}
		}
	}
}