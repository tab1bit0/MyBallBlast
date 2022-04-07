using UnityEngine;

namespace BallBlast
{
	public class MeteorFissionable : Meteor
	{
		[SerializeField] Meteor[] _splitsPrefabs;

		protected override void Die()
		{
			SplitMeteor();
			Destroy(gameObject);
		}

		private void SplitMeteor()
		{
			for (int i = 0; i < 2; i++)
			{
				var meteor = Instantiate(_splitsPrefabs[i], transform.position, Quaternion.identity, MeteorSpawner.Instance.transform);
				meteor.GetComponent<Rigidbody2D>().velocity = new Vector2(_leftAndRight[i], 5f);

				meteor.transform.localScale = meteor.transform.localScale / 1.5f;
			}
		}
	}
}