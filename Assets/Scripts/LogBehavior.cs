using UnityEngine;
using System.Collections;


public enum LogState
{
		Horizontal,
		Vertical,
		Upright}
;

public class LogBehavior : MoveableObject
{
		public LogState state;
		GameObject logCylinder;

		void Start ()
		{
				base.Start ();
				logCylinder = transform.GetChild (0).gameObject;
				OrientLogForState ();
		}

		void OrientLogForState ()
		{
				if (state == LogState.Horizontal) {
						OrientLogHorizontally ();
				} else if (state == LogState.Vertical) {
						OrientLogVertically ();
				} else if (state == LogState.Upright) {
						OrientLogUpright ();
				}
		}

		void OrientLogHorizontally ()
		{
				logCylinder.transform.localEulerAngles = new Vector3 (0, 0, 0);
				state = LogState.Horizontal;
		}

		void OrientLogVertically ()
		{
				logCylinder.transform.localEulerAngles = new Vector3 (0, 90, 0);
				state = LogState.Vertical;
		}

		void OrientLogUpright ()
		{
				logCylinder.transform.localEulerAngles = new Vector3 (90, 0, 0);
				state = LogState.Upright;
		}

		void PushTo (Vector3 location)
		{
				if (CanMoveIntoSquare (location.x, location.z)) {
						float dx = location.x - transform.position.x;
						float dz = location.z - transform.position.z;

						bool isPushingHorizontally = location.x > transform.position.x || location.x < transform.position.x;
						bool isPushingVertically = location.z > transform.position.z || location.z < transform.position.z;

						if (state == LogState.Upright) {
								if (isPushingHorizontally) {
										OrientLogVertically ();
								} else if (isPushingVertically) {
										OrientLogHorizontally ();
								}
						} else if (state == LogState.Horizontal) {
								if (isPushingVertically) {
										OrientLogUpright ();
								}
						} else if (state == LogState.Vertical) {
								if (isPushingHorizontally) {
										OrientLogUpright ();
								}
						}
			
						MoveToSquare (location);
						if (isPushingHorizontally && state == LogState.Horizontal || isPushingVertically && state == LogState.Vertical) {
								Vector3 nextLocation = new Vector3 (location.x + dx, 0, location.z + dz);
								PushTo (nextLocation);
						}
				}
		}
}
