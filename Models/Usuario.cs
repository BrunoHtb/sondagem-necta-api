﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SondagemNectaAPI.Models
{
    public class Usuario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("login")]
        public string Login { get; set; }

        [Column("nome")]
        public string NomeUsuario { get; set; }

        [Column("permissao")]
        public string PerfilUsuario { get; set; }
    }
}
