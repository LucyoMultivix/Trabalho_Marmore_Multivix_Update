using System.ComponentModel;
using Trabalho_Marmore;
//Usandi System eu posso utilizar strings (Claro que eu poderia apenas usar System.string, mas System é bem amplo e já inclui o que eu preciso)
using System;
//Usando System.IO eu meio que não vou precisar usar sw.Close ou sr.Close()
using System.IO;

int esc = 0, w = 0, w2 = 0, n = 0, n2 = 0, num_bloco = 0, codigo = 0, check = -1;
string pedreira = "", material = "", descricao = "";
double valor_compra = 0, valor_venda = 0, metros_cubicos = 0;
string[] pedreiras = new string[20]; //Guarda o nome de cada nova pedreira adicionada (Util para a função listar() no modo = 3; até 20 pedreiras)
var blocos = new List<Bloco>(); //Cria uma lista com futuros objetos da classe Bloco

//Inicializa os slots da variavel pedreiras[] como ""
for (var i = 0; i < 20; i++)
{
    pedreiras[i] = "";
}


//Adicionei alguns blocos e pedreiras iniciais para já ter listas possiveis de se ver sem ter que Digitar um monte de blocos para ver o resultado do programa 
if (!File.Exists("blocos.txt")) { 
    blocos.Add(new Bloco(2403, 1, "quebradiço", "granito", "santa fé", 1000, 1500, 100));
    blocos.Add(new Bloco(2456, 2, "feio kkk", "granito", "santa fé", 300, 450, 30));
    blocos.Add(new Bloco(2479, 3, "branco", "marmore", "rezende", 200, 300, 20));
    blocos.Add(new Bloco(2590, 4, "branco", "marmore", "santa fé", 200, 300, 20));
    blocos.Add(new Bloco(2594, 5, "três vêios de ametista", "granito", "santa fé", 50, 75, 5));
    blocos.Add(new Bloco(2621, 6, "preto", "granito", "rezende", 200, 300, 20));
    blocos.Add(new Bloco(2673, 7, "cortado desigual", "marmore", "itaoca pedra", 300, 450, 30));
    blocos.Add(new Bloco(2701, 8, "dourado", "granito", "santa fé", 100, 150, 10));
    blocos.Add(new Bloco(2701, 9, "dourado", "granito", "itaoca pedra", 400, 600, 40));

    n = 9;

    pedreiras[0] = "santa fé";
    pedreiras[1] = "rezende";
    pedreiras[2] = "itaoca pedra";
}

//Carrega os blocos da ultima sessão (Isso se houver um arquivo bloco.txt)
LerBlocos();

