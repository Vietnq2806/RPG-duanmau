using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float timeDestroy = 0.5f;
    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }


    void Update()
    {
        MoveBullets();
    }
    private void MoveBullets()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
}
