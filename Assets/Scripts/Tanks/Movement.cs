using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour
{

	private float m_HorizontalInput;
	private float m_VerticalInput;

	public AudioSource m_AudioSource;
	public AudioClip m_audioClip;

	public float m_TurnSpeed = 40f;
	public float m_MoveSpeed = 30f;

	private Rigidbody rb;

	private enum State { Idealing, Moving }

	State state;


	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		m_AudioSource = GetComponent<AudioSource>();
		state = State.Idealing;
	}


	private void Update()
	{
		m_HorizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
		m_VerticalInput = CrossPlatformInputManager.GetAxis("Vertical");
		EngineSound();
	}

	private void FixedUpdate()
	{
		Move();
		Turn();
	}

	private void Move()
	{
		state = State.Moving;
		Vector3 movement = transform.forward * m_VerticalInput * m_MoveSpeed * Time.deltaTime;
		rb.MovePosition(rb.position + movement);
	}

	private void Turn()
	{
		state = State.Moving;
		float turn = m_HorizontalInput * m_TurnSpeed * Time.deltaTime;
		Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
		rb.MoveRotation(rb.rotation * turnRotation);
	}

	private void EngineSound()
	{
		if (Mathf.Abs(m_VerticalInput) > 0.0005f && Mathf.Abs(m_HorizontalInput) > 0.0005f)
		{
			if (!m_AudioSource.isPlaying)
			{
				m_AudioSource.clip = m_audioClip;
				m_AudioSource.Play();
			}
		}
		else
		{
			m_AudioSource.Stop();
		}


	}
}
