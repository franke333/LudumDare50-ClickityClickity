using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultBall : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 target;
    public float speed = 2f;
    public float range;
    public int dmg = 1;

    public GameObject anim;

    // Update is called once per frame
    void FixedUpdate()
    {
        var gm = GameManager.Instance;
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * Time.deltaTime * gm.GameSpeed * speed;
        if (direction.magnitude < 0.05f || (target - transform.position).normalized == - direction)
        {
            Boom();
        }
    }

    private void Boom()
    {
        SoundManager.Instance.PlayExplosion();
        foreach (var cs in FindObjectsOfType<CrabScript>())
            if ((cs.transform.position - transform.position).magnitude <= range)
                cs.HP -= dmg;
        var a = Instantiate(anim, transform.position, Quaternion.identity);
        a.transform.localScale *= range;
        Destroy(a, 0.40f);
        Destroy(gameObject);

    }
}
