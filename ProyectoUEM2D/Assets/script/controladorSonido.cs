using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorSonido : MonoBehaviour
{
    public TopDownMovement dash;
    public AudioSource sonido;
    private void Start()
    {
        sonido = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (dash.dashing == true)
        {
            sonido.Play();
        }
        if (dash.dashing == false)
        {
            sonido.Stop();
        }
    }
}
