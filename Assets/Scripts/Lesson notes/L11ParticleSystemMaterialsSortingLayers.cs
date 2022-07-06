using UnityEngine;

namespace Lesson_notes
{
    public class L11ParticleSystemMaterialsSortingLayers: MonoBehaviour
    {
        /*
    particle system: default particle system, there are others to try out 
        GameObject -> Particle system
     
    particle system acts and moves in 3D. the axis that the particle system is relevant
        at x,y,z(0,0,0) the particle system shoots particles toward you out of the screen
        at -90, 0, 0 the particle system shoots up
        
        needs to use material
     
     material: instructions on how to render an object 
        in the shader field drop down: choose particles then standard unlit (for objects without lighting)
        in the maps section
            albedo: texture of the material
        in the blending options section
            rendering mode: fade will allow us to fade the object to nothing 
     
     to change the material of a particle system
        in the particle system component in the inspector
            find the renderer tab and click to expand it (not clear it can be expanded)
            the material field of this tab is the destination of any material you prepared for this particle system to use
            
    some settings to affect the particle system are in the particle system tab of the particle system component
        on the far right of each field, there is a drop down arrow
            random between two constants: to create variation in the particle setting
    
    emission tab in particle system component
        bursts: burst of particles
        
    particle effect tab: click restart to see the effect happen 
    
    color over lifetime tab: affect the color of the particle over time
    
    use existing animator controller to control new animation clips and states
        when creating new clips
            click record to prepare the animation for sprite selection
            choose the sprite(s) you want to use 
            the chosen sprite will turn red and a keyframe will appear on the animation timeline
            click the record button to disable any more key frame additions to the timeline of that animation
            
    anim files can be dragged and dropped into the animator controller window to create a new state
    
    create a new clip by using the drop down menu in the animation window that has the name of the current animation clip
    
    renderer tab in the particle system component: 
        to add a sorting layer
        click on the sorting layer ID drop down menu and click add sorting layer
        the tags and layers menu will be brought up
        open the sorting layers drop down menu 
        add a layer using the plus button
        
    the layers in the sorting layer are stacked in rendering order: the top is rendered toward the back of the screen away from you and the bottom of the stack is closer to you
        the rendering of objects with the same layer is then governed by the sorting layer within that object - -10 (toward the back of ths screen) vs 10 
        
    
     */
    }
}
