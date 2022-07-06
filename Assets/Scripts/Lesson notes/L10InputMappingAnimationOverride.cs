using UnityEngine;

namespace Lesson_notes
{
    public class L10InputMappingAnimationOverride : MonoBehaviour
    {
        /*
     Input system: all the buttons and joystick input that you need for your game
        within project settings
     axes within the input system: the individual axis that you assign to a button or joystick
     
     important fields of input axis: name, descriptive name, negative and positive buttons, gravity, dead, sensitivity, type, axis
        negative and positive buttons: the buttons that will represent the negative and positive sides of the axis
        sensitivity: how long it takes for a button press or joystick press to reach the full value of 1
            if the sensitivity is 3, it takes 0.33 seconds for the value to reach 1. 
            1/(sensitivity) = number of seconds it will take for the value to reach 1
            
     to save settings, save project, NOT JUST THE SCENE
     
     string interpolation: converting a value such as an integer into a string that gets used as input into a method
    
    debugging in windows: usb controller settings -> controller properties: calibration for dead zones, button mappings, input axes
    
    close and relaunch editor to fix some issues
    
    character wiggling can be a joystick that has a hardware problem: calibrate the dead zone value 
    
    it is hard to rearrange the input axes in the input manager so plan a little ahead to keep things organized
    
    the axis options in the axis field of the axes of the input system do not always map to what you intuitively think the axis should map to. test and retest often
    look up your controller mappings online for your controller and unity 
    
    keep things private until you need to change things to public later. dont use setters until necessary
    
    animator override controller: use an existing controller to control the sprites of an anim folder that has different sprites
        you dont have to rebuild a controller over and over 
     
     */
    }
}
