using System;
using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Samples.SpawnerMini.WithMini.Mini.Controller.Commands;
using RMC.Core.Architectures.Mini.View;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using CounterChangedCommand = RMC.Core.Architectures.Mini.Samples.CountUp.WithMini.Mini.Controller.Commands.CounterChangedCommand;

public class DroneSpawnerView : MonoBehaviour,IView
    {
        //  Events ----------------------------------------
        [HideInInspector] 
        public readonly UnityEvent OnIncrementCounter = new UnityEvent();
        
        [HideInInspector] 
        public readonly UnityEvent OnSpawn = new UnityEvent();
        
        //  Properties ------------------------------------
        public bool IsInitialized { get; private set; }
        public IContext Context { get; private set; }

        private Text BodyText { get { return _bodyText;}}
        
        //  Fields ----------------------------------------

        [SerializeField] 
        private Button _spawnToggleButton;
        
        [SerializeField] 
        private Text _bodyText;
        

        
        //  Initialization  -------------------------------
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                Context = context;
                
                //
                Context.CommandManager.AddCommandListener<IdCounterChangedCommand>(
                    OnCounterChangedCommand);
                
                _spawnToggleButton?.onClick.AddListener(
                    SpawnToggleButton_OnClicked);
            }
        }

        
        public void RequireIsInitialized()
        {
            if (!IsInitialized)
            {
                throw new Exception("MustBeInitialized");
            }
        }
        
        
        //  Unity Methods ---------------------------------
        

        
        //  Methods ---------------------------------------
        
        
        //  Event Handlers --------------------------------

        public void SpawnToggleButton_OnClicked()
        {
            Debug.Log("ButtonClicked");
            OnSpawn.Invoke();
        }
        
        private void OnCounterChangedCommand(IdCounterChangedCommand idCounterChangedCommand)
        {
            RequireIsInitialized();
            
            BodyText.text = $"{idCounterChangedCommand.CurrentValue}";
        }
        
    }