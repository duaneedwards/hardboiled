namespace HardBoiled

open System

open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget

[<Activity (Label = "Preparation", MainLauncher = true)>]
type MainActivity () =
    inherit Activity ()

    override this.OnCreate (bundle) =

        base.OnCreate (bundle)

        this.SetContentView (Resource_Layout.Prepare)

        let button = this.FindViewById<Button>(Resource_Id.btnNext)
        button.Click.Add (fun args -> 
            this.StartActivity(typeof<TurnOnActivity>)
        )

