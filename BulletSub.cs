using UnityEngine;

public class BulletSub : MonoBehaviour
{
    float bulletSpeed, bulletMass;
    public int shotColor;
    public bool r, b, g;

    

    private void Start()
    {
        bulletSpeed = 8f; //too much force and the bullet goes thru objects ='(
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        this.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D target = collision.collider;
        if (target.gameObject.GetComponent<ZombieSub>() != null)
        {
            if (r == true)
            {
                target.gameObject.GetComponent<ZombieSub>().r =
                    !target.gameObject.GetComponent<ZombieSub>().r;
            }
            if (g == true)
            {
                target.gameObject.GetComponent<ZombieSub>().g =
                    !target.gameObject.GetComponent<ZombieSub>().g;
            }
            if (b == true)
            {
                target.gameObject.GetComponent<ZombieSub>().b =
                    !target.gameObject.GetComponent<ZombieSub>().b;
            }
            target.gameObject.GetComponent<ZombieSub>().hit = true;
        }
        if (target.gameObject.GetComponent<SpawnSub>() != null)
        {
            target.gameObject.GetComponent<SpawnSub>().TakeDamage();
        }
        //Debug.Log(target);
        Destroy(gameObject, .01f);
    }
}

/*
// homing bullets?
// lvl 1 closest
// lvl 2 closest same color
//

burst fire (3 shots, pause, repeat)
explosions
lazer beam
shotgun
shield for melee
cone shape aoe



    */