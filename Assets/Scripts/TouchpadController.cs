using UnityEngine;

public class TouchpadController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Vector2 startPosition;
    private Camera camera;
    private float targetPosition;

    private void Start()
    {
        camera = GetComponent<Camera>();
        targetPosition = transform.position.x;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            startPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        
        else if (Input.GetMouseButtonUp(0))
        {
            float pos = camera.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x;
            //transform.position = new Vector3(Mathf.Clamp(transform.position.x - pos, -11, 11), transform.position.y, transform.position.z);
            targetPosition = Mathf.Clamp(transform.position.x - pos, -11, 11);
        }

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosition, _speed * Time.deltaTime), transform.position.y, transform.position.z);
    }
}