using System;
using System.Collections;
using UnityEngine;

public class MovingPointToPoint : MonoBehaviour
{
    [Range(0, 10f)]
    [SerializeField]
    protected float _speed = 3f;

    [Range(0, 4f)]
    [SerializeField]
    private float _waitDuration = 0.1f;

    public bool isCircle;
    public int nextPointDefault = 0;

    protected int _speedMultiplier = 1;

    protected Vector3 _tagetPos;
    protected int _pointIndex;

    private GameObject _ways;// get object has name  = ways
    private Transform[] _wayPoints;
    private int _pointCount;
    private int _direction = 1;

    private void Start()
    {
        AddWayPoints();
        SetInit();
    }

    public virtual void Update()
    {
        Moving();
    }
    protected void SetInit()
    {
        _pointCount = _wayPoints.Length;
        _pointIndex = nextPointDefault;
        _tagetPos = _wayPoints[_pointIndex].transform.position;
    }
    protected void AddWayPoints()
    {
        _ways = transform.parent.Find("Ways").gameObject;
        _wayPoints = new Transform[_ways.transform.childCount];
        for (int i = 0; i < _ways.transform.childCount; i++)
        {
            _wayPoints[i] = _ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void GobackDirection()
    {
        if (_pointIndex == _pointCount - 1)
        {
            _direction = -1;
        }
    }
    private void CircleDirection()
    {
        if (_pointIndex == _pointCount - 1)
        {
            _pointIndex = 0;
            _direction = 0;
        }
    }
    protected void RunningStyle()
    {
        if (isCircle)
        {
            CircleDirection();
        }
        else
        {
            GobackDirection();
        }

        NextPoint();

        PointStart();
    }
    private void PointStart()
    {
        if (_pointIndex == 0)
        {
            _direction = 1;
        }
    }
    private void NextPoint()
    {
        _pointIndex += _direction;
        _tagetPos = _wayPoints[_pointIndex].transform.position;
        StartCoroutine(WaitNextPoint());
    }
    public virtual void Moving()
    {
        float step = _speed * _speedMultiplier * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _tagetPos, step);

        if (Vector3.Distance(transform.position, _tagetPos) < 0.2f)
        {
            RunningStyle();
        }
    }

    IEnumerator WaitNextPoint()
    {
        _speedMultiplier = 0;
        yield return new WaitForSeconds(_waitDuration);
        _speedMultiplier = 1;
    }
}
