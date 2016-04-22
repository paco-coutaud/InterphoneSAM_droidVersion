using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

namespace InterphoneSAM
{
    [Activity(Label = "InterphoneSAM", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, TextureView.ISurfaceTextureListener
    {
        CameraU _mCamera;
        TextureView _mCameraView;
        Surface sf;
        TextView tv;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            tv = FindViewById<TextView>(Resource.Id.cameraLocation);
            _mCameraView = FindViewById<TextureView>(Resource.Id.cameraView);
            _mCameraView.SurfaceTextureListener = this;
        }

        public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height) //When SurfaceTexture is available
        {
            sf = new Surface(surface);
            _mCamera = new CameraU(this, "FRONT", sf);
            tv.Text = "La caméra " + _mCamera.cameraLocation + " est ouverte !";
        }

        public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
        {
            return true;
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
            sf.SetSize(width, height);
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
        }
    }
}

