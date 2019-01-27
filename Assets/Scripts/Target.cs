
using UnityEngine;

public class Target : MonoBehaviour {

    public float health;
    public Material critical;

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if(health <= 0f)
        {
            Die();
        }else if(health <= 50f)
        {
            changeMat();
        }
        
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void changeMat()
    {
        GetComponent<Renderer>().material = critical;
    }
}
