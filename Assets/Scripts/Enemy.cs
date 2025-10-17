using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemyMoveSpeed = 1f;
    protected Player player;
    protected virtual void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    protected void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.transform.position,enemyMoveSpeed*Time.deltaTime);
    }
}
