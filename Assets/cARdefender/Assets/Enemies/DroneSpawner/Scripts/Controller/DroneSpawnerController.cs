using System;
using System.Collections;
using System.Collections.Generic;
using cARdefender.Assets.Enemies.Drone.Scripts.Model;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using RMC.Core.Architectures.Mini.Samples.SpawnerMini.WithMini.Mini.Controller.Commands;
using RMC.Core.Architectures.Mini.Samples.SpawnerMini.WithMini.Mini.Model;
using UnityEngine;

public class DroneSpawnerController : IController
    {
    
        //  Events ----------------------------------------


        //  Properties ------------------------------------
        public bool IsInitialized { get; private set; }
        public IContext Context { get; private set; }
        
        
        //  Fields ----------------------------------------
        private readonly DroneSpawnerView _droneSpawnerView;
        private readonly DroneSpawnerModel _droneSpawnerModel;
        
        private readonly DroneView _droneViewPrefab;
        
        DroneController _spawnedDroneController;
        
        
        //  Initialization  -------------------------------
        public DroneSpawnerController(DroneSpawnerModel droneSpawnerModel,
            DroneSpawnerView droneSpawnerView, DroneView droneViewPrefab)
        {
            //MODEL
            _droneSpawnerModel = droneSpawnerModel;
            
            
            //VIEW
            _droneSpawnerView = droneSpawnerView;
            _droneViewPrefab = droneViewPrefab;
        }
        
        
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                Context = context;
                
                //View
                _droneSpawnerModel.IdCounter.OnValueChanged.AddListener(Counter_OnValueChanged);
                _droneSpawnerView.OnSpawn.AddListener(DroneSpawnerView_OnSpawn);
                Debug.Log("Init Spawner");
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
        private void DroneSpawnerView_OnSpawn()
        {
            RequireIsInitialized();

            Debug.Log("Drone Inizializzato prima");
            
            
            // Spawn Drone 
            
            DroneView newDroneView = GameObject.Instantiate(_droneViewPrefab).GetComponent<DroneView>();
            newDroneView.Initialize(Context);
            

            DroneModel newDroneModel = new DroneModel();
            
            _spawnedDroneController = new DroneController(newDroneModel, newDroneView);
            _spawnedDroneController.Initialize(Context);
            
            newDroneView.gameObject.SetActive(true);
            
            
            Debug.Log("Drone Inizializzato dopo");

            _droneSpawnerModel.IdCounter.Value += 1;
        }
        
        private void Counter_OnValueChanged(int previousValue, int currentValue)
        {
            RequireIsInitialized();
            
            Context.CommandManager.InvokeCommand(
                new IdCounterChangedCommand(previousValue, currentValue));
            
        }
        
    }
