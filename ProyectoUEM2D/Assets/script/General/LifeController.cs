using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public Animator charLife;
    public Animator charLife2;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float currentLife = charLife.GetFloat("LifeCharacter");
            charLife.SetFloat("LifeCharacter", currentLife - 0.1f);
        }
    }
}
