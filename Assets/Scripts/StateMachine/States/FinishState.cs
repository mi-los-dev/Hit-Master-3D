using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishState : State
{
    private float _danceTimeInSec = 3f;
    
    public FinishState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }
    
    public override void Enter()
    {
        character.Animator.SetBool("IsDance", true);
    }

    public override void Update()
    {
        if (_danceTimeInSec > 0) _danceTimeInSec -= Time.deltaTime;
        else SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}