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

            _hotDogListView.ItemClick += _hotDogListView_ItemClick;
        }

        private void _hotDogListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var hotDog = _hotDogs[e.Position];

            var intent = new Intent();
            intent.SetClass(this, typeof(HotDogDetailActivity));
            intent.PutExtra("selectedHotDogId", hotDog.HotDogId);
            StartActivityForResult(intent, 100);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok && requestCode == 100)
            {
                var selectedHotDog = _hotDogDataService.GetHotDogById(data.GetIntExtra("selectedHotDogId", 0));

                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Confirmation");
                dialog.SetMessage(string.Format("You've added {0} time(s) the {1}", data.GetIntExtra("amount", 0), selectedHotDog.Name));
                dialog.Show();
            }
        }
    }
}