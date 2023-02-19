using dotenv.net;
class Program
{
    static void Main()
    {
        DotEnv.Load();
        var envVars = DotEnv.Read();
        string minhaConexao = envVars["Banco"];

        NovoUsuario person1 = new NovoUsuario(minhaConexao);
        person1.Listar().ForEach(usuarioTotal =>{
            Console.WriteLine("person1 = {0}",usuarioTotal.Nome);
        
        });
       
    }
}