//Loop do Menu
while (w == 0)
{
    //Reseta as variaveis de cadastro e de escolha
    esc = 0;
    w2 = 0;
    n2 = 0;
    num_bloco = 0;
    codigo = 0;
    check = -1;
    pedreira = "";
    material = "";
    descricao = "";
    valor_compra = 0;
    valor_venda = 0;
    metros_cubicos = 0;

    //Printa as opções do Menu na Tela
    Console.WriteLine("\n\n      >>> GESTÃO DE BLOCOS <<<\n1-Cadastrar Bloco\n2-Listar Blocos\n3-Buscar Bloco por código\n4-Listar Blocos por pedreira\n5-Sair\n");
    
    //Verifica se esc é um valor inteiro
    try
    {
        esc = Convert.ToInt32(Console.ReadLine())!;
    }
    catch (FormatException e) {
        Console.WriteLine("\nNão é uma escolha válida: " + e.Message);
        esc = 0;
    }

    //Define o que cada opção de Gestão faz 
    switch (esc)
    {
        //Cadastrar Bloco (Com a opção de corrigir informações colocadas incorretamente)
        case 1:
            while(w2 == 0)
            {
                opcoes(codigo, descricao, material, pedreira, valor_compra, valor_venda, metros_cubicos);
                
                //Verifica se esc é um valor inteiro
                try
                {
                    esc = Convert.ToInt32(Console.ReadLine())!;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("\nNão é uma escolha válida: " + e.Message);
                    esc = 0;
                }

                //Define o que cada opção de Cadastro faz 
                switch (esc)
                {
                    //Alterar Código do bloco
                    case 1:
                        Console.WriteLine("\n(Defina o código do bloco)");
                        codigo = Convert.ToInt32(Console.ReadLine())!;
                        break;
                    
                    //Alterar Pedreira do bloco
                    case 2:
                        //Se a pedreira definida anteriormente for uma nova pedreira que não existia na lista e agora será corrigida, remove o antigo nome da lista
                        if (check > -1)
                        {
                            Console.WriteLine("\n(Defina o nome da pedreira com os caracteres em minusculo)");
                            pedreira = Console.ReadLine()!;
                            pedreiras[check] = pedreira;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\n(Defina o nome da pedreira com os caracteres em minusculo)");
                            pedreira = Console.ReadLine()!;

                        }

                        check = -1;

                        //Checa se essa pedreira já está cadastrada
                        for (var i = 0; i < 20; i++)
                        {
                            //Se essa pedreira já está cadastrada, simplesmente segue em frente
                            if (pedreiras[i] == pedreira && check == -1)
                            {
                                check = 0;//Esse é um marcador que sinaliza que essa pedreira já existia na lista
                            }
                        }

                        //Se a variavel check for -1, significa que a pedreira não existe na lista, então adiciona a nova pedreira à lista
                        if(check == -1)
                        {
                            //Checa qual o próximo espaço vazio da lista de pedreiras e adiciona a nova pedreira
                            for(var i = 0; i < 20; i++)
                            {
                                if(pedreiras[i] == "")
                                {
                                    pedreiras[i] = pedreira;
                                    check = i;//Esse marcador, sendo diferente de -1, sinaliza que essa pedreira foi adicionada na lista recentemente e passa a guardar o valor de i para uma futura correção
                                    break;
                                }
                            }
                        }
                        break;
                    
                    //Alterar Material do bloco
                    case 3:
                        Console.WriteLine("\n(Defina entre granito ou mármore)");
                        material = Console.ReadLine()!;
                        break;

                    //Alterar Descrição do bloco
                    case 4:
                        Console.WriteLine("\n(Defina a descrição do bloco)");
                        descricao = Console.ReadLine()!;
                        break;

                    //Alterar valor de compra do bloco
                    case 5:
                        Console.WriteLine("\n(Defina o valor de compra)");
                        valor_compra = Convert.ToDouble(Console.ReadLine())!;
                        break;

                    //Alterar valor de venda do bloco
                    case 6:
                        Console.WriteLine("\n(Defina o valor de venda)");
                        valor_venda = Convert.ToDouble(Console.ReadLine())!;
                        break;
                    
                    //Alterar metros cúbicos
                    case 7:
                        Console.WriteLine("\n(Defina os metros cúbicos)");
                        metros_cubicos = Convert.ToDouble(Console.ReadLine())!;
                        break;
                    
                    //Finaliza o cadastro e constroi o novo objeto da classe Bloco e adiciona na lista blocos
                    case 8:
                        n++;
                        blocos.Add(new Bloco(codigo, n, descricao, material, pedreira, valor_compra, valor_venda, metros_cubicos));
                        w2 = 1;
                        //Salva a lista atual de blocos com a adição do novo bloco
                        SalvarBlocos();
                        break;

                    default:
                        esc = 0;
                        break;
                }
            }
            break;
        
        //Listar Blocos
        case 2:
            //Chama a função listar com modo = 1 (Lista todos os blocos por ordem de entrada na lista)
            listar(1);
            break;
        
        //Buscar Bloco por código
        case 3:
            while (w2 == 0)
            {
                //Recebe o codigo do produto que o usuario quer encontrar e armazena na variavel "n2"
                Console.WriteLine("\nDigite o Código referente ao registro do bloco:");
                //Verifica se n2 é um valor inteiro
                try
                {
                    n2 = Convert.ToInt32(Console.ReadLine()!);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("\nNão é uma escolha válida: " + e.Message);
                    n2 = 0;
                }

                //Encontra na lista blocos[], um bloco que contenha o mesmo valor digitado, em sua variavel "Codigo", e "n2" armazena agora o index desse bloco na lista blocos[]
                n2 = blocos.FindIndex(x => x.Codigo == n2);

                //Se não houver bloco com esse Código, revela essa mensagem:
                if (n2 == -1)
                {
                    Console.WriteLine("\nNenhum bloco com esse Código foi cadastrado\n1-Tentar Novamente\n2-Voltar ao Menu");
                    
                    //Verifica se esc é um valor inteiro
                    try
                    {
                        esc = Convert.ToInt32(Console.ReadLine())!;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("\nNão é uma escolha válida: " + e.Message);
                        esc = 0;
                    }

                    if (esc == 2)
                    {
                        w2 = 1;
                    }
                }
                //Se houver algum bloco com esse Código, ativa o método info() que printa na tela o bloco encontrado
                else
                {
                    blocos[n2].info();
                    w2 = 1;
                }
            }
            break;
        
        //Listar Blocos por pedreita
        case 4:
            while(w2 == 0)
            {
                //Escolha qual tipo de busca você deseja
                Console.WriteLine("\n\nDefina a modalidade de exibição:\n1-Listar blocos em grupos de pedreiras\n2-Listar blocos de uma pedreira em especifico\n3-Voltar ao Menu\n");
                
                //Verifica se esc é um valor inteiro
                try
                {
                    esc = Convert.ToInt32(Console.ReadLine())!;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("\nNão é uma escolha válida: " + e.Message);
                    esc = 0;
                }

                switch (esc)
                {
                    //Listar blocos em grupos de pedreiras
                    case 1:
                        //Chama a função listar com modo = 2 (Lista todos os blocos por grupos de pedreiras)
                        listar(2);
                        w2 = 1;
                        break;

                    //Listar blocos de uma pedreira em especico
                    case 2:
                        //Procura se a Pedreira está nos registros
                        Console.WriteLine("\n\nDigite o nome da Pedreira:\n");
                        pedreira = Console.ReadLine()!;
                        

                        check = 0;

                        for(var i = 0; i < 20; i++)
                        {
                            //Se a pedreira existir, procede normalmente
                            if(pedreiras[i] == pedreira)
                            {
                                check = -1;
                            }
                        }

                        //Se não houver nenhuma pedreira com esse nome, revela essa mensagem:
                        if (check != -1)
                        {
                            Console.WriteLine("\nNenhuma Pedreira com esse Nome foi cadastrada\n");
                        }
                        //Se houver algum bloco com esse Código, ativa o método info() que printa na tela o bloco encontrado
                        else
                        {
                            //Chama a função listar com modo = 3 (Lista todos os blocos de uma pedreira em especifico)
                            listar(3);
                            w2 = 1;
                        }
                        break;

                    //Voltar ao Menu
                    case 3:
                        w2 = 1;
                        break;
                }
            }
            break;
        
        //Sair
        case 5:
            w = 1;
            break;
        
        default:
            esc = 0;
            break;
    }
}

