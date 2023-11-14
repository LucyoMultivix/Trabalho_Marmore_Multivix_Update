using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Marmore
{
    public class Bloco
    {
        private int codigo;
        private int num_bloco;
        private string descricao;
        private string pedreira;
        private string material;
        private double valor_compra;
        private double valor_venda;
        private double metros_cubicos;

        public int Codigo { get => codigo; set => codigo = value; }
        public int Num_bloco { get => num_bloco; set => num_bloco = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public string Pedreira { get => pedreira; set => pedreira = value; }
        public string Material { get => material; set => material = value; }
        public double Valor_compra { get => valor_compra; set => valor_compra = value; }
        public double Valor_venda { get => valor_venda; set => valor_venda = value; }
        public double Metros_cubicos { get => metros_cubicos; set => metros_cubicos = value; }


        //Esse metodo especifica que o contrutor da classe não segue o padrão
        public Bloco(int codigo, int num_bloco, string descricao, string material, string pedreira, double valor_compra, double valor_venda, double metros_cubicos) {
            Codigo = codigo;
            Num_bloco = num_bloco;
            Pedreira = pedreira;
            Material = material;
            Descricao = descricao;
            Valor_compra = valor_compra;
            Valor_venda = valor_venda;
            Metros_cubicos = metros_cubicos;
        }

        //Esse medoto printa na tela todas as caracteristicas do bloco em questão 
        public void info()
        {
            Console.WriteLine($"Nº{Num_bloco} :: #{Codigo} :: {Pedreira} :: {Material} :: R${Valor_compra} :: R${Valor_venda} :: {Metros_cubicos}M³ :: {Descricao}");
        }
    }
}
