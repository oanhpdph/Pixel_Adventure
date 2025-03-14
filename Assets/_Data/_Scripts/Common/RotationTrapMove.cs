using UnityEngine;

public class RotationTrapMove : MonoBehaviour
{
    [SerializeField] private Transform rotationCenter;
    [SerializeField] private float rotationRadius = 3f, angularSpeed = 3f;
    [SerializeField] private float angleLimit;

    private bool isIncreaseAngle = false;
    private float currentAngle = 0;
    private void Start()
    {
        currentAngle = Vector2.SignedAngle(Vector2.right, transform.position - rotationCenter.position);

    }
    private void Update()
    {
        Moving();
    }

    private void Moving()
    {
        Vector2 newPosition = new();
        newPosition.x = rotationCenter.position.x + Mathf.Cos(currentAngle * Mathf.Deg2Rad) * rotationRadius;
        newPosition.y = rotationCenter.position.y + Mathf.Sin(currentAngle * Mathf.Deg2Rad) * rotationRadius;

        transform.position = newPosition;

        CheckAngle();
        RotationAngle();

    }
    private void CheckAngle()
    {
        float angle = Vector2.SignedAngle(Vector2.down, transform.position - rotationCenter.position);
        if (angle > angleLimit)
        {
            isIncreaseAngle = true;
        }
        else if (angle < -angleLimit)
        {
            isIncreaseAngle = false;
        }
    }

    private void RotationAngle()
    {
        if (isIncreaseAngle)
        {

            currentAngle -= Time.deltaTime * angularSpeed;
        }
        else
        {
            currentAngle += Time.deltaTime * angularSpeed;
        }
    }
}
