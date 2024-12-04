using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f; // Velocidad del enemigo
    public enum EnemyType { Fast, Slow, Zigzag }
    public EnemyType type;

    void Update()
    {
        switch (type)
        {
            case EnemyType.Fast:
                Move(-speed * 2f); // Enemigo rápido
                break;
            case EnemyType.Slow:
                Move(-speed * 0.5f); // Enemigo lento
                break;
            case EnemyType.Zigzag:
                float zigzag = Mathf.Sin(Time.time * speed) * 2f; // Movimiento lateral
                transform.Translate(zigzag * Time.deltaTime, 0, -speed * Time.deltaTime, Space.World);
                break;
            default:
                Move(-speed); // Movimiento base
                break;
        }

        DestroyEnemy();
    }

    private void Move(float zSpeed)
    {
        // Movimiento lineal hacia -Z
        transform.Translate(0, 0, zSpeed * Time.deltaTime, Space.World);
    }

    private void DestroyEnemy()
    {
        // Destruir cuando el enemigo pasa más allá del jugador
        if (transform.position.z < -10f) // Ajustar el valor según la posición del jugador
        {
            GameManager.instance.ScoreUp();
            Destroy(gameObject);
        }
    }
}
