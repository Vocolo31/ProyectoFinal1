using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{
    public Animator charLife;
    public CheckPoint guardado;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float currentLife = charLife.GetFloat("LifeCharacter");
            charLife.SetFloat("LifeCharacter", currentLife - 0.1f);

            if (currentLife == 0)
            {
                transform.position = guardado.savedPosition; 
            }
        }
    }
}
