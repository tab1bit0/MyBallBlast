using UnityEngine;

namespace BallBlast
{
	public class ScreenSides : MonoBehaviour
	{
		[SerializeField] BoxCollider2D leftWallCollider;
		[SerializeField] BoxCollider2D rightWallCollider;

		private void Start()
		{
			float screenWidth = GameManager.Instance.ScreenWidth;

			leftWallCollider.transform.position = new Vector3(-screenWidth - leftWallCollider.size.x / 2f, 0f, 0f);
			rightWallCollider.transform.position = new Vector3(screenWidth + rightWallCollider.size.x / 2f, 0f, 0f);

			Destroy(this);
		}

	}
}