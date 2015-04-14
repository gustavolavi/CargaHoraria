namespace Infra.Migrations
{
    using Entidades.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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
              new TipoDeRegistro { Tipo = "Saída" },
              new TipoDeRegistro { Tipo = "Volta do almoço" },
              new TipoDeRegistro { Tipo = "Sair para o almoço" }
            );
        }
    }
}
