using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                _droneSpawnerView.OnDestroyDroneEvent.AddListener(DroneSpawnerView_OnDestroyDrone);

                _droneSpawnerModel.IdCounter.Value = 0;
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

            // Spawn Drone 
            
            DroneView newDroneView = GameObject.Instantiate(_droneViewPrefab).GetComponent<DroneView>();
            newDroneView.Initialize(Context);

            DroneModel newDroneModel = new DroneModel();
            
            _spawnedDroneController = new DroneController(newDroneModel, newDroneView);
            _spawnedDroneController.Initialize(Context);
            
            newDroneView.gameObject.SetActive(true);

            _droneSpawnerModel.IdCounter.Value += 1;
        }
        
        private async void Counter_OnValueChanged(int previousValue, int currentValue)
        {
            RequireIsInitialized();

            if (_droneSpawnerModel.IdCounter.Value < 3)
            {
                await SpawnNewDrone();
            }
            
            Context.CommandManager.InvokeCommand(
                new IdCounterChangedCommand(previousValue, currentValue));
            
        }
        
        private async Task SpawnNewDrone()
        {
            await Task.Delay(5000);
            _droneSpawnerView.OnSpawn.Invoke();
        }
        
        private void DroneSpawnerView_OnDestroyDrone()
        {
            RequireIsInitialized();

            _droneSpawnerModel.IdCounter.Value -= 1;
        }
        
    }
