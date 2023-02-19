using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Senha { get; set; }
}

public class UsuarioListado
{
    private MySqlConnection connection;

    public UsuarioListado(string connectionString)
    {
        connection = new MySqlConnection(connectionString);
    }

    public List<Usuario> Select()
    {
        List<Usuario> usuarios = new List<Usuario>();

        try
        {
            connection.Open();

            string sql = "SELECT idusuarios, nome, cpf, email, telefone, senha FROM usuarios";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                Usuario usuario = new Usuario
                {
                    Id = Convert.ToInt32(dataReader["idusuarios"]),
                    Nome = dataReader["nome"].ToString(),
                    CPF = dataReader["cpf"].ToString(),
                    Email = dataReader["email"].ToString(),
                    Telefone = dataReader["telefone"].ToString(),
                    Senha = dataReader["senha"].ToString()
                };
                usuarios.Add(usuario);
            }

            dataReader.Close();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Erro ao tentar listar usuários: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }

        return usuarios;
    }
}

public class NovoUsuario
{
    private string connectionString;

    public NovoUsuario(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void Inserir(Usuario usuario)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("INSERT INTO usuarios (nome, cpf, email, telefone, senha) VALUES (@nome, @cpf, @email, @telefone, @senha)", connection);
                command.Parameters.AddWithValue("@nome", usuario.Nome);
                command.Parameters.AddWithValue("@cpf", usuario.CPF);
                command.Parameters.AddWithValue("@email", usuario.Email);
                command.Parameters.AddWithValue("@telefone", usuario.Telefone);
                command.Parameters.AddWithValue("@senha", usuario.Senha);

                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao tentar inserir usuário: " + ex.Message);
            }
        }
    }

    public List<Usuario> Listar()
    {
        List<Usuario> usuarios = new List<Usuario>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM usuarios", connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.Id = reader.GetInt32("idusuarios");
                        usuario.Nome = reader.GetString("nome");
                        usuario.CPF = reader.GetString("cpf");
                        usuario.Email = reader.GetString("email");
                        usuario.Telefone = reader.GetString("telefone");
                        usuario.Senha = reader.GetString("senha");

                        usuarios.Add(usuario);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao tentar listar usuários: " + ex.Message);
            }
        }

        return usuarios;
    }

    public void Atualizar(Usuario usuario)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            MySqlCommand command = new MySqlCommand("UPDATE usuarios SET nome = @nome, cpf = @cpf, email = @email, telefone = @telefone, senha = @senha WHERE idusuarios = @id", connection);
            command.Parameters.AddWithValue("@nome", usuario.Nome);
            command.Parameters.AddWithValue("@cpf", usuario.CPF);
            command.Parameters.AddWithValue("@email", usuario.Email);
            command.Parameters.AddWithValue("@telefone", usuario.Telefone);
            command.Parameters.AddWithValue("@senha", usuario.Senha);
            command.Parameters.AddWithValue("@id", usuario.Id);

            command.ExecuteNonQuery();
        }
    }

    public void Deletar(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            MySqlCommand command = new MySqlCommand("DELETE FROM usuarios WHERE idusuarios = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }
}


public class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int AnoPublicacao { get; set; }
}

public class Emprestimo
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public int IdLivro { get; set; }
    public DateTime DataEmprestimo { get; set; }
    public DateTime DataDevolucao { get; set; }
}

public class Biblioteca
{
    private string connectionString;

    public Biblioteca(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void EmprestarLivro(int idUsuario, int idLivro, int diasEmprestimo)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Verifica se o livro já está emprestado
                MySqlCommand checkCommand = new MySqlCommand("SELECT id FROM emprestimos WHERE id_livro=@id_livro AND data_devolucao IS NULL", connection);
                checkCommand.Parameters.AddWithValue("@id_livro", idLivro);
                MySqlDataReader checkReader = checkCommand.ExecuteReader();

                if (checkReader.HasRows)
                {
                    Console.WriteLine("O livro já está emprestado.");
                    checkReader.Close();
                    return;
                }

                checkReader.Close();

                // Insere o empréstimo
                Emprestimo emprestimo = new Emprestimo
                {
                    IdUsuario = idUsuario,
                    IdLivro = idLivro,
                    DataEmprestimo = DateTime.Now,
                    DataDevolucao = DateTime.Now.AddDays(diasEmprestimo)
                };

                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO emprestimos (id_usuario, id_livro, data_emprestimo, data_devolucao) VALUES (@id_usuario, @id_livro, @data_emprestimo, @data_devolucao)", connection);
                insertCommand.Parameters.AddWithValue("@id_usuario", emprestimo.IdUsuario);
                insertCommand.Parameters.AddWithValue("@id_livro", emprestimo.IdLivro);
                insertCommand.Parameters.AddWithValue("@data_emprestimo", emprestimo.DataEmprestimo);
                insertCommand.Parameters.AddWithValue("@data_devolucao", emprestimo.DataDevolucao);

                insertCommand.ExecuteNonQuery();

                // Marca o livro como emprestado
                MySqlCommand updateCommand = new MySqlCommand("UPDATE livros SET emprestado=1 WHERE id=@id", connection);
                updateCommand.Parameters.AddWithValue("@id", idLivro);

                updateCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao tentar emprestar o livro: " + ex.Message);
            }
        }
    }    
    
public void DevolverLivro(int idEmprestimo)
{
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        try
        {
            connection.Open();

            MySqlCommand command = new MySqlCommand("UPDATE emprestimos SET data_devolucao = @dataDevolucao WHERE id_emprestimo = @idEmprestimo", connection);
            command.Parameters.AddWithValue("@dataDevolucao", DateTime.Now);
            command.Parameters.AddWithValue("@idEmprestimo", idEmprestimo);

            command.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Erro ao tentar devolver livro: " + ex.Message);
        }
    }
}
}




