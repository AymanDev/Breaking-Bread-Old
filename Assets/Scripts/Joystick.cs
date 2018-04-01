using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.CrossPlatformInput.PlatformSpecific;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {

	public enum AxisOption {
		// Options for which axes to use
		Both,
		// Use both
		OnlyHorizontal,
		// Only horizontal
		OnlyVertical
		// Only vertical
	}

	public int MovementRange = 100;
	public AxisOption axesToUse = AxisOption.Both;

	Vector3 m_StartPos;
	public bool m_UseX;
	public bool m_UseY;
	public float horizontal;
	public float vertical;

	void OnEnable () {
		CreateVirtualAxes ();
	}

	void Start () {
		m_StartPos = transform.position;
	}

	void UpdateVirtualAxes (Vector3 value) {
		PlayerController playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		var delta = m_StartPos - value;
		delta.y = -delta.y;
		delta /= MovementRange;

		if (m_UseX) {
			horizontal = -delta.x;
		}

		if (m_UseY) {
			vertical = delta.y;
		}
	}

	void CreateVirtualAxes () {
		m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
		m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);
	}


	public void OnDrag (PointerEventData data) {
		Vector3 newPos = Vector3.zero;

		if (m_UseX) {
			int delta = (int)(data.position.x - m_StartPos.x);
			delta = Mathf.Clamp (delta, -MovementRange, MovementRange);
			newPos.x = delta;
		}

		if (m_UseY) {
			int delta = (int)(data.position.y - m_StartPos.y);
			delta = Mathf.Clamp (delta, -MovementRange, MovementRange);
			newPos.y = delta;
		}
		transform.position = new Vector3 (m_StartPos.x + newPos.x, m_StartPos.y + newPos.y, m_StartPos.z + newPos.z);
		UpdateVirtualAxes (transform.position);
	}


	public void OnPointerUp (PointerEventData data) {
		transform.position = m_StartPos;
		UpdateVirtualAxes (m_StartPos);
	}

	public void OnPointerDown (PointerEventData data) {
	}
}
