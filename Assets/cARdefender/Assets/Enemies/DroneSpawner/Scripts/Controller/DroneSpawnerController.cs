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
                _droneSpawnerModel.AliveDronesCounter.OnValueChanged.AddListener(Counter_OnValueChanged);
                _droneSpawnerView.OnSpawn.AddListener(DroneSpawnerView_OnSpawn);
                _droneSpawnerView.OnDestroyDroneEvent.AddListener(DroneSpawnerView_OnDestroyDrone);
                
                Context.CommandManager.AddCommandListener<GameLevelChangedCommand>(OnGameLevelChanged);

                _droneSpawnerModel.AliveDronesCounter.Value = 0;
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
        private void DroneSpawnerView_OnSpawn(Transform spawnPosition)
        {
            RequireIsInitialized();

            // Spawn Drone 
            
            DroneView newDroneView = GameObject.Instantiate(_droneViewPrefab,spawnPosition.position,spawnPosition.rotation).GetComponent<DroneView>();
            newDroneView.Initialize(Context);

            DroneModel newDroneModel = new DroneModel();
            
            _spawnedDroneController = new DroneController(newDroneModel, newDroneView);
            _spawnedDroneController.Initialize(Context);
            
            newDroneView.gameObject.SetActive(true);

            _droneSpawnerModel.AliveDronesCounter.Value += 1;
        }
        
        private void Counter_OnValueChanged(int previousValue, int currentValue)
        {
            RequireIsInitialized();

            if (_droneSpawnerModel.AliveDronesCounter.Value <= 0)
            {
                Context.CommandManager.InvokeCommand(new LevelChangeRequestCommand());
            }
            
        }
        
        private async Task SpawnNewDrone()
        {
            await Task.Delay(1000);
            _droneSpawnerView.OnSpawn.Invoke(_droneSpawnerView.spawnPosition);
        }
        
        private void DroneSpawnerView_OnDestroyDrone()
        {
            RequireIsInitialized();

            _droneSpawnerModel.AliveDronesCounter.Value -= 1;
        }

        private async void SpawnDronesOnLevelChanged()
        {
            while (_droneSpawnerModel.AliveDronesCounter.Value < _droneSpawnerModel.MaxAliveDrones.Value)
            {
                await SpawnNewDrone();
            }
            
        }
        
        private void OnGameLevelChanged(GameLevelChangedCommand gameLevelChangedCommand)
        {
            _droneSpawnerModel.MaxAliveDrones.Value = gameLevelChangedCommand.Level;
            
            SpawnDronesOnLevelChanged();
        }
        
    }
