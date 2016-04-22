using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Java.Lang;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Hardware.Camera2;
using Android.Util;
using Android.Hardware.Camera2.Params;
using Android.Graphics;

/*Min API lvl 21*/

namespace InterphoneSAM
{
    class CameraU
    {
        private CameraManager _cameraManager; //Camera manager
        private CameraDeviceStateCallBackU cb; //CallBack
        private string _id; //_id of the camera which is in use.
        private static string _tag = "CameraU";
        private Size[] sa;
        public string cameraLocation { get; set; }

        public CameraU(Context context, string type, Surface sv)
        {
            //Initialize
            Log.Debug(_tag, "Trying to adquire camera's informations...");
            _cameraManager = (CameraManager)context.GetSystemService(Context.CameraService); //Instance of CameraManager
            Log.Debug(_tag, "camera's informations adquired succefully !");

            sa = new Size[10];

            _id = CameraU.getCameraIdFromType(context, type);

            configureStream();

            //Create an Instance pf CameraDeviceStateCallBackU
            cb = new CameraDeviceStateCallBackU(sv,sa[0].Width,sa[0].Height);

            try
            {
                _cameraManager.OpenCamera(_id, cb, null); //Open Camera in function of type (When camera is open, go to callback OnOpened Method)
                if (type == "FRONT") { cameraLocation = "FRONTALE"; }
                else if (type == "BACK") { cameraLocation = "ARRIERE"; }
            }
            catch(CameraAccessException e)
            {
                Log.Error(_tag, "CameraAccessException : " + e.Message);
            }
            catch(SecurityException e)
            {
                Log.Error(_tag, "SecurityException : " + e.Message);
            }

        }

        /*This static method provide a simple way to obtain id of Back or Front CAMERA on a system
         It takes two arguments, Context c is the context where we want to have camera informations and string type is to know if we want back or front camera
         return the camera id if found otherwise -1*/
        public static string getCameraIdFromType(Context context, string type = "FRONT")
        {
            CameraManager  tmpCM = (CameraManager)context.GetSystemService(Context.CameraService); //Instance of CameraManager
            CameraCharacteristics tmpCC; //Instance of CameraCharacteristics

            string tmpID = "-1"; //_id

            foreach (string currentID in tmpCM.GetCameraIdList())
            {
                try
                {
                    tmpCC = tmpCM.GetCameraCharacteristics(currentID);

                    if (type == "BACK" && (int)(tmpCC.Get(CameraCharacteristics.LensFacing)) == 1) //It's back Camera
                    {
                        tmpID = currentID;
                    }
                    else if (type == "FRONT" && (int)(tmpCC.Get(CameraCharacteristics.LensFacing)) == 0) //It's front camera
                    {
                        tmpID = currentID;
                    }
                }
                catch (CameraAccessException e)
                {
                    Log.Error(_tag, "CameraAccessException : " + e.Message);
                }
                catch(IllegalArgumentException e)
                {
                    Log.Error(_tag, "IllegalArgumentException : " + e.Message);
                }
            }

            return tmpID;
        }
        public void configureStream()
        {
            CameraCharacteristics characteristics = _cameraManager.GetCameraCharacteristics(_id);
            StreamConfigurationMap configs = (StreamConfigurationMap)(characteristics.Get(CameraCharacteristics.ScalerStreamConfigurationMap));
            sa = configs.GetOutputSizes(Java.Lang.Class.FromType(typeof(SurfaceTexture)));
            //sa = configs.GetOutputSizes((int)ImageFormatType.Nv16);

            for (int i=0; i<sa.Length;i++)
            {
                System.Diagnostics.Debug.WriteLine("width : " + sa[i].Width + " Height : " + sa[i].Height + "\n");
            }
        }
    }
}