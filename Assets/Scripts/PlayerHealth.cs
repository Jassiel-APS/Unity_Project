using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public int maxHP = 3;
    public float invulnTime = 0.8f;   // tiempo invulnerable tras recibir golpe
    public float knockbackForce = 8f; // empujón al recibir golpe

    public int hp;
    float invulnTimer;
    Rigidbody rb;

    void Awake()
    {
        hp = maxHP;
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int dmg, Vector3 hitFromDirection)
    {
        if (invulnTimer > 0f) return; // aún invulnerable

        hp -= dmg;
        invulnTimer = invulnTime;

        // Knockback (empujón en X/Z + un poco hacia arriba)
        if (rb != null)
        {
            Vector3 push = (transform.position - hitFromDirection).normalized;
            push.y = 0.25f; // levanta tantito
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0); // reset horizontal para empujón consistente
            rb.AddForce(push * knockbackForce, ForceMode.Impulse);
        }

        Debug.Log($"Player HP: {hp}/{maxHP}");

        if (hp <= 0) Die();
    }

    void Die()
    {
        Debug.Log("PLAYER DEAD");
        // TODO: desactivar control, reproducir animación, recargar escena o Game Over
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (invulnTimer > 0f) invulnTimer -= Time.deltaTime;
    }

    public bool IsInvulnerable() => invulnTimer > 0f;
}
