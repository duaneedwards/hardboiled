namespace HardBoiled

open System

open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget

[<Activity (Label = "DoneActivity")>]
type DoneActivity() =
  inherit Activity()

  override this.OnCreate(bundle) =
    base.OnCreate (bundle)

    this.SetContentView (Resource_Layout.Done)

    let button = this.FindViewById<Button>(Resource_Id.btnImDone)
    button.Click.Add (fun args -> 
        button.Text <- "You're Done!"
    )

[<Activity (Label = "Hard Boiled", MainLauncher = true)>]
type MainActivity () =
    inherit Activity ()

    let mutable count:int = 1

    override this.OnCreate (bundle) =

        base.OnCreate (bundle)

        this.SetContentView (Resource_Layout.Main)

        let button = this.FindViewById<Button>(Resource_Id.myButton)
        button.Click.Add (fun args -> 
            this.StartActivity(typeof<DoneActivity>)
            //this.SetContentView(Resource_Layout.Done)
        )

