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
        
        private readonly HealthBoxView _healthBoxViewPrefab;
        
        private readonly DoubleGunBoxView _doubleGunBoxViewPrefab;

        private BoxManager _boxManager;
        
        DroneController _spawnedDroneController;
        
        
        //  Initialization  -------------------------------
        public CrateSpawnerController(
            CrateSpawnerView crateSpawnerView, HealthBoxView healthBoxViewPrefab, DoubleGunBoxView doubleGunBoxViewPrefab)
        {
            //MODEL
            
            //VIEW
            _crateSpawnerView = crateSpawnerView;
            _healthBoxViewPrefab = healthBoxViewPrefab;
            _doubleGunBoxViewPrefab = doubleGunBoxViewPrefab;
        }
        
        
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                Context = context;
                
                //View
                _crateSpawnerView.OnHealthBoxSpawnRequestEvent.AddListener(View_OnSpawnHealthBox);
                _crateSpawnerView.OnDoubleGunBoxSpawnRequestEvent.AddListener(View_OnSpawnDoubleGun);
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
        private void View_OnSpawnBox<BoxViewType>(BoxConsumerHandle boxConsumerHandle, BoxView prefab) where BoxViewType : BoxView
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
                boxConsumer =  boxConsumerReturn;
            }
            else
            {
                boxConsumer = newBoxView.GetComponentInChildren<BoxConsumer>();
            }

            boxConsumer.OnBoxLost.AddListener(() =>
            {
                _boxManager.Unsubscribe(boxConsumerHandle);
                newBoxView.DestryBox();
            });
            boxConsumer.SubscribeToHandle(boxConsumerHandle);

            newBoxView.gameObject.SetActive(true);
        }

        // Usage examples:
        private void View_OnSpawnHealthBox(BoxConsumerHandle boxConsumerHandle)
        {
            View_OnSpawnBox<HealthBoxView>(boxConsumerHandle, _healthBoxViewPrefab);
        }

        private void View_OnSpawnDoubleGun(BoxConsumerHandle boxConsumerHandle)
        {
            View_OnSpawnBox<DoubleGunBoxView>(boxConsumerHandle, _doubleGunBoxViewPrefab);
        }

        public void SpawnDoubleGunCrate()
        {
            RequireIsInitialized();

            // Spawn Box 
            DoubleGunBoxView newBoxView = GameObject.Instantiate(_doubleGunBoxViewPrefab).GetComponent<DoubleGunBoxView>();
            //HealthBoxView newBoxView = GameObject.Instantiate(_healthBoxViewPrefab).GetComponent<HealthBoxView>();
            newBoxView.Initialize(Context);

            BoxModel boxModel = new BoxModel();

            BoxController spawnedBoxController = new BoxController(boxModel, newBoxView);
            spawnedBoxController.Initialize(Context);

            newBoxView.gameObject.SetActive(true);
        }

    }

