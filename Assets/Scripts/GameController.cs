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
		public GameObject solidIceFloorPrefab;
		public GameObject crackedIceFloorPrefab;
		public GameObject buttonPrefab;
		public GameObject gatePrefab;

		public AudioClip footstepSound;
		public AudioClip pushSound;
		public AudioClip slideSound;

		public GameObject playerPrefab;
		
		public Texture2D[] levelImages;

		public bool playerIsMoving = false;

		float playerStartX = 0;
		float playerStartZ = 0;

		void Awake ()
		{
				instance = this;
		}

		public int startingLevelNumber = 0;
		int currentLevelNumber;

		void Start ()
		{
				currentLevelNumber = startingLevelNumber;
				StartCoroutine ("LoadLevel");
		}

		public void LoadNextLevel ()
		{
				if (currentLevelNumber < levelImages.Length - 1) {
						currentLevelNumber++;
						StartCoroutine ("LoadLevel");
				}
		}

		public void ReloadLevel ()
		{
				StartCoroutine ("LoadLevel");
		}

		public void LoadPreviousLevel ()
		{
				if (currentLevelNumber > 0) {
						currentLevelNumber--;
						StartCoroutine ("LoadLevel");
				}
		}

		void CreatePageText (int which)
		{
				TextMesh text = GameObject.FindGameObjectWithTag ("PageText").GetComponent <TextMesh> ();
				text.text = Constants.bookText [which];
		}

		IEnumerator LoadLevel ()
		{
				
			
				
				animation.Play ("PreviousPage");
				yield return new WaitForSeconds (.5f);
				foreach (GameObject obj in GameObject.FindGameObjectsWithTag("LevelObject")) {
						Destroy (obj);
				}
				LoadLevelFromTexture (levelImages [currentLevelNumber]);

				CreatePageText (currentLevelNumber);
				yield return null;
				
		}
	
		// Update is called once per frame
		void Update ()
		{	
				if (Input.GetKeyDown (KeyCode.J)) {
						LoadPreviousLevel ();
				}
				if (Input.GetKeyDown (KeyCode.K)) {
						LoadNextLevel ();
				}

				if (Input.GetKeyDown (KeyCode.R)) {
						ReloadLevel ();
				}
		}

	
		void RandomlyOrientObject (GameObject obj)
		{
				float rotationAmount = (float)Random.Range (0, 360);
				obj.transform.Rotate (new Vector3 (0, rotationAmount, 0), Space.World);
		}

		void RandomlyOrientObjectSnapped (GameObject obj)
		{
				float rotationAmount = Random.Range (0, 4) * 90f;
				obj.transform.Rotate (new Vector3 (0, rotationAmount, 0), Space.World);
		}

		void LoadLevelFromTexture (Texture2D levelImage)
		{
				int width = 20;
				int height = 12;
				int imageXOffset = 0;
				int imageYOffset = 4;
        
				for (int x = 0; x < Constants.PAGE_WIDTH; x++) {
						for (int y = 0; y < Constants.PAGE_HEIGHT; y++) {
								var pixel = levelImage.GetPixel (x + imageXOffset, y + imageYOffset);
								
								int red = (int)(pixel.r * 255);
								int green = (int)(pixel.g * 255);
								int blue = (int)(pixel.b * 255);
								int alpha = (int)(pixel.a * 255);

								GameObject obj = null;

								if (red == 0 && green == 127 && blue == 14) {
										obj = Instantiate (treePrefab, new Vector3 (x, -1, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (-90, 0, 0);
										RandomlyOrientObject (obj);

								} else if (red == 127 && green == 51 && blue == 0) {
										obj = Instantiate (stumpPrefab, new Vector3 (x, -1, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (-90, 0, 0);
										RandomlyOrientObject (obj);
							
								} else if (red == 255 && green == 178 && blue == 127) {
										obj = Instantiate (cratePrefab, new Vector3 (x, -.5f, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (-90, 0, 0);
										RandomlyOrientObjectSnapped (obj);

								} else if (red == 72 && green == 0 && blue == 255) {
										obj = Instantiate (shroomPrefab, new Vector3 (x, -1, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (-90, 0, 0);
										RandomlyOrientObject (obj);
								
								} else if (red == 64 && green == 64 && blue == 64) {
										obj = Instantiate (holePrefab, new Vector3 (x, -.94f, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (-90, 0, 0);
										RandomlyOrientObjectSnapped (obj);
								
								} else if (red == 127 && green == 89 && blue == 63) {
										obj = Instantiate (stonePrefab, new Vector3 (x, -.5f, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (90, 0, 0);
										RandomlyOrientObjectSnapped (obj);
								
								} else if (red == 128 && green == 128 && blue == 128) {
										obj = Instantiate (crackedStoneFloorPrefab, new Vector3 (x, -.94f, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (0, 0, 0);
										RandomlyOrientObjectSnapped (obj);

								} else if (red == 0 && green == 255 && blue == 255) {
										obj = Instantiate (solidIceFloorPrefab, new Vector3 (x, -.94f, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (-90, 0, 0);
										RandomlyOrientObjectSnapped (obj);

								} else if (red == 63 && green == 127 && blue == 127) {
										obj = Instantiate (crackedIceFloorPrefab, new Vector3 (x, -.94f, y), Quaternion.identity) as GameObject;
										//obj.transform.Rotate (0, 0, 0);
										RandomlyOrientObjectSnapped (obj);

								} else if (red == 255 && green == 0 && blue == 0) {
										playerStartX = x;
										playerStartZ = y;
										obj = Instantiate (playerPrefab, new Vector3 (playerStartX, 0, playerStartZ), Quaternion.identity) as GameObject;   

								} else if (red == 255 && green == 106 && blue == 0) { // upright log
										obj = Instantiate (logPrefab, new Vector3 (x, -.5f, y), Quaternion.identity) as GameObject;
										obj.SendMessage ("OrientLogUpright");
								} else if (red == 211 && green == 84 && blue == 0) { // horizontal log
										obj = Instantiate (logPrefab, new Vector3 (x, -.5f, y), Quaternion.identity) as GameObject;
										obj.SendMessage ("OrientLogHorizontally");
								} else if (red == 165 && green == 115 && blue == 81) { // vertical log
										obj = Instantiate (logPrefab, new Vector3 (x, -.5f, y), Quaternion.identity) as GameObject;
										obj.SendMessage ("OrientLogVertically");

								} else if (red == 127 && green == 0 && blue == 0) {
										obj = Instantiate (buttonPrefab, new Vector3 (x, -.94f, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (-90, 0, 0);
										RandomlyOrientObject (obj);

								} else if (red == 127 && green == 106 && blue == 0) {
										obj = Instantiate (gatePrefab, new Vector3 (x, -1f, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (-90, 0, 0);

								} else if (red == 127 && green == 116 && blue == 63) {
										obj = Instantiate (gatePrefab, new Vector3 (x, -1f, y), Quaternion.identity) as GameObject;
										obj.transform.Rotate (-90, 90, 0);
								}
						}
				}
		}
}