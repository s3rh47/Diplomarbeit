using System;
using Google.Cloud.TextToSpeech.V1;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Google.Apis.Auth.OAuth2;
using Grpc.Auth;

namespace Hortrainingsprogramm.Practice_and_Quiz_Menu.Model
{

    public class GoogleTextToSpeech
    {
        private string textToSpeech;
        private string selectedLanguage;
        private string selectedVoice;
        private int generatedCount = 1;
        private double speakingRate = 1;
        private double pitchValue = 0;
        private string textToSpeechHolder;
        private string selectedLanguageHolder;
        private string selectedVoiceHolder;
        private double speakingRateHolder;
        private double pitchValueHolder;
    


        private TextToSpeechClient client;
        private readonly VoiceSelectionParams voiceSelection = new();
        private readonly SynthesisInput input = new();
        private readonly AudioConfig audioConfig = new();



        private TextToSpeechClient createTextToSpeechClient()
        {

            var credentialJson = new
            {
                type = "service_account",
                project_id = "practical-bolt-327301",
                private_key_id = "fe9319f3ab06bafcf8ca26051cc8fc0e79ca521c",

                private_key = "-----BEGIN PRIVATE KEY-----\n" +
                "MIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCWrnp4ogTs+ICH\n" +
                "OSwQUL5cTqK9SNl4xKyu3u0mFsB83HsPtJojEbroWi9JugBGACDcF6WAKEjMIh4X\n" +
                "mq2cbIgCFABC/1rNCvPanvYKuR5uNIuTTf6PAYeCBIorX1jlYrMiv6k3CnA4fZNS\n" +
                "uMXajy10P1t23h4LRlejUSkfBXjMqAkQ4MIhbIavOB0jczahLLkr9dfrawZbIdl+\n" +
                "DZ79VqEgUfZtO2R9Jv86FhmmBtCXyMvktGmsKL741gYuxc7yJTIPhimkt8ZEKCIP\n" +
                "J2q7nx6J9DOhb/Yj3G8TUXHZirLc6tcQ8QNxfgX61jMHB4tSBlO3X/YIM7DtUr4L\n" +
                "/6xZy1qzAgMBAAECggEANIgfqm7RyMvaA0qrUgTUWNoRYmxDZRcsHMsbYmwML3uu\n" +
                "a4tLMW68+sR2N/3gmobw7cpQqJ1iw8ghNZl3bVFpGVBfxvu/bDylR1Kj9JkVQtaO\n" +
                "0oweczzVQT7T7MsQ8ue2DCdFlyrMdOVSt0Qztj60WhK8kHRfhQ7A/u8MpwQBWVey\nt" +
                "lXvxx85PcI8WNmuKSRO5/7smfcvrR9FXDR2Pz7TGWtUoGx2NRF+o4+fDWVoYDA1\n" +
                "Tqi9ClKapkIuLq83EjgOA+12W7BSoZOhLFWBabZ3kjHXIFYPxxZEDgsl8oLuXMgt\n" +
                "8bHFjPOU6/cS59Yr14pLWb3GMoCbeSpbc/MQ1OfKAQKBgQDOIuK4PM5y1AcbHJM/\n" +
                "t7WU3SlE53IsfDuooHRM9tjAayhFTdDhv9LOV+CqzXgPXHrc8dD9/E1Aks7zTWdY\n" +
                "GiTyn5yUsAHu/Ie+lRqTSzwov9RXdmPgDc5i+MU/7p3gO4xIyc3ozuGRDgBMxA2B\n" +
                "6h4ZH6QiDkAJoD9lQaEgOHzgTwKBgQC7IYaTP4T5t2NFISkHX5PqFf6KIbBRN6aF\n" +
                "iFW1Hv6/czVdzxcTNyW30Y5Sje498StFup5zALaCEF8L4QokXNnhizZfyfgC0lAL\n" +
                "RoY71wVdq4r1x5U6VLt1G8+8V3baKbjJ3V64aJqkndJToSwDKDJqO9gnVamjEX32\n" +
                "NcUJh+7CXQKBgA0CVpyZ+c11ZB1p6EEVLvh+HKSklqgIDxvNRIm26mb6XEhDaPNW\n" +
                "T8+F7D/bR8BwbbCT8kSVL6rrmPPx5pvAyqQlPpSDpxuWBFsavdqd3OYVdSkv5420\n" +
                "UVodqRUn0f7cwzW0xrHynHmzEIHHvbJ9O/kJ7inYDGKPInEZezm1VuwxAoGAQSLe\n" +
                "UQrfInzvFPUB7wsZ1XVqcHJhiSOx1vEpxC3Rxpo3jTu0cH/VpTJQM/QrZWw4/8CB\n" +
                "vs4UuRkxvFTMzvNy88sdViJbLcA/FG2r89BYkc5QRFUAYJl11sGjgY+AU1gKdlmT\n" +
                "yuS+T2aP+4QyabboNYo3JxnuPlCsY7M7rqGePhECgYAdHooozHs4g0bMOW/kCoFr\n" +
                "ECxmIA9ld4eMlHVtSS87dkbQWTesgnkFgUJiiAWlCqTW0s1sW1rhMhJA8hG0CKeB\n" +
                "/6jTNIj02Kc1+3k0o25tFlLfpltFsL+w3nLMm0KYctZegMjV7EYQNINMI7OXewBE\n" +
                "W75C3UpmQomKb3hLgulTAA==\n" +
                "-----END PRIVATE KEY-----\n",

                client_email = "serhat-karadag@practical-bolt-327301.iam.gserviceaccount.com",
                client_id = "110658281679232253894",
                auth_uri = "https://accounts.google.com/o/oauth2/auth",
                token_uri = "https://oauth2.googleapis.com/token",
                auth_provider_x509_cert_url = "https://www.googleapis.com/oauth2/v1/certs",
                client_x509_cert_url = "https://www.googleapis.com/robot/v1/metadata/x509/serhat-karadag%40practical-bolt-327301.iam.gserviceaccount.com"
            };


            var credential = GoogleCredential.FromJson(JsonConvert.SerializeObject(credentialJson))
                .CreateScoped(TextToSpeechClient.DefaultScopes);
            var client = new TextToSpeechClientBuilder
            {
                ChannelCredentials = credential.ToChannelCredentials()
            }.Build();

            return client;
        }



