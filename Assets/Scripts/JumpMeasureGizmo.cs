using UnityEngine;

public class JumpMeasureGizmo : MonoBehaviour
{
    public float jumpHeight = 0.93f;
    public float jumpDistance = 1.05f;
    public float doubleJumpHeight = 1.05f;

    private void OnDrawGizmos()
    {
        Collider2D col = GetComponent<Collider2D>();

        if (col == null) return;

        // 🔥 Punto REAL de los pies del jugador
        Vector3 start = new Vector3(
            transform.position.x,
            col.bounds.min.y,
            transform.position.z
        );

        // ================= ALTURA SALTO NORMAL =================
        Gizmos.color = Color.green;

        Gizmos.DrawLine(start, start + Vector3.up * jumpHeight);
        Gizmos.DrawWireSphere(start + Vector3.up * jumpHeight, 0.08f);

        // ================= DISTANCIA HORIZONTAL =================

        // Derecha
        Gizmos.DrawLine(start, start + Vector3.right * jumpDistance);
        Gizmos.DrawWireSphere(start + Vector3.right * jumpDistance, 0.08f);

        // Izquierda 🔥
        Gizmos.DrawLine(start, start + Vector3.left * jumpDistance);
        Gizmos.DrawWireSphere(start + Vector3.left * jumpDistance, 0.08f);

        // ================= DOBLE SALTO =================
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(start, start + Vector3.up * doubleJumpHeight);
        Gizmos.DrawWireSphere(start + Vector3.up * doubleJumpHeight, 0.08f);
    }
}