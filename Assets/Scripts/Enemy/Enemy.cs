using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator AnimatorBoss { get; private set; }

    [SerializeField] private Transform _player;
    [SerializeField] private RoboArm _roboArm;

    private void Start()
    {
        AnimatorBoss = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public Transform GetPlayerTransform()
    {
        return _player;
    }
    public bool IsAnimationPlaying(string animationName)
    {
        // берем информацию о состоянии
        var animatorStateInfo = AnimatorBoss.GetCurrentAnimatorStateInfo(0);
        // смотрим, есть ли в нем имя какой-то анимации, то возвращаем true
        if (animatorStateInfo.IsName(animationName))
            return true;

        return false;
    }

    public RoboArm GetRoboArm()
    {
        return _roboArm;
    }
}
