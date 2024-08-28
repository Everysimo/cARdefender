using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Drone.Scripts.Model;
using cARdefender.Assets.Interactable.Boxes.Scripts;
using cARdefender.Tests.BoxPlacement;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using RMC.Core.Architectures.Mini.Samples.SpawnerMini.WithMini.Mini.Controller.Commands;
using UnityEngine;

public class CrateSpawnerController : IController
{
    //  Events ----------------------------------------


    //  Properties ------------------------------------
    public bool IsInitialized { get; private set; }
    public IContext Context { get; private set; }


    //  Fields ----------------------------------------
    private readonly CrateSpawnerView _crateSpawnerView;

    private readonly HealthCrateView _healthCrateViewPrefab;

    private readonly DoubleGunCrateView _doubleGunCrateViewPrefab;

    private readonly AutoAimCrateView _autoAimCrateViewPrefab;

    private BoxManager _boxManager;

    DroneController _spawnedDroneController;


    //  Initialization  -------------------------------
    public CrateSpawnerController(
        CrateSpawnerView crateSpawnerView, HealthCrateView healthCrateViewPrefab,
        DoubleGunCrateView doubleGunCrateViewPrefab, AutoAimCrateView autoAimCrateViewPrefab)
    {
        //MODEL

        //VIEW
        _crateSpawnerView = crateSpawnerView;
        _healthCrateViewPrefab = healthCrateViewPrefab;
        _doubleGunCrateViewPrefab = doubleGunCrateViewPrefab;
        _autoAimCrateViewPrefab = autoAimCrateViewPrefab;
    }


    public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            IsInitialized = true;
            Context = context;

            //View
            _crateSpawnerView.OnHealthCrateSpawnRequestEvent.AddListener(View_OnSpawnHealthCrate);
            _crateSpawnerView.OnDoubleGunCrateSpawnRequestEvent.AddListener(View_OnSpawnDoubleGunCrate);
            _crateSpawnerView.OnAutoAimCrateSpawnRequestEvent.AddListener(View_OnSpawnAutoAimCrate);

            _crateSpawnerView.OnSpawnCrateRequestEvent.AddListener(SpawnDoubleGunCrate);

            _boxManager = _crateSpawnerView.boxManager;
        }
    }


    public void RequireIsInitialized()
    {
        if (!IsInitialized)
        {
            throw new Exception("MustBeInitialized");
        }
    }


    //  Methods ---------------------------------------


    //  Event Handlers --------------------------------
    private void View_OnSpawnBox<BoxViewType>(BoxConsumerHandle boxConsumerHandle, CrateView prefab)
        where BoxViewType : CrateView
    {
        RequireIsInitialized();

        // Spawn Box 
        BoxViewType newBoxView = GameObject.Instantiate(prefab).GetComponent<BoxViewType>();
        newBoxView.Initialize(Context);

        BoxModel boxModel = new BoxModel();

        BoxController spawnedBoxController = new BoxController(boxModel, newBoxView);
        spawnedBoxController.Initialize(Context);

        BoxConsumer boxConsumer;
        if (newBoxView.TryGetComponent<BoxConsumer>(out BoxConsumer boxConsumerReturn))
        {
            boxConsumer = boxConsumerReturn;
        }
        else
        {
            boxConsumer = newBoxView.GetComponentInChildren<BoxConsumer>();
        }

        boxConsumer.OnBoxLost.AddListener(() =>
        {
            _boxManager.Unsubscribe(boxConsumerHandle);
            newBoxView.DestroyBox();
        });
        boxConsumer.SubscribeToHandle(boxConsumerHandle);

        newBoxView.gameObject.SetActive(true);
    }

    // Usage examples:
    private void View_OnSpawnHealthCrate(BoxConsumerHandle boxConsumerHandle)
    {
        View_OnSpawnBox<HealthCrateView>(boxConsumerHandle, _healthCrateViewPrefab);
    }

    private void View_OnSpawnDoubleGunCrate(BoxConsumerHandle boxConsumerHandle)
    {
        View_OnSpawnBox<DoubleGunCrateView>(boxConsumerHandle, _doubleGunCrateViewPrefab);
    }

    private void View_OnSpawnAutoAimCrate(BoxConsumerHandle boxConsumerHandle)
    {
        View_OnSpawnBox<AutoAimCrateView>(boxConsumerHandle, _autoAimCrateViewPrefab);
    }

    public void SpawnDoubleGunCrate()
    {
        RequireIsInitialized();

        // Spawn Box 
        DoubleGunCrateView newCrateView =
            GameObject.Instantiate(_doubleGunCrateViewPrefab).GetComponent<DoubleGunCrateView>();
        //HealthBoxView newBoxView = GameObject.Instantiate(_healthBoxViewPrefab).GetComponent<HealthBoxView>();
        newCrateView.Initialize(Context);

        BoxModel boxModel = new BoxModel();

        BoxController spawnedBoxController = new BoxController(boxModel, newCrateView);
        spawnedBoxController.Initialize(Context);

        newCrateView.gameObject.SetActive(true);
    }
}