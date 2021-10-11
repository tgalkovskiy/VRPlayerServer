using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float m_Speed = 2000.0f;

    public int damage;

    public Transform m_Tip = null;

    private Rigidbody m_Rigid = null;
    private bool m_IsStopped = true;
    private Vector3 m_LastPosition = Vector3.zero;

    public LayerMask LayersToStop;

    private void Awake()
    {
        m_Rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (m_IsStopped)
            return;

        // Rotate
        m_Rigid.MoveRotation(Quaternion.LookRotation(m_Rigid.velocity, transform.up));

        // Collision
        if (Physics.Linecast(m_LastPosition, m_Tip.position, LayersToStop))
        {
            Stop();
        }

        // Store position
        m_LastPosition = m_Tip.position;
    }


    private void Stop()
    {

        m_IsStopped = true;

        m_Rigid.isKinematic = true;
        m_Rigid.useGravity = false;


    }

    public void Fire(float pullValue)
    {

        m_IsStopped = false;
        transform.parent = null;

        m_Rigid.isKinematic = false;
        m_Rigid.useGravity = true;
        m_Rigid.AddForce(transform.forward * (pullValue * m_Speed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().Damage(damage);
            gameObject.transform.parent = collision.gameObject.transform;
        }
    }
}
