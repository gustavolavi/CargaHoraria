namespace Infra.Migrations
{
    using System;
    using System.Linq;
    using Entidades.Entities;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Infra.Data.Conexao>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Infra.Data.Conexao context)
        {
            context.TipoDeRegistro.AddOrUpdate(
              x => x.Tipo,
              new TipoDeRegistro { Tipo = "Entrar" },
              new TipoDeRegistro { Tipo = "Sa�da" },
              new TipoDeRegistro { Tipo = "Volta do almo�o" },
              new TipoDeRegistro { Tipo = "Sair para o almo�o" }
            );
        }
    }
}
