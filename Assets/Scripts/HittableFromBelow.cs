using UnityEngine;
using UnityEngine.UIElements;

public class HittableFromBelow : MonoBehaviour
{
    //the protected keyword denotes a generally private field but is accessible by the entities that inherit from this class
    [SerializeField] protected Sprite _usedSprite;
    
    //this property is accessed by a child entity that will determine if the sprite is usable and set this to true
    protected virtual bool CanUse => true; 
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
            //this is a method in the child class
            Use();
            //if the sprite is not usable, change the sprite
            if (CanUse == false)
            {
                GetComponent<SpriteRenderer>().sprite = _usedSprite; 
            }
        }
    }
    //this is a method that children will override so the protected keyword is used for children accessibility
    //the virtual keyword allows for the override
    protected virtual void Use()
    {
        Debug.Log($"Used {gameObject.name}");
    }
}