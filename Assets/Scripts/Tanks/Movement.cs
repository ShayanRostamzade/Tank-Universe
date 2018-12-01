using System.Collections.Generic;
using UnityEngine;

public class Movement : Tank
{

	public MyJoystick joystick;

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
		//transform.rotation.eulerAngles.Set(Mathf.Atan2(transform.position.y, transform.position.z),
		//								   Mathf.Atan2(transform.position.x, transform.position.z),
		//								   Mathf.Atan2(transform.position.x, transform.position.y));
	}


	private void Update()
	{
		m_HorizontalInput = joystick.Coordinates().x;
		m_VerticalInput = joystick.Coordinates().z;
		//EngineSound();
	}

	private void FixedUpdate()
	{
		Move();
		Turn();
	}

	public void Move()
	{
		state = State.Moving;
		Vector3 movement = transform.forward * m_VerticalInput * m_MoveSpeed * Time.fixedDeltaTime;
		rb.MovePosition(rb.position + movement);
	}

	public void Turn()
	{
		state = State.Moving;
		float turn = m_HorizontalInput * m_TurnSpeed * Time.deltaTime;
		Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
		rb.MoveRotation(rb.rotation * turnRotation);
	}

	public void Damage(float damage)
	{
		throw new System.NotImplementedException();
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
