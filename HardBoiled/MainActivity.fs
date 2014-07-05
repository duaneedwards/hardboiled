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

[<Activity (Label = "RestActivity")>]
type RestActivity() =
  inherit Activity()

  override this.OnCreate(bundle) =
    base.OnCreate (bundle)

    this.SetContentView (Resource_Layout.Rest)

    let button = this.FindViewById<Button>(Resource_Id.btnNext)
    button.Click.Add (fun args -> 
        this.StartActivity(typeof<DoneActivity>)
    )

[<Activity (Label = "BoilActivity")>]
type BoilActivity() =
  inherit Activity()

  override this.OnCreate(bundle) =
    base.OnCreate (bundle)

    this.SetContentView (Resource_Layout.Boil)

    let button = this.FindViewById<Button>(Resource_Id.btnNext)
    button.Click.Add (fun args -> 
        this.StartActivity(typeof<RestActivity>)
    )

[<Activity (Label = "TurnOnActivity")>]
type TurnOnActivity() =
  inherit Activity()

  override this.OnCreate(bundle) =
    base.OnCreate (bundle)

    this.SetContentView (Resource_Layout.TurnOn)

    let button = this.FindViewById<Button>(Resource_Id.btnNext)
    button.Click.Add (fun args -> 
        this.StartActivity(typeof<BoilActivity>)
    )

[<Activity (Label = "Hard Boiled", MainLauncher = true)>]
type MainActivity () =
    inherit Activity ()

    override this.OnCreate (bundle) =

        base.OnCreate (bundle)

        this.SetContentView (Resource_Layout.Prepare)

        let button = this.FindViewById<Button>(Resource_Id.btnNext)
        button.Click.Add (fun args -> 
            this.StartActivity(typeof<TurnOnActivity>)
        )

