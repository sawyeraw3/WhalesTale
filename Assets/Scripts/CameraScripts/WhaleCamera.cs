using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Cameras
{
	public class WhaleCamera : FreeLookCam
	{
		protected override void Start() {
			base.Start ();
			if(m_Target != null)
				m_Target.GetComponent<WhaleMove> ().moveSpeed = m_MoveSpeed * 1.5f;
		}

		protected override void FollowTarget(float deltaTime)
		{
			if (m_Target == null)
				return;
			transform.position += m_Target.transform.forward * m_MoveSpeed * deltaTime;
		}
	}
}