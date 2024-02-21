using System.Collections.Generic;
using UnityEngine;

public class PaddockMechanic : MonoBehaviour
{
    public List<SheepMechanic> CatchedSheeps;
    public List<Transform> ParkingPoints;
    private void Awake()
    {
        CatchedSheeps = new List<SheepMechanic>();
    }
    private void OnTriggerEnter(Collider other)
    {
        SheepMechanic animal = other.GetComponent<SheepMechanic>();
        
        if(animal)
        {
            if(!CatchedSheeps.Contains(animal))    
            {
                animal.isInPaddock = true;
                animal.isMovable = false;
                animal.SaveSheep();
                
                animal.SetFinishPosition(ParkingPoints[CatchedSheeps.Count].position);
                ParkingPoints[CatchedSheeps.Count].transform.GetChild(0).gameObject.SetActive(true);

                CatchedSheeps.Add(animal);
            }
        }
    }
    
    
}
