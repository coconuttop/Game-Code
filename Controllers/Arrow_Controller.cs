using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Controller : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private string targetLayerName = "Player";

    [SerializeField] private float xVelocity;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private bool canMove;
    [SerializeField] private bool flipped;

    private CharacterStats myStats;
    private int facingDir;

    private void Start()
    {
        if (facingDir == -1)
            Flip();
    }

    private void Update()
    {
        if(canMove)
            rb.velocity = new Vector2(xVelocity * facingDir, rb.velocity.y);
    }

    public void SetupArrow(float _speed, CharacterStats _myStats, int _facingDir)
    {
        xVelocity = _speed;
        myStats = _myStats;
        facingDir = _facingDir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {
            //collision.GetComponent<CharacterStats>()?.TakeDamage(damage);
            myStats.DoDamage(collision.GetComponent<CharacterStats>());

            StuckInto(collision);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            StuckInto(collision);
    }

    private void StuckInto(Collider2D collision)
    {
        GetComponentInChildren<ParticleSystem>().Stop();
        GetComponent<CapsuleCollider2D>().enabled = false;
        canMove = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;

        Destroy(gameObject, 5);
    }

    public void FlipArrow()
    {
        if (flipped)
            return;

        Flip();
        flipped = true;
        targetLayerName = "Enemy";
    }

    private void Flip()
    {
        xVelocity = xVelocity * -1;
        transform.Rotate(0, 180, 0);
    }
}
