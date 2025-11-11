using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemySimpleMove : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 3f;            // Velocidad de movimiento
    public float moveDistance = 3f;     // Distancia máxima desde el punto inicial
    public bool startRight = true;      // Dirección inicial (true = derecha)
    public bool flipVisual = true;      // Si quieres que se voltee visualmente

    private Vector3 startPos;
    private int direction;              // 1 = derecha, -1 = izquierda

    void Start()
    {
        startPos = transform.position;
        direction = startRight ? 1 : -1;
        if (flipVisual) SetFacing(direction);
    }

    void Update()
    {
        // Movimiento lateral simple (en espacio mundial)
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime, Space.World);

        // Cambiar dirección si alcanza el límite de distancia
        if (Mathf.Abs(transform.position.x - startPos.x) >= moveDistance)
        {
            ReverseDirection();
        }
    }

    void ReverseDirection()
    {
        direction *= -1;
        if (flipVisual) SetFacing(direction);
    }

    void SetFacing(int d)
    {
        transform.rotation = Quaternion.Euler(0f, d == 1 ? 0f : 180f, 0f);
    }

    // Cambia de dirección si choca con algo que tenga tag (y no esté Untagged)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
            return; // ignora objetos sin tag

        if (collision.gameObject.CompareTag("Suelo"))
            return; // ignora suelo

        ReverseDirection();
    }

    // También sirve si usas colliders tipo trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Untagged"))
            return;

        if (other.CompareTag("Suelo"))
            return;

        ReverseDirection();
    }
}
