using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Model;
using UnityEngine;

//  Namespace Properties ------------------------------

//  Class Attributes ----------------------------------

/// <summary>
/// The Model stores runtime data 
/// </summary>
public class GunModel : IModel
{
    //  Events ----------------------------------------


    //  Properties ------------------------------------
    public bool IsInitialized { get { return _isInitialized;} }
    public IContext Context { get { return _context;} }
        
    //  Fields ----------------------------------------
    private bool _isInitialized = false;
        
    //Context
    private IContext _context;
    
    public Observable<int> MaxAmmo { get { return _maxAmmo;} }
    
    public Observable<int> ActualAmmo { get { return _actualAmmo;} }
    
    public Observable<float> ReloadSpeed { get { return _reloadSpeed;} }
    
    public Observable<float> ShootDamage { get { return _shootDamage;} }
    public Observable<float> ShootSpeed { get { return _shootSpeed;} }
    
    public Observable<GameObject> ProjectilePrefab { get { return _projectilePrefab;} }

    public Observable<Transform> StartPoint { get { return _startPoint;} }
    
        
    //  Fields ----------------------------------------
    private readonly Observable<float> _reloadSpeed = new Observable<float>();
    private readonly Observable<float> _shootSpeed = new Observable<float>();
    private readonly Observable<float> _shootDamage = new Observable<float>();
    
    private readonly Observable<int> _maxAmmo = new Observable<int>();
    private readonly Observable<int> _actualAmmo = new Observable<int>();
    
    private readonly Observable<GameObject> _projectilePrefab = new Observable<GameObject>();
    
    private readonly Observable<Transform> _startPoint = new Observable<Transform>();

        
    //  Initialization  -------------------------------
    public void Initialize(IContext context) 
    {
        if (!IsInitialized)
        {
            _isInitialized = true;
            _context = context;

            // Set Defaults
            _maxAmmo.Value = 40;
        }
    }

    public void RequireIsInitialized()
    {
        throw new System.NotImplementedException();
    }
    

    //  Methods ---------------------------------------

    public void SetGunStats(int maxAmmo,int actualAmmo, float reloadSpeed,float shootDamage, float shootSpeed)
    {

        _maxAmmo.Value = maxAmmo;
        _actualAmmo.Value = actualAmmo;
        _reloadSpeed.Value = reloadSpeed;
        _shootDamage.Value = shootDamage;
        _shootSpeed.Value = shootSpeed;
        
    }
    

    //  Event Handlers --------------------------------

}
