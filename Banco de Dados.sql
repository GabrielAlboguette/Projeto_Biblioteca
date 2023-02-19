
CREATE DATABASE Biblioteca;

USE BIBLIOTECA;

CREATE TABLE usuarios (
    idusuarios INT NOT NULL AUTO_INCREMENT,
    nome VARCHAR(50) NOT NULL,
    cpf VARCHAR(15) UNIQUE,
    email VARCHAR(50) NOT NULL,
    telefone VARCHAR(50),
    senha VARCHAR(255) NOT NULL,
    PRIMARY KEY (idusuarios)
);

CREATE TABLE livros (
    idlivros INT NOT NULL AUTO_INCREMENT,
    titulo VARCHAR(255) NOT NULL,
    autor VARCHAR(255) NOT NULL,
    editora VARCHAR(255),
    ano_publicacao INT,
    genero VARCHAR(50),
    disponivel BOOLEAN NOT NULL DEFAULT true,
    PRIMARY KEY (idlivros)
    
);

/* Registrando alguns livros */

INSERT INTO livros (titulo, autor, editora, ano_publicacao, genero, disponivel) 
VALUES ('O Cortiço', 'Aluísio Azevedo', 'Martin Claret', 1890, 'Romance', true);

INSERT INTO livros (titulo, autor, editora, ano_publicacao, genero, disponivel) 
VALUES ('O Guarani', 'José de Alencar', 'Martin Claret', 1857, 'Romance', true);

INSERT INTO livros (titulo, autor, editora, ano_publicacao, genero, disponivel) 
VALUES ('A Hora da Estrela', 'Clarice Lispector', 'Rocco', 1977, 'Romance', true);

INSERT INTO livros (titulo, autor, editora, ano_publicacao, genero, disponivel) 
VALUES ('O Hobbit', 'J. R. R. Tolkien', 'HarperCollins', 1937, 'Fantasia', true);

INSERT INTO livros (titulo, autor, editora, ano_publicacao, genero, disponivel) 
VALUES ('Harry Potter e a Pedra Filosofal', 'J. K. Rowling', 'Rocco', 1997, 'Fantasia', true);

INSERT INTO livros (titulo, autor, editora, ano_publicacao, genero, disponivel) 
VALUES ('A Metamorfose', 'Franz Kafka', 'Companhia das Letras', 1915, 'Ficção', true);

INSERT INTO livros (titulo, autor, editora, ano_publicacao, genero, disponivel) 
VALUES ('A Revolução dos Bichos', 'George Orwell', 'Companhia das Letras', 1945, 'Ficção', true);

INSERT INTO livros (titulo, autor, editora, ano_publicacao, genero, disponivel) 
VALUES ('1984', 'George Orwell', 'Companhia das Letras', 1949, 'Ficção', true);

INSERT INTO livros (titulo, autor, editora, ano_publicacao, genero, disponivel) 
VALUES ('O Pequeno Príncipe', 'Antoine de Saint-Exupéry', 'Agir', 1943, 'Infantil', true);

INSERT INTO livros (titulo, autor, editora, ano_publicacao, genero, disponivel) 
VALUES ('A Menina que Roubava Livros', 'Markus Zusak', 'Intrínseca', 2005, 'Ficção', true);

/* inserindo usuarios */

INSERT INTO usuarios (nome, cpf, email, telefone, senha) 
VALUES ('João Silva', '123.456.789-00', 'joao.silva@email.com', '(11) 99999-9999', 'senha123');

INSERT INTO usuarios (nome, cpf, email, telefone, senha) 
VALUES ('Maria Santos', '987.654.321-00', 'maria.santos@email.com', '(11) 99999-8888', 'senha456');

INSERT INTO usuarios (nome, cpf, email, telefone, senha) 
VALUES ('Pedro Souza', '111.222.333-44', 'pedro.souza@email.com', '(11) 99999-7777', 'senha789');

INSERT INTO usuarios (nome, cpf, email, telefone, senha) 
VALUES ('Ana Costa', '555.666.777-88', 'ana.costa@email.com', '(11) 99999-6666', 'senhaabc');

INSERT INTO usuarios (nome, cpf, email, telefone, senha) 
VALUES ('Bruno Oliveira', '999.888.777-66', 'bruno.oliveira@email.com', '(11) 99999-5555', 'senhaxyz');

INSERT INTO usuarios (nome, cpf, email, telefone, senha) 
VALUES ('Lucas Santos', '222.333.444-55', 'lucas.santos@email.com', '(11) 99999-4444', 'senha123');

INSERT INTO usuarios (nome, cpf, email, telefone, senha) 
VALUES ('Julia Lima', '555.444.333-22', 'julia.lima@email.com', '(11) 99999-3333', 'senha456');

INSERT INTO usuarios (nome, cpf, email, telefone, senha) 
VALUES ('Paula Costa', '111.444.777-88', 'paula.costa@email.com', '(11) 99999-2222', 'senha789');

INSERT INTO usuarios (nome, cpf, email, telefone, senha) 
VALUES ('Thiago Pereira', '777.888.999-00', 'thiago.pereira@email.com', '(11) 99999-1111', 'senhaabc');

INSERT INTO usuarios (nome, cpf, email, telefone, senha) 
VALUES ('Carla Ribeiro', '444.555.666-77', 'carla.ribeiro@email.com', '(11) 99999-0000', 'senhaxyz');

