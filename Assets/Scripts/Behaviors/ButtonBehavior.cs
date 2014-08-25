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

				iTween.MoveBy (gameObject, iTween.Hash ("z", 0.09f, "easeType", "easeOut", "time", .1f));
		}

		void StepOn ()
		{
				foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag ("LevelObject")) {
						if (obstacle.GetComponent<GateBehavior> ()) {
								obstacle.SendMessage ("Open");
								
						}
				}
				iTween.MoveBy (gameObject, iTween.Hash ("z", -0.09f, "easeType", "easeOut", "time", .1f));
		}
}
