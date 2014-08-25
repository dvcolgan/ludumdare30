using UnityEngine;
using System.Collections;


public enum LogState
{
		Horizontal,
		Vertical,
		Upright}
;

public class LogBehavior : MonoBehaviour
{
		public LogState state;
		GameObject logCylinder;

		void Awake ()
		{
				logCylinder = transform.GetChild (0).gameObject;
				Debug.Log (logCylinder);
		}

		public void OrientLogHorizontally ()
		{
				logCylinder.transform.localEulerAngles = new Vector3 (0, 90, 0);
				state = LogState.Horizontal;
		}

		public void OrientLogVertically ()
		{
				logCylinder.transform.localEulerAngles = new Vector3 (0, 0, 0);
				state = LogState.Vertical;
		}

		public void OrientLogUpright ()
		{
				logCylinder.transform.localEulerAngles = new Vector3 (90, 0, 0);
				state = LogState.Upright;
		}
}
