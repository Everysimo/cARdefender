using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Model;
using UnityEngine;

public class GameManagerModel : BaseModel
{
    //  Events ----------------------------------------


    //  Properties ------------------------------------

    public Observable<int> score
    {
        get { return _score; }
    }

    public Observable<int> livesLost
    {
        get { return _livesLost; }
    }

    //  Fields ----------------------------------------

    private readonly Observable<int> _score = new Observable<int>();
    
    private readonly Observable<int> _livesLost = new Observable<int>();


    //  Initialization  -------------------------------
    public override void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            base.Initialize(context);

            // Set Defaults
            _score.Value = 0;
            _livesLost.Value = 0;
        }
    }

    //  Methods ---------------------------------------

    //  Event Handlers --------------------------------
}