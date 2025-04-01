using UnityEngine;

[System.Serializable]
public abstract class State
{
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();

}