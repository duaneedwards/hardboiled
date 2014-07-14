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