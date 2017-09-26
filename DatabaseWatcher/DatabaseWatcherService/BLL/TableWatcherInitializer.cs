using TableWatcher.Factory;
using BennerESocialDbWatcherService.Model;

namespace BennerESocialDbWatcherService.BLL
{
    public class TableWatcherInitializer
    {
        TableWatcherStrategy<Eso_Agente_Acidente_Trabalho> twEso_Agente_Acidente_Trabalho;
        TableWatcherStrategy<Eso_Agente_Doenca_Profissional> twEso_Agente_Doenca_Profissional;
        TableWatcherStrategy<Eso_Categoria_Trabalhador> twEso_Categoria_Trabalhador;
        TableWatcherStrategy<Eso_Codificacao_Acidente_Trab> twEso_Codificacao_Acidente_Trab;
        TableWatcherStrategy<Eso_Descricao_Natureza_Lesao> twEso_Descricao_Natureza_Lesao;
        TableWatcherStrategy<Eso_Estado_Civil> twEso_Estado_Civil;
        TableWatcherStrategy<Eso_Financiam_Aposent_Especial> twEso_Financiam_Aposent_Especial;
        TableWatcherStrategy<Eso_Motivo_Afastamento> twEso_Motivo_Afastamento;
        TableWatcherStrategy<Eso_Param_Estado_Civil> twEso_Param_Estado_Civil;
        TableWatcherStrategy<Eso_Param_Tipo_Dependente> twEso_Param_Tipo_Dependente;
        TableWatcherStrategy<Eso_Param_Tipo_Logradouro> twEso_Param_Tipo_Logradouro;
        TableWatcherStrategy<Eso_Parte_Corpo_Atingida> twEso_Parte_Corpo_Atingida;
        TableWatcherStrategy<Eso_S1010_Rubrica> twEso_S1010_Rubrica;
        TableWatcherStrategy<Eso_S1020_Lotacao_Tributaria> twEso_S1020_Lotacao_Tributaria;
        TableWatcherStrategy<Eso_S1030_Cargo_Emprego_Public> twEso_S1030_Cargo_Emprego_Public;
        TableWatcherStrategy<Eso_S1040_Funcao_Cargo_Comiss> twEso_S1040_Funcao_Cargo_Comiss;
        TableWatcherStrategy<Eso_S1207_Beneficio_Previdenc> twEso_S1207_Beneficio_Previdenc;
        TableWatcherStrategy<Eso_S2260_Convocacao_Trab_Inte> twEso_S2260_Convocacao_Trab_Inte;
        TableWatcherStrategy<Eso_Situacao_Geradora_Acidente> twEso_Situacao_Geradora_Acidente;
        TableWatcherStrategy<Eso_Tipo_Dependente> twEso_Tipo_Dependente;
        TableWatcherStrategy<Eso_Tipo_Inscricao> twEso_Tipo_Inscricao;
        TableWatcherStrategy<Eso_Tipo_Logradouro> twEso_Tipo_Logradouro;
        TableWatcherStrategy<Eso_Versao> twEso_Versao;
        TableWatcherStrategy<Paises> twPaises;

        public TableWatcherInitializer()
        {
            twEso_Agente_Acidente_Trabalho = new TableWatcherStrategy<Eso_Agente_Acidente_Trabalho>();
            twEso_Agente_Doenca_Profissional = new TableWatcherStrategy<Eso_Agente_Doenca_Profissional>();
            twEso_Categoria_Trabalhador = new TableWatcherStrategy<Eso_Categoria_Trabalhador>();
            twEso_Codificacao_Acidente_Trab = new TableWatcherStrategy<Eso_Codificacao_Acidente_Trab>();
            twEso_Descricao_Natureza_Lesao = new TableWatcherStrategy<Eso_Descricao_Natureza_Lesao>();
            twEso_Estado_Civil = new TableWatcherStrategy<Eso_Estado_Civil>();
            twEso_Financiam_Aposent_Especial = new TableWatcherStrategy<Eso_Financiam_Aposent_Especial>();
            twEso_Motivo_Afastamento = new TableWatcherStrategy<Eso_Motivo_Afastamento>();
            twEso_Param_Estado_Civil = new TableWatcherStrategy<Eso_Param_Estado_Civil>();
            twEso_Param_Tipo_Dependente = new TableWatcherStrategy<Eso_Param_Tipo_Dependente>();
            twEso_Param_Tipo_Logradouro = new TableWatcherStrategy<Eso_Param_Tipo_Logradouro>();
            twEso_Parte_Corpo_Atingida = new TableWatcherStrategy<Eso_Parte_Corpo_Atingida>();
            twEso_S1010_Rubrica = new TableWatcherStrategy<Eso_S1010_Rubrica>();
            twEso_S1020_Lotacao_Tributaria = new TableWatcherStrategy<Eso_S1020_Lotacao_Tributaria>();
            twEso_S1030_Cargo_Emprego_Public = new TableWatcherStrategy<Eso_S1030_Cargo_Emprego_Public>();
            twEso_S1040_Funcao_Cargo_Comiss = new TableWatcherStrategy<Eso_S1040_Funcao_Cargo_Comiss>();
            twEso_S1207_Beneficio_Previdenc = new TableWatcherStrategy<Eso_S1207_Beneficio_Previdenc>();
            twEso_S2260_Convocacao_Trab_Inte = new TableWatcherStrategy<Eso_S2260_Convocacao_Trab_Inte>();
            twEso_Situacao_Geradora_Acidente = new TableWatcherStrategy<Eso_Situacao_Geradora_Acidente>();
            twEso_Tipo_Dependente = new TableWatcherStrategy<Eso_Tipo_Dependente>();
            twEso_Tipo_Inscricao = new TableWatcherStrategy<Eso_Tipo_Inscricao>();
            twEso_Tipo_Logradouro = new TableWatcherStrategy<Eso_Tipo_Logradouro>();
            twEso_Versao = new TableWatcherStrategy<Eso_Versao>();
            twPaises = new TableWatcherStrategy<Paises>();
        }

