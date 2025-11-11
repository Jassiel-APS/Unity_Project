using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    private PlayerHealth playerHealth;

    private float vidaMaxima;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        vidaMaxima = playerHealth.hp;
    }

    // Update is called once per frame
    void Update()
    {
        rellenoBarraVida.fillAmount = playerHealth.hp / vidaMaxima;
    }
}
