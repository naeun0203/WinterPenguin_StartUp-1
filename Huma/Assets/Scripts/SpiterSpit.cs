using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiterSpit : MonoBehaviour
{
    public float Speed = 1f;
    public float Damage = 1.0f;
    private Transform PlayerTr;
    Rigidbody rigd;
    Poolable poolable;
    void Start()
    {
        PlayerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        poolable = this.gameObject.GetComponent<Poolable>();
        rigd = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        Vector3 direction = PlayerTr.position - transform.position;
        //transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        //rigd.AddForce(transform.forward * Speed);
        //rigd.velocity = direction.normalized * Speed;
        //rigd.velocity = transform.forward * Speed;
        //transform.Translate(direction);
    }

    private void OnBecameInvisible()
    {
        poolable.Push();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "wall")
        {
            poolable.Push();
        }
        else if (collision.collider.tag == "Player")
        {
            poolable.Push();
        }
    }
}

