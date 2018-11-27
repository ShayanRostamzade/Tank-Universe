using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
	public float radius;
	public float G;
	Rigidbody rb;
    public Mesh planet;
    //public Vector3 tank;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}
    private void Start()
    {
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    void FixedUpdate()
	{
        //Projectile[] projectile = FindObjectsOfType <GameObject.FindWithTag(projectile)>();

        //TankAction[] tankMovements = FindObjectsOfType<TankAction>();

        //foreach (Bullet bullet in bullets)
        //{
        //	Attract(bullet.gameObject);
        //}

        //foreach (TankAction tankMovement in tankMovements)
        //{
        //	Attract(tankMovement.gameObject);
        //}
        Movement[] movements = FindObjectsOfType<Movement>();
        print(movements.Length);
        foreach(Movement m in movements)
        {
            Attract(m.gameObject);
        }
	}

	void Attract(GameObject attract)
	{
		Vector3 direction = transform.position - attract.transform.position;

        float distance = direction.magnitude;

		float force = G * (rb.mass * attract.GetComponent<Rigidbody>().mass) / Mathf.Pow(distance, 2);

		attract.GetComponent<Rigidbody>().AddForce(direction * force);

        //Quaternion targeRotation = Quaternion.FromToRotation(tank.up)
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireMesh(planet);
    }
}