//Essa função salva os dados dos blocos em um arquivo de texto blocos.txt
void SalvarBlocos()
{
    //Tenta salvar cada bloco no arquivo bloco.txt
    try
    {
        using (StreamWriter sw = new StreamWriter("blocos.txt"))
        {
            foreach (var bloco in blocos)
            {
                sw.WriteLine($"{bloco.Codigo},{bloco.Num_bloco},{bloco.Descricao},{bloco.Material},{bloco.Pedreira},{bloco.Valor_compra},{bloco.Valor_venda},{bloco.Metros_cubicos}");
            }
        }
        Console.WriteLine("\nDados dos blocos salvos no arquivo 'blocos.txt'.");
    }
    catch (IOException ex)
    {
        Console.WriteLine("\nOcorreu um erro ao salvar os dados:" + ex.Message);
    }
}


//Essa função lê os dados dos blocos no arquivo bloco.txt
void LerBlocos()
{
    try
    {
        string linha;
        var b = 0; //Variavel usada para percorrer cada indice da array pedreiras[]
        var checagem = 0; //Se essa variavel for 1 significa que não há pedreiras com esse nome no array pedreiras[]
        using (StreamReader sr = new StreamReader("blocos.txt"))
        {          
            while ((linha = sr.ReadLine()) != null)
            {
                string[] dados = linha.Split(',');
                int codigo = int.Parse(dados[0]);
                int num_bloco = int.Parse(dados[1]);
                string descricao = dados[2];
                string material = dados[3];
                string pedreira = dados[4];
                double valor_compra = double.Parse(dados[5]);
                double valor_venda = double.Parse(dados[6]);
                double metros_cubicos = double.Parse(dados[7]);

                blocos.Add(new Bloco(codigo, num_bloco, descricao, material, pedreira, valor_compra, valor_venda, metros_cubicos));

                //Checa se já existe a pedreira do bloco atual, na array pedreiras[], caso não exista, ela é adicionada
                while (pedreiras[b] != "" && checagem == 0) {
                    if (pedreiras[b] == pedreira) {
                        checagem = 1;
                    }
                    b++;
                }
                
                //Só chega até aqui se nenhuma outra pedreira nos indices anteriores da array pedreiras[] foi igual a pedreira
                if (pedreiras[b] == "" && checagem == 0)
                {
                    //Adiciona a nova pedreira
                    pedreiras[b] = pedreira;
                }
                b = 0;
                checagem = 0;
                n++;
            }
        }
        Console.WriteLine("\nDados dos blocos lidos do arquivo 'blocos.txt'.");
    }
    catch (IOException ex)
    {
        Console.WriteLine($"Ocorreu um erro ao ler os dados: {ex.Message}");
    }
    catch (FormatException ex)
    {
        Console.WriteLine($"Ocorreu um erro de formato nos dados: {ex.Message}");
    }
}


