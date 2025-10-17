using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float timeDestroy = 0.5f;
<<<<<<< HEAD

    void Start() => Destroy(gameObject, timeDestroy);

    void Update() => transform.Translate(moveSpeed * Time.deltaTime * Vector2.right, Space.Self);
}
=======
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
>>>>>>> 061993557d81537aa292318782a916b84d3e9f87
