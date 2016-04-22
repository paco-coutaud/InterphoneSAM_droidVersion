using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Hardware.Camera2;
using Android.Graphics.Drawables;

namespace InterphoneSAM
{
    class CameraPreviewU : SurfaceView, ISurfaceHolderCallback
    {
        private ISurfaceHolder _surfaceHolder;
        private CameraU _camera;
        private Context _context;

        public CameraPreviewU(Context context, CameraU camera):base(context)
        {
            _context = context;
            _camera = camera;
            _surfaceHolder = Holder;

            _surfaceHolder.AddCallback(this);
            //_surfaceHolder.SetType(SurfaceType.PushBuffers);

        }

        //Implement ISurfaceHolderCallBack interface
        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
        }
    }
}
