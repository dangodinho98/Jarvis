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
using System.IO;
using System.Speech;

namespace Jarvis
{
    public partial class Jarvis : Form
    {
        //Forms
        private SelecionarVoz selecionarVoz = null;

        private SpeechRecognitionEngine engine;
        private bool IsJarvisListening = true;

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
                commands.Add(GrammarRules.WhatDateIs.ToArray());
                commands.Add(GrammarRules.JarvisStartListening.ToArray());
                commands.Add(GrammarRules.JarvisStopListening.ToArray());
                commands.Add(GrammarRules.Minimize.ToArray());
                commands.Add(GrammarRules.Maximize.ToArray());
                commands.Add(GrammarRules.ChangeVoice.ToArray());

                GrammarBuilder gbcommands = new GrammarBuilder();
                gbcommands.Append(commands);

                Grammar g_commands = new Grammar(gbcommands);
                g_commands.Name = "sys";

                engine.LoadGrammar(g_commands);//carregar gramática

                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Rec);
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(Engine_AudioLevelUpdated);
                engine.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(Rej);

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
        private void Rec(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text; //string reconhecida
            float conf = e.Result.Confidence;

            string Data = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString()
                + "_" + DateTime.Now.Year.ToString();

            string log_filename = "Log\\" + Data + ".txt";

            StringWriter sw = new StringWriter();


            if (File.Exists(log_filename))
            {
                sw.Write("Jarvis: " + speech);
            }
            else
            {
                sw.Write("Jarvis: " + speech);
            }
            sw.Close();


            if (conf > 0.35f)
            {
                this.label1.BackColor = Color.DarkCyan;
                this.label1.ForeColor = Color.LawnGreen;

                if (GrammarRules.JarvisStopListening.Any(x => x == speech))
                {
                    IsJarvisListening = false;
                    Speaker.Speech("Irei dormir.");
                }
                else if (GrammarRules.JarvisStartListening.Any(x => x == speech))
                {
                    IsJarvisListening = true;
                    Speaker.Speech("Estou a disposição", "Olá", "Estou aqui", "Em que posso ajudar?");
                }

                if (IsJarvisListening)
                {
                    switch (e.Result.Grammar.Name)
                    {
                        case "sys":
                            if (GrammarRules.WhatTimeIS.Any(x => x == speech))
                            {
                                Runner.WhatTimeIs();
                            }
                            else if (GrammarRules.WhatDateIs.Any(x => x == speech))
                            {
                                Runner.WhatDateIs();
                            }
                            else if (GrammarRules.Maximize.Any(x => x == speech))
                            {
                                Maximize();
                            }
                            else if (GrammarRules.Minimize.Any(x => x == speech))
                            {
                                Minimize();
                            }
                            else if (GrammarRules.ChangeVoice.Any(x => x == speech))
                            {
                                if (selecionarVoz == null || selecionarVoz.IsDisposed == true)
                                    selecionarVoz = new SelecionarVoz();

                                selecionarVoz.Show();
                            }
                            break;
                    }
                }
                else
                {
                    this.label1.ForeColor = Color.Red;
                }
            }

        }

        private void Engine_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = e.AudioLevel;
        }

        private void Rej(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            this.label1.ForeColor = Color.Red;
        }

        private void Minimize()
        {
            if (this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Minimized;
                Speaker.Speech("Minimizando a janela", "Como quiser", "Tudo bem", "Como queira");
            }
            else
            {
                Speaker.Speech("Já fiz isso", "Estou descansando como pediu", "A janela está minimizada");
            }
        }
        private void Maximize()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                Speaker.Speech("Farei isso", "Como quiser", "Tudo bem", "Como queira", "É pra já");
            }
            else
            {
                Speaker.Speech("Já fiz isso", "Já estou de volta", "Estou pronto");
            }
        }



    }
}
