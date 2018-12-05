using Android.App;
using Android.Widget;
using Android.OS;
using Com.Example.Testlib;
//using IO.Reactivex;
//using Com.Squareup.Duktape;


namespace BindingTest
{
    [Activity(Label = "BindingTest", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            //TestJava test = new TestJava();
            TestKotlin testKotlin = new TestKotlin();

            ////button.Text = "wha: "+ testKotlin.ReturnSomething();
            //button.Text = "wha: " + testKotlin.TestWithArrow();
            button.Text = "huhwa: " + testKotlin.TestWithDuktape();

            //button.Text = "rx: " + Single.Just("Rx Hullo").BlockingGet();

            button.Text = "rx: " + testKotlin.TestWithRx();

            //Duktape dt = Duktape.Create();

            //button.Text = (string) dt.Evaluate("'hello world'.toUpperCase();");

            //button.Text = "hey";

            button.Click += delegate { button.Text = $"{count++} clicks!"; };
        }
    }
}

