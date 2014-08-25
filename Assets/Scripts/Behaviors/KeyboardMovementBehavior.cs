using UnityEngine;
using System.Collections;

public class KeyboardMovementBehavior : MonoBehaviour
{
		void Update ()
		{
				if (!GameController.instance.playerIsMoving) {
						if (transform.localPosition.x == Constants.PAGE_WIDTH - 1 && Input.GetKey (KeyCode.RightArrow)) {
								GameController.instance.LoadNextLevel ();
						} else {
								float newX = transform.localPosition.x;
								float newZ = transform.localPosition.z;
								bool keyPressed = false;
								if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)) {
										keyPressed = true;
								}
			
								if (Input.GetKey (KeyCode.UpArrow)) {
										newZ += 1f;
										iTween.RotateTo (gameObject, iTween.Hash ("y", 270, "easeType", "easeInOut", "time", .3f));
								} else if (Input.GetKey (KeyCode.DownArrow)) {
										newZ -= 1f;
										iTween.RotateTo (gameObject, iTween.Hash ("y", 90, "easeType", "easeInOut", "time", .3f));
								} else if (Input.GetKey (KeyCode.LeftArrow)) {
										newX -= 1f;
										iTween.RotateTo (gameObject, iTween.Hash ("y", 180, "easeType", "easeInOut", "time", .3f));
								} else if (Input.GetKey (KeyCode.RightArrow)) {
										newX += 1f;
										iTween.RotateTo (gameObject, iTween.Hash ("y", 0f, "easeType", "easeInOut", "time", .3f));
								}
			
								if (keyPressed) {
										gameObject.GetComponent<PushableBehavior> ().PushTo (new Vector3 (newX, 0, newZ));
								}
						}
				}
		}
}
