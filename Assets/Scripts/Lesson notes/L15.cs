using UnityEngine;

namespace Lesson_notes
{
    public class L15 : MonoBehaviour
    { 
        /*
        to have objects align with each other like door parts, have the transforms of the children reset to the transform of the parent
             
        profiling: as the game is playing, you can see where memory bottlenecks and buildups are so you can fix performance issues
            window -> analysis -> profiler
            on the left there are toggles for the processes you want to select for
            garbage collector: process that cleans up allocated memory, minimize this as much as possible
                on the bottom left of the window: make sure hierarchy is selected in the drop down
                sort the profiler at the bottom by GC alloc to see what largest allocations at the top
                use deep profile at the top, before you start playing, to get a more detailed look at what is causing allocation
                as you click through the hierarchy on the left that is showing you what is causing issues, you should see the methods causing the problems
                
        ctrl t: search everywhere
            
        dissect each unique piece of a method to figure out what is causing issues
            find code that is creating something over and over: instantiate, new, etc
            if you are not sure what the problem is, have the candidate processes run more than once to see if the allocation goes up
                the number of repeat executions is the multiple of the original allocation
        canvas: UI object that can be overlayed on the world space or onto the camera
        
        images: UI elements used in UI objects that uses a sprite
        
        sprite: the images themselves but can used by image or spriterenderer
        
        debugging: test differences in location, duplicates, components, etc
        
        */
    
    
    }
}
