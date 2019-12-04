using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServicoHostWCF
{
    [ServiceContract]
    interface IServico
    {
        [OperationContract]
        List<EntTesteCadastro> Consultar(string nome);
    }
}
namespace ServicoHostWCF
{
    class Servico : IServico
    {
        TesteCadastroBO boTesteCadastro;
        public List<EntTesteCadastro> Consultar(string nome)
        {
            boTesteCadastro = new TesteCadastroBO();
            EntTesteCadastro cliente = new EntTesteCadastro();
            cliente.Nome = nome;
            return boTesteCadastro.BuscarCliente(cliente);
        }
    }
}
namespace ServicoHostWCF
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ServicoHostWCF.Servico), new Uri[] { });
            host.Open();
            Console.WriteLine("Serviço rodando...");
            Console.WriteLine("Tecle para finalizar.");
            Console.ReadKey();
            host.Close();
        }
    }
}
