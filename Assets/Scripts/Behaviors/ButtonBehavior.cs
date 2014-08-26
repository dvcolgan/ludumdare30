using UnityEngine;
using System.Collections;

public class ButtonBehavior : MonoBehaviour
{

		void StepOff ()
		{
				foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag ("LevelObject")) {
						if (obstacle.GetComponent<GateBehavior> ()) {
								obstacle.SendMessage ("Close");

						}
				}
				Vector3 pos = transform.position;
				pos.y += 0.09f;
				transform.position = pos;
		}

		void StepOn ()
		{
				foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag ("LevelObject")) {
						if (obstacle.GetComponent<GateBehavior> ()) {
								obstacle.SendMessage ("Open");
								
						}
				}
				Vector3 pos = transform.position;
				pos.y -= 0.09f;
				transform.position = pos;
		}
}
