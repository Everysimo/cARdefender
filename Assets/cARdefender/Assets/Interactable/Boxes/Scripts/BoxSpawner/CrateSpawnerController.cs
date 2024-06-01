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
        private readonly CrateSpawnerView crateSpawnerView;
        
        private readonly BoxView _healthBoxViewPrefab;

        private BoxManager _boxManager;
        
        DroneController _spawnedDroneController;
        
        
        //  Initialization  -------------------------------
        public CrateSpawnerController(
            CrateSpawnerView crateSpawnerView, BoxView healthBoxViewPrefab)
        {
            //MODEL
            
            //VIEW
            this.crateSpawnerView = crateSpawnerView;
            _healthBoxViewPrefab = healthBoxViewPrefab;
        }
        
        
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                Context = context;
                
                //View
                crateSpawnerView.OnBusDetectedEvent.AddListener(View_OnSpawnHealthBox);

                _boxManager = crateSpawnerView.boxManager;
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
        private void View_OnSpawnHealthBox(BoxConsumerHandle boxConsumerHandle)
        {
            RequireIsInitialized();

            // Spawn Box 
            
            BoxView newBoxView = GameObject.Instantiate(_healthBoxViewPrefab).GetComponent<BoxView>();
            newBoxView.Initialize(Context);

            BoxModel boxModel = new BoxModel();
            
            BoxController spawnedBoxController = new BoxController(boxModel, newBoxView);
            spawnedBoxController.Initialize(Context);
            
            BoxConsumer boxConsumer = newBoxView.GetComponent<BoxConsumer>();
            
            boxConsumer.OnBoxLost.AddListener(() =>
            {
                _boxManager.Unsubscribe(boxConsumerHandle);
                newBoxView.DestryBox();
            });
            boxConsumer.SubscribeToHandle(boxConsumerHandle);
            
            newBoxView.gameObject.SetActive(true);
            
        }
        
        
    }

