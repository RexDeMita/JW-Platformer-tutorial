using System.Collections.Generic;
  using System.Linq;
  using TMPro;
  using UnityEngine;
  using UnityEngine.Events;

public class Collector : MonoBehaviour
  {
  //this class should keep track of what is being collected and what to do after all the collectibles have been collected
  //this is an array of collectibles
  [SerializeField] List<Collectible> _collectibles;
  [SerializeField] UnityEvent _onCollectionComplete;
  
  //to set this color for the unselected gizmo, select all the objects that will use the gizmo, then change the color. 
  //when selecting a different type of object, the color selected above will show
  [SerializeField] Color _gizmoColor;

  TMP_Text _remainingText;
  int _countCollected;
  

  // Start is called before the first frame update
    void Start()
    {
      //this is a variable to hold a reference to the text component of the tmp component of the canvas that is a child of this collectible
      _remainingText = GetComponentInChildren<TMP_Text>();
      
      //this is a foreach loop that will iterate over each item and pass a reference of the object using this code into the setCollector method
      foreach (var collectible in _collectibles)
      {
        //this appends a new registration for OnPickedUp to call ItemPickedUp
        collectible.OnPickedUp += ItemPickedUp; 
      }
      //this line and the line with the setText will set the text at start
      //initialize count remaining variable the total of items in the array minus the amount collected so far
      int countRemaining = _collectibles.Count - _countCollected;  
      
      //this line checks to see if remainingText exists then assigns countRemaining
      _remainingText?.SetText(countRemaining.ToString());
    }

    //this method is called from collectibles so that the collectible tells the collector when it is picked up
    //this is better than checking for how many collectibles each frame. the increment is only done when the collectible is picked up
    public void ItemPickedUp()
    {
      //this will increment countCollected
      _countCollected++; 
      
      //initialize count remaining variable the total of items in the array minus the amount collected so far
      int countRemaining = _collectibles.Count - _countCollected;  
      
      //this line checks to see if remainingText exists then assigns countRemaining
      _remainingText?.SetText(countRemaining.ToString());
      
      //if countRemaining is greater than 0, return;
      if (countRemaining > 0)
        return;
      
      Debug.Log("Got All Gems"); 
      _onCollectionComplete.Invoke();
    }
    //this method is called in the editor only
    //called when the script is loaded or a value is changed in the inspector
    //in this case, when i drop a gem into the collector component in the inspector, if it is a duplicate, it will be deleted as soon as it is dropped in
    private void OnValidate()
    {
      //a new list of distinct items will be returned
      _collectibles = _collectibles.Distinct().ToList(); 
    }
    
    
    
    //to show a visual relationship between the selected collectors and the collectibles within their arrays
    /*
    void OnDrawGizmos()
    {
      //this loop will attach lines from this collector to each collectible in the array of collectibles
      foreach (var collectible in _collectibles)
      {
        //if the active game object is the selected game object... 
        if(UnityEditor.Selection.activeGameObject == gameObject)
          //sets the gizmo color to yellow
          Gizmos.color = Color.yellow;
        else 
          //sets the gizmo color to whatever color I want
          Gizmos.color = _gizmoColor;
        
        //a line will be drawn from the collector position to the collectible position
        Gizmos.DrawLine(transform.position, collectible.transform.position); 
      }
       
    }
    */
  }
