using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace Jarvis
{
    public class Speaker
    {
        private static SpeechSynthesizer sp = new SpeechSynthesizer();

        public static void Speech(string text)
        {
            //sp.Voice.Gender = VoiceGender.Male;
            sp.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);

            if (sp.State == SynthesizerState.Speaking)
                sp.SpeakAsyncCancelAll();
            sp.SpeakAsync(text);
        }

        public static void Speech(params string[] texts)
        {
            Random rnd = new Random();
            Speech(texts[rnd.Next(0, texts.Length)]);
        }
        public static void SetVoice(string voice)
        {
            try
            {
                sp.SelectVoice(voice);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Erro no Speaker" + ex.Message);
                //TODO: ADICIONAR NO LOG
                throw;
            }
        }

    }
}
