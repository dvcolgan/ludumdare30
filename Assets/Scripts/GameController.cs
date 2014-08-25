using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
		public static GameController instance;
		public GameObject treePrefab;
		public GameObject stumpPrefab;
		public GameObject logPrefab;
		public GameObject cratePrefab;
		public GameObject shroomPrefab;
		public GameObject holePrefab;
		public GameObject filledHolePrefab;
		public GameObject stonePrefab;
		public GameObject crackedStoneFloorPrefab;

		public GameObject playerPrefab;
		
		public Texture2D testLevel;

		void Awake ()
		{
				DontDestroyOnLoad (this);
				instance = this;
		}

		public GameObject[] levels;
		GameObject currentLevel = null;
		int currentLevelNumber;

		void Start ()
		{
				LoadLevelFromTexture (testLevel);
				return;
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

		void LoadLevelFromTexture (Texture2D levelImage)
		{
				int width = 20;
				int height = 12;
				int imageXOffset = 71;
				int imageYOffset = 197;
	
				//string[] lines = levelText.text.Split ("\n" [0]);
				//int fileLineCount = 0;
	
				//GameObject levelHolderObject = Instantiate (levelHolder) as GameObject;
				//levelHolderObject.name = "LevelHolder";
        
				for (int x = 0; x < Constants.PAGE_WIDTH; x++) {
						for (int y = 0; y < Constants.PAGE_HEIGHT; y++) {
								var pixel = levelImage.GetPixel (x + imageXOffset, y + imageYOffset);
								
								int red = (int)(pixel.r * 255);
								int green = (int)(pixel.g * 255);
								int blue = (int)(pixel.b * 255);
								int alpha = (int)(pixel.a * 255);
								Debug.Log (red + " " + green + " " + blue);

								if (red == 0 && green == 127 && blue == 14) {
										GameObject obj = Instantiate (treePrefab, new Vector3 (x, -1, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (-90, 0, 0);

								} else if (red == 127 && green == 51 && blue == 0) {
										GameObject obj = Instantiate (stumpPrefab, new Vector3 (x, -1, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (-90, 0, 0);
							
								} else if (red == 255 && green == 106 && blue == 0) {
										GameObject obj = Instantiate (logPrefab, new Vector3 (x, -.5f, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (-90, 0, 0);
								
								} else if (red == 255 && green == 178 && blue == 127) {
										GameObject obj = Instantiate (cratePrefab, new Vector3 (x, -.5f, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (-90, 0, 0);

								} else if (red == 72 && green == 0 && blue == 255) {
										GameObject obj = Instantiate (shroomPrefab, new Vector3 (x, -1, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (-90, 0, 0);
								
								} else if (red == 64 && green == 64 && blue == 64) {
										GameObject obj = Instantiate (holePrefab, new Vector3 (x, 0, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (90, 0, 0);
								
								} else if (red == 127 && green == 89 && blue == 63) {
										GameObject obj = Instantiate (stonePrefab, new Vector3 (x, 0, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (90, 0, 0);
								
								} else if (red == 128 && green == 128 && blue == 128) {
										GameObject obj = Instantiate (crackedStoneFloorPrefab, new Vector3 (x, -.94f, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (0, 0, 0);
								}
						}
				}

				/*
				Object playerLightObject = Instantiate (playerLight);
				playerLightObject.name = "FollowLight";
    
				Object cameraObject = Instantiate (camera);
				cameraObject.name = "MainCamera";
				*/
		}
}
