using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement3D_Min : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 10f;

    private Rigidbody rb;
    private bool enSuelo;
    private Vector2 movementInput;
    private bool jumpPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Obtener input de movimiento (solo horizontal)
        if (Keyboard.current != null)
        {
            // Solo procesamos las teclas A y D para movimiento lateral
            float movimientoHorizontal = (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0);
            movementInput = new Vector2(movimientoHorizontal, 0f);

            // Detectar salto
            if (Keyboard.current.spaceKey.wasPressedThisFrame && enSuelo)
            {
                jumpPressed = true;
            }
        }

        // Aplicar movimiento (solo en el eje X)
        Vector3 movimiento = new Vector3(movementInput.x, 0f, 0f) * velocidadMovimiento;
        rb.AddForce(movimiento);

        // Aplicar salto
        if (jumpPressed && enSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            enSuelo = false;
            jumpPressed = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el jugador está en el suelo
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
        }
    }
}