using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class HittableFromBelow : MonoBehaviour
{
    //the protected keyword denotes a generally private field but is accessible by the entities that inherit from this class
    [SerializeField] protected Sprite _usedSprite;
    Animator _animator;
    static readonly int Use1 = Animator.StringToHash("Use");
    AudioSource _audioSource;

    //this property is accessed by a child entity that will determine if the sprite is usable and set this to true
    protected virtual bool CanUse => true;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>(); 
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //if the sprite is not usable, return. 
        if (CanUse == false)
            return;
        
        var player = collision.collider.GetComponent<Player>(); 
        if(player == null)
            return;
        
        //if the collision is from the bottom going in the up direction and remaining coins is greater than 0
        if (collision.contacts[0].normal.y > 0)
        {
            //method to play audio
            PlayAudio();

            //method to play animation
            PlayAnimation(); 
            
            //this is a method in the child class
            Use();
            //if the sprite is not usable, change the sprite
            if (CanUse == false)
            {
                GetComponent<SpriteRenderer>().sprite = _usedSprite; 
            }
        }
    }

    void PlayAudio()
    {
        if (_audioSource != null)
            _audioSource.Play(); 
    }

    void PlayAnimation()
    {
        //if the animator exists, activate the trigger Use
        if (_animator != null)
            _animator.SetTrigger(Use1); 
    }

    //this is a method that children will override so the protected keyword is used for children accessibility
    //the virtual keyword allowed for the override
    //the abstract keyword means that the sub class must use this method
    protected abstract void Use();

}