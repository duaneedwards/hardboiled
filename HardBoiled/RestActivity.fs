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

[<Activity (Label = "Resting the eggs")>]
type RestActivity() =
  inherit Activity()

  override this.OnCreate(bundle) =
    base.OnCreate (bundle)

    this.SetContentView (Resource_Layout.Rest)

    let progress = this.FindViewById<RadialProgress.RadialProgressView>(Resource_Id.progressView)
    progress.LabelHidden <- true;
    let restProgressText = this.FindViewById<TextView>(Resource_Id.restProgressText)
    let twelveminutes = 720.0f
    progress.MaxValue <- twelveminutes

    let restTimer = new System.Timers.Timer(1000.0)
    restTimer.Elapsed.Add(fun _ -> 
        progress.Value <- progress.Value + 1.0f
        let timeSpan = TimeSpan.FromSeconds((float twelveminutes) - (float progress.Value))
        this.RunOnUiThread(fun _ -> restProgressText.Text <- String.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds))
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