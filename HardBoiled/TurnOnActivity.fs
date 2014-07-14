namespace HardBoiled

open System
open System.Collections.Generic
open System.Linq
open System.Text

open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget

[<Activity (Label = "Turn On Your Stove")>]
type TurnOnActivity() =
  inherit Activity()

  override this.OnCreate(bundle) =
    base.OnCreate (bundle)

    this.SetContentView (Resource_Layout.TurnOn)

    let button = this.FindViewById<Button>(Resource_Id.btnNext)
    button.Click.Add (fun args -> 
        this.StartActivity(typeof<BoilActivity>)
    )