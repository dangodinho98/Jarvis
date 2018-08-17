using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jarvis
{
    public partial class Jarvis : Form
    {
        private SpeechRecognitionEngine engine;

        public Jarvis()
        {
            InitializeComponent();
        }

        private void Jarvis_Load(object sender, EventArgs e)
        {
            LoadSpeech();
        }

        private void LoadSpeech()
        {
            try
            {
                engine = new SpeechRecognitionEngine();
                engine.SetInputToDefaultAudioDevice();//microfone

                string[] words = { "Olá", "Oi" };

                //carregar gramática

                engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(words))));


                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs> (Engine_SpeechRecognized);
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs> (Engine_AudioLevelUpdated);

               engine.RecognizeAsync(RecognizeMode.Multiple); //Inicia reconhecimento

                Speaker.Speech("Estou carregando os arquivos");



            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao carregar a DLL Microsoft.Speech: " + ex.Message);
                throw;
            }

        }

        private void Engine_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = e.AudioLevel;
        }

        //Quando algo nao é reconhecido
        private void Engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //MessageBox.Show(e.Result.Text);
        }
    }
}
