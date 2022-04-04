using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTime = 5f;
    public float speed = 5f;
    public int dmg = 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        var gm = GameManager.Instance;
        lifeTime -= Time.deltaTime * gm.GameSpeed;
        if (lifeTime < 0)
        {
            Destroy(this.gameObject);
            return;
        }
        transform.position +=
            transform.up * Time.deltaTime * gm.GameSpeed * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<CrabScript>(out CrabScript cs))
        {
            SoundManager.Instance.PlayHit();
            cs.HP -= 1;
            Destroy(gameObject);
        }
    }
}
