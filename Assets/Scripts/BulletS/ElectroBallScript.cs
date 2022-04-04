using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroBallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTime = 5f;
    public float speed = 5f;

    public float paralyseTime;
    public GameObject target;

    // Update is called once per frame
    void FixedUpdate()
    {
        var gm = GameManager.Instance;
        lifeTime -= Time.deltaTime * gm.GameSpeed;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
            return;
        }
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * speed * gm.GameSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CrabScript>(out CrabScript cs))
        {
            if (cs.gameObject == target)
            {
                cs.Paralyse(paralyseTime);
                Destroy(gameObject);
            }
        }
    }
}
