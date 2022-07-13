 using System.Collections;
 using UnityEngine;

 public class FadingCloud : HittableFromBelow
 {
     [SerializeField] float _resetTime = 1f;
     
     SpriteRenderer _spriteRenderer;
     Collider2D _collider2D;
     

     void Awake()
     {
         _spriteRenderer = GetComponent<SpriteRenderer>();
         _collider2D = GetComponent<Collider2D>(); 
     }
     protected override void Use()
     {
        _spriteRenderer.enabled = false;
        _collider2D.enabled = false;

        StartCoroutine(ResetAfterDelay()); 

     }

     IEnumerator ResetAfterDelay()
     {
         yield return new WaitForSeconds(_resetTime);
         _spriteRenderer.enabled = true; 
         _collider2D.enabled = true; 
     }
 }