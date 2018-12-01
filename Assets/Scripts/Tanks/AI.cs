using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{
	public float life;
	public float m_Speed = 4f;
	public float m_TurnSpeed = 180f;
	public GameObject target;
	public bool moving;

	private readonly string weapon = "BarrelEnd";
	private Vector3 destination;
	private string m_MovementAxisName;
	private string m_TurnAxisName;
	private Rigidbody m_Rigidbody;
	private float m_MovementInputValue;
	private float m_TurnInputValue;
	private float m_OriginalPitch;
	private PathFinding pathFinding;
	private IEnumerator c;
	private Transform front;

	//Transform front;
	/*
     1. break the code into several scripts
     2. shoot the bullet from the front Gameobject
     3. A way to force a gameobject to have a tag
         
     */

	private void Awake()
	{
		//front = transform.GetChild(2);
		m_Rigidbody = GetComponent<Rigidbody>();
		//m_Rigidbody.isKinematic = true;
		pathFinding = GetComponent<PathFinding>();

		c = FindWay(2.5f);
		StartCoroutine(c);

		Transform[] children = GetComponentsInChildren<Transform>();

		foreach (Transform child in children)
		{
			if (child.name == weapon)
			{
				front = child;
				return;
			}
		}
	}

	private IEnumerator FindWay(float wait)
	{
		yield return new WaitForSeconds(wait);
		destination = transform.position;
		while (true)
		{
			var data = pathFinding.FindPath(transform.position, target.transform.position);

			if (data.path != null)
			{
				destination = data.path.worldPos;
				//Turn();
				moving = true;
			}
			else
			{
				Shoot();
				moving = false;
			}

			yield return new WaitForSeconds(0.5f);
		}
	}


	private void OnEnable()
	{
		m_Rigidbody.isKinematic = false;
		m_MovementInputValue = 0f;
		m_TurnInputValue = 0f;
	}

	private void Shoot()
	{
		print("Shooting");
	}

	private void OnDisable()
	{
		m_Rigidbody.isKinematic = true;
	}


	private void Start()
	{
		m_MovementAxisName = "Vertical";
		m_TurnAxisName = "Horizontal";
	}


	private void FixedUpdate()
	{
		Move();
		Turn();
	}


	public void Move()
	{
		if (moving)
		{
			Vector3 movement = (destination - transform.position).normalized * m_Speed * Time.fixedDeltaTime;
			m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
		}
	}


	public void Turn()
	{
		//Vector3 tankDir = front.transform.position - transform.position;

		//Vector3 moveDir;

		//if (!moving)
		//	moveDir = target.transform.position - transform.position;
		//else
		//	moveDir = destination - transform.position;

		//if ((Vector3.Angle(tankDir, moveDir)) > 20)
		//{
		//	float turn = 0.5f * m_TurnSpeed * Time.deltaTime;

		//	Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

		//	m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
		//}

		//Vector3 rotation = moving ? destination - transform.position : target.transform.position - transform.position;

		//transform.rotation = Quaternion.FromToRotation(Vector3.back, rotation);

	}

	public void Damage(float damage)
	{
		life -= damage;
		if (life < 0)
		{
			Destroy(gameObject, 2);
		}
	}

	//void OnDrawGizmos()
	//{
	//	Gizmos.color = Color.black;
	//	if (!moving)
	//		Gizmos.DrawRay(transform.position, target.transform.position - transform.position);
	//	else
	//		Gizmos.DrawRay(transform.position, destination - transform.position);

	//	Gizmos.color = Color.red;
	//	Gizmos.DrawRay(transform.position, front.transform.position - transform.position);
	//}
}