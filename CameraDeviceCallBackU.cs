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
using Android.Hardware.Camera2;
using Android.Graphics;

namespace InterphoneSAM
{
    class CameraDeviceStateCallBackU : CameraDevice.StateCallback
    {
        private Surface _sv;
        private CameraCaptureSessionCallbackU _ccssc; //Camera Capture Session CallBack 
        private List<Surface> ls;
        private CaptureRequest.Builder _builder;

        public CameraDeviceStateCallBackU(Surface sv, int w, int h)
        {
            _sv = sv;
            System.Diagnostics.Debug.WriteLine("Surface valide ? " + _sv.IsValid);
            ls = new List<Surface>();
            ls.Add(_sv);
            //_ccssc = new CameraCaptureSessionCallbackU();
        }
        //Reimplements methods
        public override void OnDisconnected(CameraDevice camera)
        {
            throw new NotImplementedException();
        }

        public override void OnError(CameraDevice camera, [GeneratedEnum] CameraError error)
        {
        }

        public override void OnOpened(CameraDevice camera)
        {
            System.Diagnostics.Debug.WriteLine("Camera ouverte");

            _builder = camera.CreateCaptureRequest(CameraTemplate.Preview);
            _builder.AddTarget(_sv);
            _ccssc = new CameraCaptureSessionCallbackU(_builder);
            camera.CreateCaptureSession(ls, _ccssc, new Handler());
        }
    }
}