        public GoogleTextToSpeech()
        {

            client = createTextToSpeechClient();

        }


        public void synthesisOptions(string selectedLanguage, string selectedVoice, double speakingRate, double pitchValue)
        {

            this.selectedLanguage = selectedLanguage;
            this.selectedVoice  = selectedVoice;
            this.speakingRate = speakingRate;
            this.pitchValue = pitchValue;
            voiceSelection.LanguageCode = selectedLanguage;
            voiceSelection.Name = selectedVoice;

                

        }


        public void inputText(string textToSpeech)
        {
            input.Text = textToSpeech;
            this.textToSpeech = textToSpeech;

        }

        public MemoryStream getSoundContent()
        {

            if(textToSpeech != textToSpeechHolder || selectedLanguage != selectedLanguageHolder || selectedVoice != selectedVoiceHolder || speakingRate != speakingRateHolder || pitchValue != pitchValueHolder)
            {

                textToSpeechHolder = textToSpeech;
                selectedLanguageHolder = selectedLanguage;
                selectedVoiceHolder = selectedVoice;
                speakingRateHolder = speakingRate;
                pitchValueHolder = pitchValue;
   


                audioConfig.AudioEncoding = AudioEncoding.Linear16;
                audioConfig.SpeakingRate = speakingRate;
                audioConfig.Pitch = pitchValue;
                


                var response = client.SynthesizeSpeech(input, voiceSelection, audioConfig);

                AllocConsole();

                Console.WriteLine("Audio content generated new. Times: " + generatedCount++);

                return new MemoryStream(response.AudioContent.ToByteArray());

            }
            else
            {
                return null;
            }

        }


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();


    }


}




