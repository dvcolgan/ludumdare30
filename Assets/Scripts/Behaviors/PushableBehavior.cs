using UnityEngine;
using System.Collections;

public class PushableBehavior : MonoBehaviour
{
	
		void Start ()
		{
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

		GameObject GetObjectInSquare (float x, float z, bool collidable)
		{
				foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag ("LevelObject")) {
						if (obstacle == gameObject)
								continue;

						float obstacleX = obstacle.transform.localPosition.x;
						float obstacleZ = obstacle.transform.localPosition.z;

						if (obstacleX > x - .4f && obstacleX < x + .4f && obstacleZ > z - .4f && obstacleZ < z + .4f) {
								bool hasCollidable = !!obstacle.GetComponent<CollidableBehavior> ();
								if (hasCollidable == collidable) {
										return obstacle;
								}
						}
				}
				return null;
		}
	
		public void PushTo (Vector3 newLocation)
		{
				Vector3 oldLocation = transform.localPosition;

				GameObject otherObject = GetObjectInSquare (newLocation.x, newLocation.z, true);
				GameObject otherFloor = GetObjectInSquare (newLocation.x, newLocation.z, false);

				if (SquareIsOnPage (newLocation.x, newLocation.z)) {

						if (otherObject == null) {
								// If the destination has no collidable object in it, just move there
								GameObject previousFloor = GetObjectInSquare (oldLocation.x, oldLocation.z, false);
								if (previousFloor != null) {
										previousFloor.SendMessage ("StepOff", SendMessageOptions.DontRequireReceiver);
								}
								if (otherFloor != null) {
										otherFloor.SendMessage ("StepOn", SendMessageOptions.DontRequireReceiver);
								}
								MoveToSquare (newLocation);

								if (otherFloor && otherFloor.GetComponent<SlipperyBehavior> ()) {
										float dx = newLocation.x - oldLocation.x;
										float dz = newLocation.z - oldLocation.z;
										Vector3 otherObjectNewLocation = new Vector3 (newLocation.x + dx, 0, newLocation.z + dz);
										PushTo (otherObjectNewLocation);

								} else if (GetComponent<LogBehavior> ()) {
										LogBehavior log = GetComponent<LogBehavior> ();
										float dx = oldLocation.x - newLocation.x;
										float dz = oldLocation.z - newLocation.z;
						
										bool isPushingVertically = oldLocation.x > newLocation.x || oldLocation.x < newLocation.x;
										bool isPushingHorizontally = oldLocation.z > newLocation.z || oldLocation.z < newLocation.z;
                    
										if (log.state == LogState.Upright) {
												if (isPushingHorizontally) {
														log.OrientLogVertically ();
												} else if (isPushingVertically) {
														log.OrientLogHorizontally ();
												}
										} else if (log.state == LogState.Horizontal) {
												if (isPushingVertically) {
														log.OrientLogUpright ();
												}
										} else if (log.state == LogState.Vertical) {
												if (isPushingHorizontally) {
														log.OrientLogUpright ();
												}
										}
										
										if (isPushingHorizontally && log.state == LogState.Horizontal || isPushingVertically && log.state == LogState.Vertical) {
												Vector3 nextLocation = new Vector3 (newLocation.x - dx, 0, newLocation.z - dz);
												PushTo (nextLocation);
										}
								}
                
						} else {
								// Otherwise if that object is pushable, push it instead
								PushableBehavior pushable = otherObject.GetComponent<PushableBehavior> ();
								if (pushable != null) {
										if (GetComponent<PusherBehavior> ()) {
												float dx = newLocation.x - oldLocation.x;
												float dz = newLocation.z - oldLocation.z;
												Vector3 otherObjectNewLocation = new Vector3 (newLocation.x + dx, 0, newLocation.z + dz);
												pushable.PushTo (otherObjectNewLocation);
												GameObject potentiallyMovedObject = GetObjectInSquare (newLocation.x, newLocation.z, true);
												if (potentiallyMovedObject == null) {
														PushTo (newLocation);
												}
										}
										
								} else if (otherObject.GetComponent<HoleBehavior> ()) {
										if (GetComponent<FallableBehavior> ()) {
												otherObject.SendMessage ("Fill");
												Destroy (gameObject);
												MoveToSquare (newLocation);
										}
								}
						}
				}
		}
}
