/*Definir a Pot�ncia, Tempo, Configura��o do Aquecimento R�pido */

using System;
using System.Threading.Tasks;
using MicroondasApp.ViewModels;

namespace microondas.fontes
{
    public class MicroOndas : IEMicroondas, IIMicroondas
    {
        public Settings Settings { get; set; }
        public Controller Controller { get; set; }

        public MicroOndas()
        {
            Settings = new Settings();
            Controller = new Controller();
        }

        public void DefinirPotencia(int potencia)
        {
            if (potencia < 1)
                throw new Exception("A pot�ncia n�o pode ser 0");
            if (potencia > 10)
                throw new Exception("A pot�ncia n�o pode ser maior do que 10");
        }

        public void DefinirTempo(DateTime tempodef)
        {
            var tempo = tempodef.Minute * 60 + tempodef.Second;
            if (tempo > 120)
                throw new Exception("O tempo n�o pode ser maior que 2 minutos");
            if(tempo < 1)
                throw new Exception("O tempo n�o pode ser menor que 1 segundo");
            if (tempo == 0)
                throw new ArgumentException();
        }

        public void ConfigurarAqueceRapido()
        {
            this.Settings.ZerarTempo();
            this.Settings.AdicionarTempo(30);
            this.Settings.Potencia = 8;
        }

        public void Executar(Settings Settings)
        {
            this.Settings = Settings;
            if (String.IsNullOrEmpty(this.Settings.Programa))
            {
                try
                {
                    DefinirTempo(this.Settings.tempodef);
                    DefinirPotencia(this.Settings.Potencia);
                }
                catch (ArgumentException)
                {
                    ConfigurarAqueceRapido();
                }
            }
        }
    }
}