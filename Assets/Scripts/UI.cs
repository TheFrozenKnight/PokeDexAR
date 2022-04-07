using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class UI : MonoBehaviour
{
    public GameObject StartMenu, InAppUI, arSession;

    public ImageRecognition imageRecognition;

    public GameObject[] pokemons;
    private int ActivePokemon = 0;
    private PokemonDetails ActivePokemonDetails;

    public Text PokemonName, HP, Attack, Defense, SpAttack, SpDefense, Speed, Fact;
    public Image Icon, Type1, Type2;

    public void onStartButtonClick()
    {
        arSession.SetActive(true);
        StartMenu.SetActive(false);
        InAppUI.SetActive(true);
        ActivePokemon = 0;
        imageRecognition.currentPokemonName = pokemons[ActivePokemon].name;
        UpdateUI();
    }
    public void onExitButtonClick()
    {
        Application.Quit();
    }
    public void onNextPokemonButtonClick()
    {

        if (ActivePokemon == pokemons.Length-1)
            ActivePokemon = 0;
        else
            ActivePokemon++;

        imageRecognition.currentPokemonName = pokemons[ActivePokemon].name;

        UpdateUI();
    }
    public void onPreviousPokemonButtonClick()
    {
        if (ActivePokemon == 0)
            ActivePokemon = pokemons.Length-1;
        else
            ActivePokemon--;

        imageRecognition.currentPokemonName = pokemons[ActivePokemon].name;

        ActivePokemonDetails = pokemons[ActivePokemon].gameObject.GetComponent<PokemonDetails>();

        UpdateUI();
    }
    public void onAttackButtonClick()
    {
        imageRecognition.spawnedPokemonDetails[imageRecognition.currentPokemonName].attack();
    }
    public void onBackButtonClick()
    {
        arSession.SetActive(false);
        StartMenu.SetActive(true);
        InAppUI.SetActive(false);
    }

    public void UpdateUI()
    {
        ActivePokemonDetails = pokemons[ActivePokemon].gameObject.GetComponent<PokemonDetails>();

        PokemonName.text = ActivePokemonDetails.Name;
        Icon.sprite = ActivePokemonDetails.icon;

        HP.text = "HP " + ActivePokemonDetails.HP;
        Attack.text = "Attack " + ActivePokemonDetails.Attack;
        Defense.text = "Defense " + ActivePokemonDetails.Defense;
        SpAttack.text = "Sp. Attack " + ActivePokemonDetails.SpAttack;
        SpDefense.text = "Sp. Defense " + ActivePokemonDetails.SpDefense;
        Speed.text = "Speed " + ActivePokemonDetails.Speed;

        Fact.text = "" + ActivePokemonDetails.Fact;

        Type1.sprite = ActivePokemonDetails.Type1;
        Type2.sprite = ActivePokemonDetails.Type2;
    }
}
