using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator AnimatorBoss { get; private set; }

    [SerializeField] private Transform _player;

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
        // ����� ���������� � ���������
        var animatorStateInfo = AnimatorBoss.GetCurrentAnimatorStateInfo(0);
        // �������, ���� �� � ��� ��� �����-�� ��������, �� ���������� true
        if (animatorStateInfo.IsName(animationName))
            return true;

        return false;
    }
}
