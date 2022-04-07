using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageRecognition : MonoBehaviour
{
    private ARTrackedImageManager _arTrackedImageManager;

    [SerializeField]
    private GameObject[] pokemons;

    private Dictionary<string, GameObject> spawnedPokemon = new Dictionary<string, GameObject>();
    public Dictionary<string, PokemonDetails> spawnedPokemonDetails = new Dictionary<string, PokemonDetails>();

    public string currentPokemonName;

    private void Awake()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();

        foreach (GameObject pokemon in pokemons)
        {
            GameObject newPrefab = Instantiate(pokemon, Vector3.zero, Quaternion.identity);
            newPrefab.name = pokemon.name;
            newPrefab.SetActive(false);
            spawnedPokemon.Add(pokemon.name, newPrefab);
            spawnedPokemonDetails.Add(pokemon.name, newPrefab.GetComponent<PokemonDetails>());
        }

    }

    public void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }
    public void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }
    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            //if (trackedImage.trackingState != TrackingState.None && trackedImage.trackingState != TrackingState.Limited)
                updateImage(trackedImage);
            //else
                //spawnedPokemon[currentPokemonName].SetActive(false);
        }
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            if (trackedImage.trackingState != TrackingState.None && trackedImage.trackingState != TrackingState.Limited)
                updateImage(trackedImage);
            else
                spawnedPokemon[currentPokemonName].SetActive(false);
        }
        foreach (ARTrackedImage trackedImage in args.removed)
        {
            spawnedPokemon[currentPokemonName].SetActive(false);
        }
    }
    private void updateImage(ARTrackedImage trackedImage)
    {
        Vector3 position = trackedImage.transform.position;
        Quaternion rotation = trackedImage.transform.rotation;

        GameObject pokemon = spawnedPokemon[currentPokemonName];
        pokemon.transform.position = position;
        pokemon.transform.rotation = rotation;
        pokemon.SetActive(true);

        foreach(GameObject go in spawnedPokemon.Values)
        {
            if(go.name != currentPokemonName)
            {
                go.SetActive(false);
            }
        }
    }
}
