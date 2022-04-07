using UnityEngine;

namespace BallBlast
{
	public class Clouds : MonoBehaviour
	{
		[System.Serializable]
		private class Cloud
		{
			public MeshRenderer meshRenderer = null;
			public float speed = 0.03f;
			[HideInInspector] public Vector2 offset;
			[HideInInspector] public Material mat;
		}

		[SerializeField] Cloud[] _allClouds;

		private int _count;

		private void Start()
		{
			_count = _allClouds.Length;
			for (int i = 0; i < _count; i++)
			{
				_allClouds[i].offset = Vector2.zero;
				_allClouds[i].mat = _allClouds[i].meshRenderer.material;
			}
		}

		private void Update()
		{
			for (int i = 0; i < _count; i++)
			{
				_allClouds[i].offset.x += _allClouds[i].speed * Time.deltaTime;
				_allClouds[i].mat.mainTextureOffset = _allClouds[i].offset;
			}
		}
	}
}