//Essa função printa as opções de adição ou correção das informações no cadastro de um novo bloco
void opcoes(int codigo, string descricao, string material, string pedreira, double valor_compra, double valor_venda, double metros_cubicos)
{
    Console.WriteLine("\n\n      >>> CADASTRAR BLOCO <<<\n");

    if (codigo == 0)
    {
        Console.WriteLine("1-Adicionar Codigo");
    }
    else
    {
        Console.WriteLine($"1-Corrigir Codigoº (#{codigo})");
    }

    if (pedreira == "")
    {
        Console.WriteLine("2-Adicionar Pedreira");
    }
    else
    {
        Console.WriteLine($"2-Corrigir Pedreira ({pedreira})");
    }

    if (material == "")
    {
        Console.WriteLine("3-Adicionar Material");
    }
    else
    {
        Console.WriteLine($"3-Corrigir Material ({material})");
    }

    if (descricao == "")
    {
        Console.WriteLine("4-Adicionar Descrição");
    }
    else
    {
        Console.WriteLine($"4-Corrigir Descrição ({descricao})");
    }

    if (valor_compra == 0)
    {
        Console.WriteLine("5-Adicionar Valor de Compra");
    }
    else
    {
        Console.WriteLine($"5-Corrigir Valor de Compra (R${valor_compra})");
    }

    if (valor_venda == 0)
    {
        Console.WriteLine("6-Adicionar Valor de Venda");
    }
    else
    {
        Console.WriteLine($"6-Corrigir Valor de Venda (R${valor_venda})");
    }

    if (metros_cubicos == 0)
    {
        Console.WriteLine("7-Adicionar Metros Cubicos");
    }
    else
    {
        Console.WriteLine($"7-Corrigir Metros Cubicos ({metros_cubicos}M³)");
    }

    if (codigo != 0 && pedreira != "" && material != "" && descricao != "" && valor_compra != 0 && valor_venda != 0 && metros_cubicos != 0)
    {
        Console.WriteLine("8-Finalizar cadastro de bloco\n");
    }
    else
    {
        Console.WriteLine("(PREENCHA TODAS AS INFORMAÇÕES PARA FINALIZAR CADASTRO)\n");
    }
}


    //Essa função cria a lista de blocos
    void listar(int modo)
{
    //Lista todos os blocos na ordem de ad~ição à lista
    if(modo == 1)
    {
        Console.WriteLine("\n\nNº :: Codigo :: Pedreira :: Material :: Valor de Compra :: Valor de Venda :: Metros Cúbicos :: Descrição\n");
        foreach (var item in blocos)
        {
            item.info();
        }
    }
    
    //Lista todos os blocos agrupando por pedreiras
    if (modo == 2)
    {
        Console.WriteLine("\n\nNº :: Codigo :: Pedreira :: Material :: Valor de Compra :: Valor de Venda :: Metros Cúbicos :: Descrição\n");
        //Agrupa cada bloco de cada pedreira
        for (var i = 0; i < 20; i++)
        {
            //Percorre todos os itens da lista blocos
            foreach (var item in blocos)
            {
                //Printa apenas os blocos correspondentes à pedreira atual na lista de pedreiras
                if (item.Pedreira == pedreiras[i])
                {
                    item.info();
                }
            }

            //Se acabar as pedreiras, o loop acaba mais cedo
            if (pedreiras[i] == "")
            {
                break;
            }
        }
    }

    //Lista todos os blocos de uma pedreira em especifico
    if (modo == 3)
    {
        Console.WriteLine("\n\nNº :: Codigo :: Pedreira :: Material :: Valor de Compra :: Valor de Venda :: Metros Cúbicos :: Descrição\n");
        //Agrupa os blocos de uma pedreira em específico
        //Percorre todos os itens da lista blocos
        foreach (var item in blocos)
        {
            //Printa apenas os blocos correspondentes à pedreira selecionada
            if (item.Pedreira == pedreira)
            {
                item.info();
            }
        }
    }
}