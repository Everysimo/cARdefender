using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Drone.Scripts.Model;
using cARdefender.Assets.Interactable.Boxes.Scripts;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using RMC.Core.Architectures.Mini.Samples.SpawnerMini.WithMini.Mini.Controller.Commands;
using UnityEngine;

public class BoxSpawnerController : IController
    {
    
        //  Events ----------------------------------------


        //  Properties ------------------------------------
        public bool IsInitialized { get; private set; }
        public IContext Context { get; private set; }
        
        
        //  Fields ----------------------------------------
        private readonly BoxSpawnerView _boxSpawnerView;
        
        private readonly BoxView _healthBoxViewPrefab;
        
        DroneController _spawnedDroneController;
        
        
        //  Initialization  -------------------------------
        public BoxSpawnerController(
            BoxSpawnerView boxSpawnerView, BoxView healthBoxViewPrefab)
        {
            //MODEL
            
            //VIEW
            _boxSpawnerView = boxSpawnerView;
            _healthBoxViewPrefab = healthBoxViewPrefab;
        }
        
        
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                Context = context;
                
                //View
                _boxSpawnerView.OnBusDetectedEvent.AddListener(View_OnSpawnHealthBox);
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
        private void View_OnSpawnHealthBox()
        {
            RequireIsInitialized();

            // Spawn Drone 
            
            BoxView newBoxView = GameObject.Instantiate(_healthBoxViewPrefab).GetComponent<BoxView>();
            newBoxView.Initialize(Context);

             BoxModel boxModel = new BoxModel();
            
             BoxController spawnedBoxController = new BoxController(boxModel, newBoxView);
            spawnedBoxController.Initialize(Context);
            
            newBoxView.gameObject.SetActive(true);
            
        }
        
        
    }

