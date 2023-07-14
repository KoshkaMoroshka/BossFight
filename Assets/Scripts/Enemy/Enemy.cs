using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public Animator AnimatorBoss { get; private set; }

    [SerializeField] private Transform _player;
    [SerializeField] private RoboArm _roboArm;
    [SerializeField] private float _enemyHP = 700f;

    private bool isAlive = true;

    private void Start()
    {
        AnimatorBoss = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_enemyHP <= 0 && isAlive)
        {
            AnimatorBoss.SetTrigger("Broken");
            AnimatorBoss.ResetTrigger("Broken");
            isAlive = false;
        }
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

    public void GetDamage(float damage)
    {
        _enemyHP -= damage;
    }
}
