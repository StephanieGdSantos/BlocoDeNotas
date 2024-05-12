﻿using BlocoDeNotas.Models;

namespace BlocoDeNotas.Repositorio
{
    public interface INotasRepositorio
    {
        NotasModel Selecionar(int id);
        List<NotasModel> ListarNotas();
        NotasModel Adicionar(NotasModel nota);
        NotasModel Editar(NotasModel nota);
        bool Excluir(int id);
    }
}
