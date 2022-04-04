using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabScript : MonoBehaviour
{
    public float speed;
    public int meat;
    public GameObject strawberry;

    private float paralyseTime, slowTime;
    private float slowAmount;

    public bool paralyseImmunity = false;
    SpriteRenderer sr;

    [SerializeField]
    private Color freezeColor, paralyseColor;
    [SerializeField]
    private int hP;
    public bool IsDead { private set; get; }

    private GameManager gm;

    private int scoreWorth;

    public void Slow(float time,float amount)
    {
        if (slowTime <= 0)
        {
            slowAmount = amount;
            slowTime = time;
        }
        sr.color = freezeColor;
    }

    public void Paralyse(float time)
    {
        if (paralyseTime > 0)
            HP--;
        else
        {
            paralyseTime = time;
            sr.color = paralyseColor;
        }
    }

    public int HP {
        get => hP;
        set{
            hP = value;
            if (hP <= 0)
                IsDead=true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        IsDead = false;
        gm = GameManager.Instance;
        strawberry = gm.Strawberry;
        scoreWorth = HP * meat;
    }

    private void Update()
    {
        if(IsDead)
            Die();
        slowTime -= Time.deltaTime * gm.GameSpeed;
        paralyseTime -= Time.deltaTime * gm.GameSpeed;
        if (sr.color != Color.white && paralyseTime <= 0 && slowTime <= 0)
            sr.color = Color.white;
        if (slowTime <= 0)
            slowAmount = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gm.IsRunning || (paralyseTime>0 && !paralyseImmunity))
            return;
        transform.position +=
            (strawberry.transform.position - transform.position).normalized 
            * speed * Time.deltaTime * gm.GameSpeed * (1f-slowAmount);
    }

    private void Die()
    {
        gm.AddKilledCrab();
        gm.Money += meat;
        gm.Score += scoreWorth;
        Destroy(gameObject);
    }
}
