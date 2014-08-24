/*
using UnityEngine;
using System.IO;
using System.Collections;

public class BitmapImporter : MonoBehaviour
{
	
		public GameObject player;
		public GameObject playerLight;
		public GameObject floor;
		public GameObject fire;
		public GameObject camera;
		public GameObject backWall;
		public GameObject levelHolder;
		public TextAsset levelText;
		public GameObject textHolder;
		public GameObject exit;
	
		public Texture2D level;
	
		// Use this for initialization
		void Start ()
		{
				var height = level.height;
				var width = level.width;
		
				string[] lines = levelText.text.Split ("\n" [0]);
				int fileLineCount = 0;
		
				GameObject levelHolderObject = Instantiate (levelHolder) as GameObject;
				levelHolderObject.name = "LevelHolder";
		
				for (var x = 0; x < width; x++) {
						for (var y = 0; y < height; y++) {
								var p = level.GetPixel (x, y);
								var r = p.r;
								var g = p.g;
								var b = p.b;
								var a = p.a;
								if (g == 1 && r + b == 0) {
										Instantiate (player, new Vector3 (x, y, 0), Quaternion.identity).name = "Player";
								} else if (r == 1 && g + b == 0) {
										GameObject textHolderObject = Instantiate (textHolder, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
										textHolderObject.name = "TextHolder-" + x + "," + y;
										textHolderObject.GetComponent<TextScript> ().setText (lines [fileLineCount]);
										fileLineCount++;
								} else if (b == 1 && r + g == 0) {
										GameObject exitPiece = Instantiate (exit, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
										exit.name = "Exit";
								} else if (r * g == 1 && b == 0) {
										GameObject firePiece = Instantiate (fire, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
										firePiece.name = "fire_" + x + "/" + y;
								} else if (r * g * b == 0) {
										GameObject floorPiece = Instantiate (floor, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
										floorPiece.name = "floor_" + x + "/" + y;
										floorPiece.transform.parent = levelHolderObject.transform;
								}
								GameObject wallPiece = Instantiate (backWall, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
								wallPiece.name = "wall_" + x + "/" + y;
								wallPiece.transform.parent = levelHolderObject.transform;
						}
				}
	
				Object playerLightObject = Instantiate (playerLight);
				playerLightObject.name = "FollowLight";
		
				Object cameraObject = Instantiate (camera);
				cameraObject.name = "MainCamera";
		
		}

		void Update ()
		{
				if (Input.GetKeyDown ("r")) {
						Application.LoadLevel (Application.loadedLevel);
				}
		}
}
*/