using UnityEngine;
using System.Collections;

public class KeyboardMovementBehavior : MonoBehaviour
{
		void Update ()
		{
				if (!GameController.instance.playerIsMoving) {
						if (transform.localPosition.x == Constants.PAGE_WIDTH - 1 && Input.GetKeyDown (KeyCode.RightArrow)) {
								GameController.instance.LoadNextLevel ();
						} else {
								float newX = transform.localPosition.x;
								float newZ = transform.localPosition.z;
								bool keyPressed = false;
								if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.RightArrow)) {
										keyPressed = true;
								}
			
								if (Input.GetKeyDown (KeyCode.UpArrow)) {
										newZ += 1f;
										iTween.RotateTo (gameObject, iTween.Hash ("y", 270, "easeType", "easeInOut", "time", .3f));
								} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
										newZ -= 1f;
										iTween.RotateTo (gameObject, iTween.Hash ("y", 90, "easeType", "easeInOut", "time", .3f));
								} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
										newX -= 1f;
										iTween.RotateTo (gameObject, iTween.Hash ("y", 180, "easeType", "easeInOut", "time", .3f));
								} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
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