        public void InitializeTableWatchers()
        {
            twEso_Agente_Acidente_Trabalho.InitializeTableWatcher();
            twEso_Agente_Doenca_Profissional.InitializeTableWatcher();
            twEso_Categoria_Trabalhador.InitializeTableWatcher();
            twEso_Codificacao_Acidente_Trab.InitializeTableWatcher();
            twEso_Descricao_Natureza_Lesao.InitializeTableWatcher();
            twEso_Estado_Civil.InitializeTableWatcher();
            twEso_Financiam_Aposent_Especial.InitializeTableWatcher();
            twEso_Motivo_Afastamento.InitializeTableWatcher();
            twEso_Param_Estado_Civil.InitializeTableWatcher();
            twEso_Param_Tipo_Dependente.InitializeTableWatcher();
            twEso_Param_Tipo_Logradouro.InitializeTableWatcher();
            twEso_Parte_Corpo_Atingida.InitializeTableWatcher();
            twEso_S1010_Rubrica.InitializeTableWatcher();
            twEso_S1020_Lotacao_Tributaria.InitializeTableWatcher();
            twEso_S1030_Cargo_Emprego_Public.InitializeTableWatcher();
            twEso_S1040_Funcao_Cargo_Comiss.InitializeTableWatcher();
            twEso_S1207_Beneficio_Previdenc.InitializeTableWatcher();
            twEso_S2260_Convocacao_Trab_Inte.InitializeTableWatcher();
            twEso_Situacao_Geradora_Acidente.InitializeTableWatcher();
            twEso_Tipo_Dependente.InitializeTableWatcher();
            twEso_Tipo_Inscricao.InitializeTableWatcher();
            twEso_Tipo_Logradouro.InitializeTableWatcher();
            twEso_Versao.InitializeTableWatcher();
            twPaises.InitializeTableWatcher();
        }

        public void StartTableWatchers()
        {
            twEso_Agente_Acidente_Trabalho.StartTableWatcher();
            twEso_Agente_Doenca_Profissional.StartTableWatcher();
            twEso_Categoria_Trabalhador.StartTableWatcher();
            twEso_Codificacao_Acidente_Trab.StartTableWatcher();
            twEso_Descricao_Natureza_Lesao.StartTableWatcher();
            twEso_Estado_Civil.StartTableWatcher();
            twEso_Financiam_Aposent_Especial.StartTableWatcher();
            twEso_Motivo_Afastamento.StartTableWatcher();
            twEso_Param_Estado_Civil.StartTableWatcher();
            twEso_Param_Tipo_Dependente.StartTableWatcher();
            twEso_Param_Tipo_Logradouro.StartTableWatcher();
            twEso_Parte_Corpo_Atingida.StartTableWatcher();
            twEso_S1010_Rubrica.StartTableWatcher();
            twEso_S1020_Lotacao_Tributaria.StartTableWatcher();
            twEso_S1030_Cargo_Emprego_Public.StartTableWatcher();
            twEso_S1040_Funcao_Cargo_Comiss.StartTableWatcher();
            twEso_S1207_Beneficio_Previdenc.StartTableWatcher();
            twEso_S2260_Convocacao_Trab_Inte.StartTableWatcher();
            twEso_Situacao_Geradora_Acidente.StartTableWatcher();
            twEso_Tipo_Dependente.StartTableWatcher();
            twEso_Tipo_Inscricao.StartTableWatcher();
            twEso_Tipo_Logradouro.StartTableWatcher();
            twEso_Versao.StartTableWatcher();
            twPaises.StartTableWatcher();
        }

        public void StopTableWatchers()
        {
            twEso_Agente_Acidente_Trabalho.StopTableWatcher();
            twEso_Agente_Doenca_Profissional.StopTableWatcher();
            twEso_Categoria_Trabalhador.StopTableWatcher();
            twEso_Codificacao_Acidente_Trab.StopTableWatcher();
            twEso_Descricao_Natureza_Lesao.StopTableWatcher();
            twEso_Estado_Civil.StopTableWatcher();
            twEso_Financiam_Aposent_Especial.StopTableWatcher();
            twEso_Motivo_Afastamento.StopTableWatcher();
            twEso_Param_Estado_Civil.StopTableWatcher();
            twEso_Param_Tipo_Dependente.StopTableWatcher();
            twEso_Param_Tipo_Logradouro.StopTableWatcher();
            twEso_Parte_Corpo_Atingida.StopTableWatcher();
            twEso_S1010_Rubrica.StopTableWatcher();
            twEso_S1020_Lotacao_Tributaria.StopTableWatcher();
            twEso_S1030_Cargo_Emprego_Public.StopTableWatcher();
            twEso_S1040_Funcao_Cargo_Comiss.StopTableWatcher();
            twEso_S1207_Beneficio_Previdenc.StopTableWatcher();
            twEso_S2260_Convocacao_Trab_Inte.StopTableWatcher();
            twEso_Situacao_Geradora_Acidente.StopTableWatcher();
            twEso_Tipo_Dependente.StopTableWatcher();
            twEso_Tipo_Inscricao.StopTableWatcher();
            twEso_Tipo_Logradouro.StopTableWatcher();
            twEso_Versao.StopTableWatcher();
            twPaises.StopTableWatcher();
        }
    }
}
