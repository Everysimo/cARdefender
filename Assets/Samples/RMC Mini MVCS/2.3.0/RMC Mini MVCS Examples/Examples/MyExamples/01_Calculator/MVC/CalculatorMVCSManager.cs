using System;
using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using UnityEngine;

public class CalculatorMVCSManager : IMiniMvcs
{
   
        //  Events ----------------------------------------
        //  Properties ------------------------------------
        public bool IsInitialized { get { return _isInitialized;} }
        public Context Context { get { return _context;} }
        public CalculatorModel RollABallModel { get { return _calculatorModel;} }
        public CalculatorView InputView { get { return _calculatorView;} }
        public CalculatorController RollABallController { get { return _calculatorController;} }
        public CalculatorService RollABallService { get { return _calculatorService;} }
        
        //  Fields ----------------------------------------
 
        private bool _isInitialized = false;
        
        //Context
        private Context _context;
        
        //Model
        private CalculatorModel _calculatorModel;
        
        //View
        private CalculatorView _calculatorView;

        
        //Controller
        private CalculatorController _calculatorController;
        
        //Service
        private CalculatorService _calculatorService;


        //  Initialization  -------------------------------
        public CalculatorMVCSManager(CalculatorView calculatorView)
        {
            _calculatorView = calculatorView;
        }
        
        
        //  Initialization --------------------------------
        public void Initialize()
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                
                //Context
                _context = new Context();
                
                //Model
                _calculatorModel = new CalculatorModel();
   
                //Service
                _calculatorService = new CalculatorService();
                
                //Controller
                _calculatorController = new CalculatorController(
                    _calculatorModel, 
                    _calculatorView,
                    _calculatorService);
            
                //Model
                _calculatorModel.Initialize(_context);
                
                //View
                _calculatorView.Initialize(_context);
                
                //Service
                _calculatorService.Initialize(_context);
                
              
                
                //Controller (Init this main controller last)
                _calculatorController.Initialize(_context);
            }
        }

        public void RequireIsInitialized()
        {
            if (!_isInitialized)
            {
                throw new Exception("MustBeInitialized");
            }
        }
        
        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
}