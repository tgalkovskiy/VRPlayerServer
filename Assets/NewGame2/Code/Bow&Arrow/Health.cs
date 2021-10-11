using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int life = 50;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (life <= 0)
            {
                Die();
            }
        }
    }

    public void Damage(int hit)
    {
        life -= hit;
    }

    public void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
}
