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

namespace InterphoneSAM
{
    class CameraCaptureSessionCallbackU : CameraCaptureSession.StateCallback
    {
        private CameraCaptureSession _session;
        private CaptureRequest.Builder _builder;
        public CameraCaptureSessionCallbackU(CaptureRequest.Builder b)
        {
            _builder = b;
        }
        public override void OnConfigured(CameraCaptureSession session)
        {
            _session = session;
            _builder.Set(CaptureRequest.ControlMode,null);
            HandlerThread thread = new HandlerThread("CameraPreview");
            thread.Start();
            Handler backgroundHandler = new Handler(thread.Looper);
            _session.SetRepeatingRequest(_builder.Build(), null, backgroundHandler);

            System.Diagnostics.Debug.WriteLine("Session configurée");
        }

        public override void OnConfigureFailed(CameraCaptureSession session)
        {
            System.Diagnostics.Debug.WriteLine("Configure failed !");
        }
    }
}