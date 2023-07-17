using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamagable
{
    public bool IsAction = true;
    public Animator AnimatorBoss { get; private set; }

    [SerializeField] private Transform _player;
    [SerializeField] private RoboArm _roboArm;
    [SerializeField] private float _enemyHP = 700f;
    [SerializeField] private Slider _slider;

    private bool isAlive = true;

    private void Start()
    {
        _slider.maxValue = _enemyHP;
        _slider.value = _enemyHP;
        AnimatorBoss = GetComponent<Animator>();
    }

    private void Update()
    {
        _slider.value = _enemyHP;
        if (_enemyHP <= 0 && isAlive)
        {
            GetComponent<EnemyController>().DisableComponents();
            transform.position += new Vector3(0, -1f, 0);
            AnimatorBoss.SetTrigger("Broken");
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
