using System.Collections.Generic;
using UnityEngine;

public class TubeHandler : MonoBehaviour
{
    private Transform[] _insidePoints;
    private List<Animal> _catchedAnimals;

    private bool _reachedFirstPoint = false;

    private void Awake()
    {
        _insidePoints = new[] { transform.GetChild(0), transform.GetChild(1) };
        _catchedAnimals = new List<Animal>();
    }

    private void FixedUpdate()
    {
        if (_catchedAnimals.Count > 0)
        {
            for (int i = 0; i < _catchedAnimals.Count; i++)
            {
                Animal caughtAnimal = _catchedAnimals[i];
                if (!_reachedFirstPoint)
                {
                    MoveToFirstPoint(caughtAnimal);
                }
                else
                {
                    // Двигаем объект к второй точке
                    MoveToSecondPoint(caughtAnimal);
                }
            }
        }
    }

    private void MoveToFirstPoint(Animal animal)
    {
        Vector3 firstPointPosition = _insidePoints[0].position;
        float distanceToFirstPoint = Vector3.Distance(animal.transform.position, firstPointPosition);
        
        if (distanceToFirstPoint > 0.4f)
        {
            animal.transform.position = Vector3.Lerp(animal.transform.position, firstPointPosition, Time.deltaTime * 5f);
        }
        else
        {
            _reachedFirstPoint = true;
        }
    }

    private void MoveToSecondPoint(Animal animal)
    {
        Vector3 secondPointPosition = _insidePoints[1].position;
        float distanceToSecondPoint = Vector3.Distance(animal.transform.position, secondPointPosition);
        
        if (distanceToSecondPoint > 0.4f)
        {
            animal.transform.position = Vector3.Lerp(animal.transform.position, secondPointPosition, Time.deltaTime * 5f);
        }
        else
        {
            if(animal is SheepMechanic sheep)
            {
                sheep.SaveSheep();
            }
            _catchedAnimals.Remove(animal);

            Destroy(animal.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Animal caughtAnimal = other.GetComponent<Animal>();
        if (caughtAnimal != null)
        {
            caughtAnimal.isMovable = false;
            _catchedAnimals.Add(caughtAnimal);
        }
    }
}
