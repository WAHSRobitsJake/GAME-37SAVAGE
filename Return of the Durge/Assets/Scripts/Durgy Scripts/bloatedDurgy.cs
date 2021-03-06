using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloatedDurgy : MonoBehaviour
{
    public int health = 1;
    public float randNum;
    public AudioSource bloaterSource;
    public AudioClip bloaterShoot, bloatersDeath;
    public GameObject swarmDurgies;
    public GameObject deathSoundObject;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
           for (int i = 0; i < 4; i++)
            {
                randNum = Random.Range(-1.0f, 1.0f);
                Instantiate(swarmDurgies, this.gameObject.transform.position, Quaternion.identity);
                swarmDurgies.GetComponent<Rigidbody2D>().AddForce(new Vector2(randNum, 15.0f));
            }
            GameObject deathSound = Instantiate(deathSoundObject, this.gameObject.transform.position, Quaternion.identity);
            Destroy(deathSound, 0.7f);

            Destroy(this.gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            health--;
        }
        if (other.gameObject.CompareTag("pBullet"))
        {
            health--;
        }
    }
 
}
