using UnityEngine;
using System.Collections;

public class PlayerController : MoveableObject
{


		// Update is called once per frame
		void Update ()
		{
				float newX = transform.position.x;
				float newZ = transform.position.z;
				float pushX = newX;
				float pushZ = newZ;
				bool keyPressed = false;
				if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.RightArrow)) {
						keyPressed = true;
				}
				
				if (Input.GetKeyDown (KeyCode.UpArrow)) {
						newZ += 1f;
						pushZ = newZ + 1f;
				} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
						newZ -= 1f;
						pushZ = newZ - 1f;
				} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
						newX -= 1f;
						pushX = newX - 1f;
				} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
						newX += 1f;
						pushX = newX + 1f;
				}

				if (keyPressed) {
						if (CanMoveIntoSquare (newX, newZ)) {
								MoveToSquare (new Vector3 (newX, 0, newZ));
						} else {
								foreach (Transform child in otherEntities.transform) {
										GameObject obstacle = child.gameObject;
										if (obstacle.gameObject.transform.position.x == newX && obstacle.gameObject.transform.position.z == newZ) {
												obstacle.SendMessage ("PushTo", new Vector3 (pushX, 0, pushZ), SendMessageOptions.DontRequireReceiver);
										}
								}
						}
				}
		}
}
