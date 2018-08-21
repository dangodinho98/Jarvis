using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis
{
   public class GrammarRules
    {
        public static IList<string> WhatTimeIS = new List<String>()
        {
            "Que horas são",
            "Me diga as horas",
            "Poderia me dizer que horas são"
        };

        public static IList<string> WhatDateIs = new List<String>()
        {
            "Data de hoje",
            "Qual é a data de hoje",
            "Você sabe me dizer que dia é hoje",
            "Você sabe que dia é hoje"
        };

        public static IList<string> JarvisStartListening = new List<String>()
        {
            "Acorde",
            "Acordar",
            "Iniciar",
            "Jarvis",
            "Você está ai",
            "Olá  Jarvis",
            "Oi Jarvis"
        };

        public static IList<string> JarvisStopListening = new List<String>()
        {
            "Silêncio",
            "Pare de ouvir",
            "Pare de me ouvir",
            "Desligar",
            "Aguarde",
            "Espere",
            "Dormir"
        };

        public static IList<string> Minimize = new List<String>()
        {
            "Minimizar janela",
            "Minimize a janela",
            "Privacidade por favor",
            "Descanse"
        };

        public static IList<string> Maximize = new List<String>()
        {
            "Maximize janela",
            "Maximize a janela",            
            "Prepare-se",
            "Fique pronto",
            "Apareça"
        };

        public static IList<string> ChangeVoice = new List<String>()
        {
            "Alterar a voz",
            "Altere a voz"
        };
    }
}
