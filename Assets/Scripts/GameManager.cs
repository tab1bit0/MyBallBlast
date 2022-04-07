using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BallBlast
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }

		public float ScreenWidth { get; private set; }

		private void Awake()
		{
			Instance = this;
			ScreenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
			Application.targetFrameRate = 60;
		}

		public void GameOver()
        {
			Time.timeScale = 0;
			StartCoroutine(WaitAndRestartCoroutine(2f));
        }

        IEnumerator WaitAndRestartCoroutine(float delay)
        {
			yield return new WaitForSecondsRealtime(delay);
			RestartLevel();
        }

        private void RestartLevel()
        {
			Time.timeScale = 1f;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}