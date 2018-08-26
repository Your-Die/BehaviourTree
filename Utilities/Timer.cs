using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private readonly MonoBehaviour _context;
    private readonly float _duration; 

    public event Action Finished;

    public bool IsActive { get; private set; }

    public Timer(MonoBehaviour context, float duration, Action onFinished = null)
    {
        _context = context;
        _duration = duration;

        if (onFinished != null)
            Finished += onFinished;
    } 

    public void Start()
    {
        _context.StartCoroutine(TimerRoutine());
    }

    private IEnumerator TimerRoutine()
    { 
        IsActive = true; 

        yield return new WaitForSeconds(_duration);

        IsActive = false;
        Finished?.Invoke();
    }
}

public static class TimerExtensions
{
    public static Timer StartTimer(this MonoBehaviour behaviour, float duration, Action onFinished = null)
    {
        Timer timer = new Timer(behaviour, duration, onFinished);
        timer.Start();

        return timer;
    }
}