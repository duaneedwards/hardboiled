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

    let progress = this.FindViewById<RadialProgress.RadialProgressView>(Resource_Id.progressView)
    progress.LabelHidden <- true;
    let twelveminutes = 720.0f
    progress.MaxValue <- twelveminutes
    let restTimer = new System.Timers.Timer(1000.0)
    restTimer.Elapsed.Add(fun _ -> 
        progress.Value <- progress.Value + 1.0f
        if progress.Value = twelveminutes
        then 
            ( 
                restTimer.Stop() 
                this.StartActivity(typeof<DoneActivity>) 
            )
    )
    restTimer.Start()

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
    progress.LabelHidden <- true;
    let boilProgressText = this.FindViewById<TextView>(Resource_Id.boilProgressText)
    let threeminutes = 180.0f
    progress.MaxValue <- threeminutes

    let boilTimer = new System.Timers.Timer(1000.0)
    boilTimer.Elapsed.Add(fun _ -> 
        progress.Value <- progress.Value + 1.0f
        let timeSpan = TimeSpan.FromSeconds((float threeminutes) - (float progress.Value))
        this.RunOnUiThread(fun _ -> boilProgressText.Text <- String.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds))
        if progress.Value = threeminutes
        then
            (
                boilTimer.Stop()
                this.StartActivity(typeof<RestActivity>)
            )
    )
    boilTimer.Start()

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

