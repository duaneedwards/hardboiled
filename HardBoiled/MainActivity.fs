namespace HardBoiled

open System

open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget

[<Activity (Label = "Done!")>]
type DoneActivity() =
  inherit Activity()

  override this.OnCreate(bundle) =
    base.OnCreate (bundle)

    this.SetContentView (Resource_Layout.Done)

    let button = this.FindViewById<Button>(Resource_Id.btnImDone)
    button.Click.Add (fun args -> 
        button.Text <- "You're Done!"
    )

[<Activity (Label = "Resting the eggs")>]
type RestActivity() =
  inherit Activity()

  override this.OnCreate(bundle) =
    base.OnCreate (bundle)

    this.SetContentView (Resource_Layout.Rest)

    let button = this.FindViewById<Button>(Resource_Id.btnNext)
    button.Click.Add (fun args -> 
        this.StartActivity(typeof<DoneActivity>)
    )

[<Activity (Label = "Boiling the eggs")>]
type BoilActivity() =
  inherit Activity()

  override this.OnCreate(bundle) =
    base.OnCreate (bundle)

    this.SetContentView (Resource_Layout.Boil)

    let progress = this.FindViewById<RadialProgress.RadialProgressView>(Resource_Id.progressView)

    let timer = new System.Timers.Timer(1000.0)
    timer.Elapsed.Add(fun _ -> 
        progress.Value <- progress.Value + 1.0f
        if progress.Value = 180.0f
        then
        (
            timer.Stop()
            this.StartActivity(typeof<RestActivity>)
        )
    )
    timer.Start()

    let button = this.FindViewById<Button>(Resource_Id.btnNext)
    button.Click.Add (fun args -> 
        this.StartActivity(typeof<RestActivity>)
    )

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

