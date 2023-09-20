using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PhotonView photonView;

    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float lifeTime = 3f;

    void Start()
    {
        StartCoroutine(BulletFly());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.velocity = transform.right * speed * Time.fixedDeltaTime;
        transform.position += transform.right * speed * Time.deltaTime;
    }

    //private void OnTriggerEnter(Collider other)
    //{

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            return;
        }

        if (collision.tag == "Player")
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.GetDamage(damage);
            }
        }
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);

        }
    }

    private IEnumerator BulletFly()
    {
        yield return new WaitForSeconds(lifeTime);

        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);

        }
    }
}
