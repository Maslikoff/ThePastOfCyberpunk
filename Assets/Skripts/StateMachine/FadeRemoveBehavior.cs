using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehavior : StateMachineBehaviour
{
    public float fadeTime = 1.25f;
    public float fadeDelay = .0f;

    private float _timeElapsed = 0f;
    private float _fadeDelayElapsed = 0f;
    private SpriteRenderer _sprite;
    private GameObject _gameObject;
    private Color _startColor;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeElapsed = 0f;
        _sprite = animator.GetComponent<SpriteRenderer>();
        _startColor = _sprite.color;
        _gameObject = animator.gameObject;  
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fadeDelay > _fadeDelayElapsed)
        {
            _fadeDelayElapsed += Time.deltaTime;
        }
        else
        {
            _timeElapsed += Time.deltaTime;

            float newAlpha = _startColor.a * (1 - _timeElapsed / fadeTime);

            _sprite.color = new Color(_startColor.r, _startColor.g, _startColor.b, newAlpha);

            if (_timeElapsed > fadeTime)
            {
                Destroy(_gameObject);
            }
        }
    }
}
