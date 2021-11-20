using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public virtual GameState CurrentState
    {
        get { return _currentState; }
        set { Transition (value); }
    }
    protected GameState _currentState;
    protected bool _inTransition;


    public virtual T GetState<T> () where T : GameState
  {
    T target = GetComponent<T>();
    if (target == null)
      target = gameObject.AddComponent<T>();
    return target;
  }
  
  public virtual void ChangeState<T> () where T : GameState
  {
    CurrentState = GetState<T>();
  }
  protected virtual void Transition (GameState value)
  {
    if (_currentState == value || _inTransition)
      return;
    _inTransition = true;
    
    if (_currentState != null)
      _currentState.Disable();
    
    _currentState = value;
    
    if (_currentState != null)
      _currentState.Enable();
    
    _inTransition = false;
  }
}
