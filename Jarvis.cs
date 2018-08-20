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
            Speaker.Speech("Arquivos carregados.");
        }

        private void LoadSpeech()
        {
            try
            {
                engine = new SpeechRecognitionEngine(); //instância
                engine.SetInputToDefaultAudioDevice(); //microfone

                Choices commands = new Choices();
                commands.Add(GrammarRules.WhatTimeIS.ToArray());

                GrammarBuilder gbcommands = new GrammarBuilder();
                gbcommands.Append(commands);

                Grammar g_commands = new Grammar(gbcommands);
                g_commands.Name = "sys";

                engine.LoadGrammar(g_commands);//carregar gramática

                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(rec);
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(Engine_AudioLevelUpdated);
                engine.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs> (rej);

                engine.RecognizeAsync(RecognizeMode.Multiple); //Inicia o reconhecimento

                Speaker.Speech("Estou carregando os arquivos");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao carregar a DLL Microsoft.Speech: " + ex.Message);
                throw;
            }

        }       

        //Quando algo é reconhecido
        private void rec(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text; //string reconhecida
            float conf = e.Result.Confidence;

            if (conf > 0.35f)
            {
                this.label1.BackColor = Color.DarkGray;
                this.label1.ForeColor = Color.Green;

                switch (e.Result.Grammar.Name)
                {
                    case "sys":
                        if (GrammarRules.WhatTimeIS.Any(x=> x == speech))
                        {
                            Runner.WhatTimeIs();
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                this.label1.ForeColor = Color.Red;
            }
        }

        private void Engine_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = e.AudioLevel;
        }

        private void rej(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            this.label1.ForeColor = Color.Red;
        }
    }
}
