using UnityEngine;

public class RunnerEnemy : BaseEnemy, GrabInterface
{
    public enum RunnerState {
        Idle,
        Running
    }
    public RunnerState runnerState = RunnerState.Idle;

    
    public void Grab(GameObject actor)
    {
        throw new System.NotImplementedException();
    }

    public void Release()
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
