using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarvelApi
{
    class Resultados
    {   
        StreamWriter escritor;
        public void SalvarArquivo()
        {
            escritor.Flush();
        }
        public string criarArquivo()
        {   
            //Criação do arquivo de resultado com data e hora da criação (A pasta já deve estar criada no caminho abaixo)
            string nome = $@"c:\api\marvel-{DateTime.Now.ToString("dd_MM_yyyy__HH_mm_ss")}.txt";
            escritor = File.CreateText(nome);  
            //Retorna o nome do arquivo para manipulações futuras
            return nome;
        }
        public void abrirArquivo(string caminho)
        {
            escritor = new StreamWriter(File.Open(caminho, FileMode.Open));
        }
        public void escrever(string texto)
        {
            escritor.WriteLine(texto);
        }
        public void fecharArquivo()
        {
            escritor.Close();
        }
        
    }
}
