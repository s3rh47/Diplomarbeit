using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Hortrainingsprogramm.Practice_and_Quiz_Menu.Model
{

    public class AudioPlayer
    {
        private IWavePlayer waveOut;
        private WaveStream waveStream;
        private MMDeviceEnumerator deviceEnumerator;
        private MMDevice defaultDevice;
        public event EventHandler PlaybackStopped;



        public AudioPlayer()
        {
            deviceEnumerator = new MMDeviceEnumerator();
            defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
        }



        //public void PlayAudio(MemoryStream memoryStream)
        //{
        //    StopAudio();

        //    var waveFileReader = new WaveFileReader(memoryStream);
        //    waveStream = new WaveFormatConversionStream(waveFileReader.WaveFormat, waveFileReader);
        //    waveOut = new WaveOut();
        //    waveOut.Init(waveStream);
        //    waveOut.PlaybackStopped += OnPlaybackStopped;

        //    waveOut.Play();

        //    TimeSpan duration = waveStream.TotalTime;

        //    getDuration = duration.TotalMilliseconds;

        //}




        public double GetDuration()
        {
            TimeSpan duration = waveStream.TotalTime;
            return duration.TotalMilliseconds;
        }



        public void SetContent(MemoryStream memoryStream)
        {
            var waveFileReader = new WaveFileReader(memoryStream);
            waveStream = new WaveFormatConversionStream(waveFileReader.WaveFormat, waveFileReader);
        }



        //Diese Methode sorgt Verzögerung bis die Sound fertig abspielt.

        public async Task PlayAudioAsync()
        {
            StopAudio();

            waveOut = new WaveOut();
            waveOut.Init(waveStream);


            var playbackFinished = new TaskCompletionSource<bool>();

            waveOut.PlaybackStopped += (s, e) =>
            {
                playbackFinished.TrySetResult(true);
                PlaybackStopped?.Invoke(this, EventArgs.Empty);
            };


            waveOut.Play();

            await playbackFinished.Task;
        }



        public double GetCurrentPlaybackPosition()
        {
            if (waveStream != null)
            {
                return waveStream.Position * 1000.0 / waveStream.WaveFormat.AverageBytesPerSecond;
            }
            return 0;
        }


        public void StopAudio()
        {
            waveOut?.Stop();
            waveOut?.Dispose();
            waveOut = null;
        }



        public async Task ReplayAudio()
        {
            if (waveStream == null)
            {
                return;
            }


            waveStream.Position = 0;

            var playbackFinished = new TaskCompletionSource<bool>();

            waveOut.PlaybackStopped += (s, e) =>
            {
                playbackFinished.TrySetResult(true);
                PlaybackStopped?.Invoke(this, EventArgs.Empty);
            };


            waveOut.Play();

            await playbackFinished.Task;

        }


        public void SetWindowsVolume(int volume)
        {
            if (volume < 0) volume = 0;
            if (volume > 100) volume = 100;

            float volumeLevel = volume / 100f;
            defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volumeLevel;
        }



        public int GetWindowsVolume()
        {
            float volumeLevel = defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
            int volume = (int)(volumeLevel * 100);
            return volume;
        }


    }


}
