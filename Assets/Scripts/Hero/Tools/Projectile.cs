using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    public Rigidbody rigidBody;

    public Vector3 direction;
    public float power;
    public LayerMask collisionLayerMask;
    public LayerMask hitLayerMask;
    public float deathDelay;
    public GameObject visual;

    public bool isShot = false;

    private void Update()
    {
        if(isShot)
        visual.transform.Rotate(rigidBody.angularVelocity);
    }

    public void SetupProjectile(Vector3 shootDirection, float shootPower)
    {
        direction = shootDirection;
        power = shootPower;
        rigidBody.AddForce(direction.normalized*power);
        isShot = true;
        rigidBody.useGravity = true;

        StartCoroutine(DeathDelay());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isShot)
        {
            if (other.gameObject.layer == collisionLayerMask)
            {
                Debug.Log("Hit " + other.gameObject.name + " in position " + other.gameObject.transform.position);
                this.gameObject.SetActive(false);
            }

            if (other.gameObject.layer == hitLayerMask)
            {
                Debug.Log("Hit " + other.gameObject.name + " in position " + other.gameObject.transform.position);
                this.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(this.gameObject);
    }
}
