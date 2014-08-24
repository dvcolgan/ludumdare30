using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
		public static GameController instance;
		public GameObject playerPrefab;

		public GameObject holePrefab;
		public GameObject filledHolePrefab;

		void Awake ()
		{
				instance = this;
		}

		public GameObject[] levels;
		GameObject currentLevel = null;
		int currentLevelNumber;

		void Start ()
		{
				currentLevel = GameObject.FindGameObjectWithTag ("Level");
				currentLevelNumber = int.Parse (currentLevel.name.Substring (5, 1));
				if (currentLevel == null) {
						LoadLevel (currentLevelNumber);
				}
				GameObject player = GameObject.FindGameObjectWithTag ("Player");
				if (player == null) {
						SpawnPlayer (0, 5);
				}
		}

		public void LoadNextLevel (float playerZ)
		{
				if (currentLevelNumber < levels.Length - 1) {
						currentLevelNumber++;
						LoadLevel (currentLevelNumber);
						SpawnPlayer (0, playerZ);
				}
		}

		public void LoadPreviousLevel (float playerZ)
		{
				if (currentLevelNumber > 0) {
						currentLevelNumber--;
						LoadLevel (currentLevelNumber);
						SpawnPlayer (Constants.PAGE_WIDTH - 1, playerZ);
				}
		}

		void SpawnPlayer (float playerX, float playerZ)
		{
				GameObject player = Instantiate (playerPrefab, new Vector3 (playerX, 0, playerZ), Quaternion.identity) as GameObject;
				player.transform.parent = currentLevel.transform;
		}

		void LoadLevel (int which)
		{
				if (currentLevel != null) {
						Destroy (currentLevel);
				}
				currentLevel = Instantiate (levels [which]) as GameObject;
		}
	
		// Update is called once per frame
		void Update ()
		{	
				return;
				if (Input.GetKeyDown (KeyCode.LeftArrow)) {
						transform.animation.Play ("PreviousPage");
						currentLevelNumber--;
						LoadLevel (currentLevelNumber);
				}
				if (Input.GetKeyDown (KeyCode.RightArrow)) {
						transform.animation.Play ("NextPage");
						currentLevelNumber++;
						LoadLevel (currentLevelNumber);
				}
		}
}
