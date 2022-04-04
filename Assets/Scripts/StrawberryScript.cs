using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberryScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CrabScript>(out CrabScript cs))
            GameManager.Instance.End();
    }
}
