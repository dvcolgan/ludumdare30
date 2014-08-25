
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
				
		
		}

		void Update ()
		{
				if (Input.GetKeyDown ("r")) {
						Application.LoadLevel (Application.loadedLevel);
				}
		}
}