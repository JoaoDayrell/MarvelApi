using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace MarvelApi
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        //Inicia as variaveis necessárias para execução do serviço
        EnviarEmail email;
        Resultados escritor;
        Api api;
        Thread m_thread = null;
        int Execucoes = 0;
        //Cria a thread para execução e manipulação separada
        protected override void OnStart(string[] args)
        {
            //Inicia a thread com o metodo Rotina como definição
            m_thread = new Thread(new ThreadStart(rotina));
            m_thread.Start();
            
        }
        private void rotina()
        {
            try
            {
                //Preenche as variaveis com os valores das classes respectivas
                escritor = new Resultados();
                escritor.criarArquivo();
                email = new EnviarEmail();
                api = new Api();
                //Indica qual metodo a thread irá executar e a inicia em seguida                
                //Envia um email informando o êxito
                email.Enviar("Relatório inicio serviço Marvel API", $@"A rotina foi iniciada com sucesso as {DateTime.Now.ToString("dd_MM_yyyy__HH_mm_ss")}");

            }
            catch (Exception ex)
            {
                //Captura a exceção e a envia por email informando o erro no inicio
                email.Enviar("Relatório inicio serviço Marvel API", $@"A rotina foi iniciada com erro as {DateTime.Now.ToString("dd_MM_yyyy__HH_mm_ss")} Erro : {ex.Message}");
            }
            try
            {
                escrever("Rotina iniciada com sucesso");
                escrever("Conectando com a api, iniciando metodo GET de Personagens");
                escritor.SalvarArquivo();
                //recebe a variavel do metodo dos personagem multiplicando por 100 as execucoes para o offset nao trazer dados repitidos
                var dados = api.getPersonagens(Execucoes * 100).data.results;
                //Percorre a variavel dado gravando no LOG os dados importados;
                foreach (var dado in dados)
                {   
                    //Verifica se a descrição é vazia
                    if (dado.description == "")
                    {
                        dado.description = "Descrição importada vazia";
                    }
                    string texto = $@"Id - {dado.id} Nome - {dado.name} Descrição - {dado.description}";
                    escrever(texto);
                    escritor.SalvarArquivo();
                }
                //Envia o e-mail de sucesso
                email.Enviar("Relatório inicio serviço Marvel API", $@"A rotina foi finalizada com sucesso as {DateTime.Now.ToString("dd_MM_yyyy__HH_mm_ss")}");
                Execucoes++;
                //Thread para por 2 horas e reinicia aumentando o offset                
                Thread.Sleep(7200000);
                if(Execucoes < 15)
                {
                    rotina();

                }
                else
                {
                    email.Enviar("Relatório inicio serviço Marvel API", $@"A rotina importou todos os dados  com sucesso as {DateTime.Now.ToString("dd_MM_yyyy__HH_mm_ss")}");

                }

            }
            catch(Exception ex)
            {
                //Envia o e-mail de erro
                email.Enviar("Relatório inicio serviço Marvel API", $@"A rotina foi finalizada com erro as {DateTime.Now.ToString("dd_MM_yyyy__HH_mm_ss")} Erro : {ex.Message}");
            }
        }
        protected override void OnStop()
        {
        }

        //Metodo para que seja possivel testar o serviço em forma de console, para tratar como um serviço normal, Projeto ->Propriedades -> Tipo de saída -> aplicativo de Windows
        public  void RunAsConsole(string[] args)
        {
            OnStart(args);
            OnStop();
        }
        private void escrever (string texto)
        {
            escritor.escrever($@"{DateTime.Now.ToString("dd_MM_yyyy__HH_mm_ss")} - {texto}");
        }
    }
}
