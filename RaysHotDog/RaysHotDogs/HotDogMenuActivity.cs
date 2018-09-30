using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RaysHotDogs.Adapters;
using RaysHotDogs.Core.Model;
using RaysHotDogs.Core.Service;

namespace RaysHotDogs
{
    [Activity(Label = "HotDogMenuActivity")]
    public class HotDogMenuActivity : Activity
    {
        private ListView _hotDogListView;
        private List<HotDog> _hotDogs;
        private HotDogDataService _hotDogDataService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HotDogMenuView);

            _hotDogDataService = new HotDogDataService();
            _hotDogs = _hotDogDataService.GetAllHotDogs();

            _hotDogListView = FindViewById<ListView>(Resource.Id.hotDogListView);
            _hotDogListView.Adapter = new HotDogListAdapter(this, _hotDogs);
            _hotDogListView.FastScrollEnabled = true;
        }
    }
}