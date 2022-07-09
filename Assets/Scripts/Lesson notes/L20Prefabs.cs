using UnityEngine;

namespace Lesson_notes
{
    public class L20Prefabs : MonoBehaviour
    {
        /*
         prefabs should just work in the scene without much more code or work
            ideally only referencing itself or its own components
            if the object references other objects in the scene, that is more likely a specific scenario that needs its own configuring
            
            something too simple would also not make a good prefab
            
            any change in a component of a prefab instance will be denoted with a minus symbol
            
            to apply changes made in one prefab instance to all instances
                go to the overrides window of the parent prefab
                you will see the change you made
                apply all 
            
            nested: making objects nested under a prefab its own prefab helps to allow for editing of the nested prefab 
                if i place a nested prefab into the scene and then edit the prefab, the unique nested prefab and 
                the original nested prefab are both edited
                
            unpacking
                removes the object as a prefab
                unpack completely
                    removes the object and all children as prefabs
                    
            
                
            
         */
    }
}
