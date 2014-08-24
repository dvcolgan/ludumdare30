using UnityEngine;
using System.Collections;

public class PushableBehavior : MonoBehaviour
{
		protected GameObject otherEntities;
	
		protected void Start ()
		{
				otherEntities = (GameObject)this.transform.parent.gameObject;
		}

		public void MoveToSquare (Vector3 location)
		{
				Vector3 pos = transform.localPosition;
				pos.x = location.x;
				pos.z = location.z;
				transform.localPosition = pos;
		}

		bool SquareIsOnPage (float x, float z)
		{
				return !(x > Constants.PAGE_WIDTH - 1 || x < 0 || z > Constants.PAGE_HEIGHT - 1 || z < 0);
		}
    
		GameObject GetObjectInSquare (float x, float z)
		{
				foreach (Transform child in otherEntities.transform) {
						GameObject obstacle = child.gameObject;
						if (obstacle == gameObject)
								continue;

						float obstacleX = obstacle.transform.localPosition.x;
						float obstacleZ = obstacle.transform.localPosition.z;

						if (obstacleX > x - .4f && obstacleX < x + .4f && obstacleZ > z - .4f && obstacleZ < z + .4f) {
								return obstacle;
						}
				}
        
				return null;
		}

    
		public void PushTo (Vector3 newLocation)
		{
				Vector3 oldLocation = transform.localPosition;

				GameObject otherObject = GetObjectInSquare (newLocation.x, newLocation.z);

				if (SquareIsOnPage (newLocation.x, newLocation.z)) {

						if (otherObject == null || otherObject.GetComponent<CollidableBehavior> () == null) {
								// If the destination square is empty, just move there
								GameObject previousObject = GetObjectInSquare (oldLocation.x, oldLocation.z);
								if (previousObject != null) {
										previousObject.SendMessage ("StepOff", SendMessageOptions.DontRequireReceiver);
								}
								if (otherObject != null) {
										otherObject.SendMessage ("StepOn", SendMessageOptions.DontRequireReceiver);
								}
								MoveToSquare (newLocation);
						} else if (otherObject != null) {
								// Otherwise if that object is pushable, push it instead
								PushableBehavior pushable = otherObject.GetComponent<PushableBehavior> ();
								if (pushable != null) {
										float dx = newLocation.x - oldLocation.x;
										float dz = newLocation.z - oldLocation.z;
										Vector3 otherObjectNewLocation = new Vector3 (newLocation.x + dx, 0, newLocation.z + dz);
										pushable.PushTo (otherObjectNewLocation);
								} else if (otherObject.GetComponent<HoleBehavior> ()) {
										if (GetComponent<FallableBehavior> ()) {

										}
								}
						}
				}
		}
}
