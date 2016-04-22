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

namespace InterphoneSAM
{
    class SurfaceTextureListenerU : TextureView.ISurfaceTextureListener
    {
        Surface m;
        public SurfaceTextureListenerU()
        {
        }

        public IntPtr Handle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
        {
            m = new Surface(surface);
            m.SetSize(width, height);
        }

        public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
        {
            throw new NotImplementedException();
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
            
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}