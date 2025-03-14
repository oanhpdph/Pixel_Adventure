using UnityEngine;

public class MovingCircle : MonoBehaviour
{
    [SerializeField] private Transform rotationCenter;
    [SerializeField] private float rotationRadius = 2f, angularSpeed = 2f;
    [SerializeField] private bool isCircle = true;

    private int _direction = 1;
    float posX, posY, angle;

    private void Start()
    {
        angle = Vector2.SignedAngle(new Vector2(1, 0), transform.position - rotationCenter.position);
    }
    private void Update()
    {
        Moving();
    }

    private void Moving()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;

        transform.position = new Vector2(posX, posY);
        Direction();
        Angle();
    }

    private void Direction()
    {
        if (!isCircle)
        {
            if (angle >= 360f * Mathf.Deg2Rad)
                _direction = -1;
            else if (angle <= 180f * Mathf.Deg2Rad)
                _direction = 1;
        }
    }

    private void Angle()
    {
        if (_direction == -1)
            angle -= Time.deltaTime * angularSpeed;
        else
        {
            angle += Time.deltaTime * angularSpeed;
        }
    }
}
