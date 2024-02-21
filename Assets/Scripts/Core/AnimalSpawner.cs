using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimalSpawner 
{
    
    public void SpawnAnimal(Action<List<Animal>> CreateListDone, Animal animalPrefab,Transform basePoint, int count, float radius)
    {
        List<Animal> activeAnimals = new List<Animal> (count);

        float angleIncrement = (Mathf.PI * 2) / count; 

        for (int i = 0; i < count; i++)
        {
            float angle = angleIncrement * i;
            float x = Mathf.Sin(angle) * radius; 
            float y = Mathf.Cos(angle) * radius; 

            Vector3 spawnPoint = basePoint.position + new Vector3(x, 1, y);
            spawnPoint.y = 1;
            activeAnimals.Add(GameObject.Instantiate(animalPrefab, spawnPoint, Quaternion.identity));
        }
        /*for (float i = 0; i < Pi2; i += Mathf.PI*2 / count )
        {
            Vector3 spawn = basePoint.position + new Vector3 ( Mathf.Sin(i), 1, Mathf.Cos(i)) * radius;
            spawn.y = 1;
            activeAnimals.Add(GameObject.Instantiate(animalPrefab, spawn, Quaternion.identity));
        }*/
        CreateListDone?.Invoke(activeAnimals);
    }
    
}
