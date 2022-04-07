using UnityEngine;
using System.Collections;

namespace BallBlast
{
	public class MeteorSpawner : MonoBehaviour
	{
		public static MeteorSpawner Instance { get; private set; }

		[SerializeField] int _meteorsCount;
		[SerializeField] float _spawnDelay;
		[SerializeField] Meteor[] _meteorPrefabs;

        private void Awake() => Instance = this;

        private void Start()
		{
			StartCoroutine(SpawnMeteors());
		}

		IEnumerator SpawnMeteors()
		{
			for (int i = 0; i < _meteorsCount; i++)
			{
				CreateMeteor();
				yield return new WaitForSeconds(_spawnDelay);
			}
		}

		private void CreateMeteor()
        {
			var prefab = _meteorPrefabs[Random.Range(0, _meteorPrefabs.Length)];
			var meteor = Instantiate(prefab, transform);
			meteor.IsResultOfFission = false;
		}
	}
}