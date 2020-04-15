using Oportunidade.Interface.Blog;
using Oportunidade.Interface.Negocio;
using Oportunidade.Models.Blog;
using Oportunidade.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Oportunidade.Negocio
{
    public class Topico : ITopico
    {
        private readonly IFeed _feed;

        public Topico(IFeed feed)
        {
            _feed = feed;
        }

        public List<TopicoDTO> UltimosTopicos()
        {
            // Recupera os tópicos do blog
            var rss = _feed.ConsultarTopicos();

            List<TopicoDTO> topicos = new List<TopicoDTO>();

            string regexRemover = CriarRegexRemovePalavrasIrrelevantes();

            foreach (var item in rss.Channel.Items.OrderByDescending(_ => _.dtPubDate).Take(10))
            {
                TopicoDTO topico = new TopicoDTO();

                TratarDescricao(regexRemover, item, ref topico);
                TratarConteudo(regexRemover, item, ref topico);

                topicos.Add(topico);
            }

            return topicos;
        }

        #region TratarConteudo
        private void TratarConteudo(string regexRemover, Item item, ref TopicoDTO topico)
        {
            TratarTexto(regexRemover, item.ContendEncoded, out int TotalPalavras, out List<PalavraDTO> palavrasPrincipais);

            topico.TotalPalavrasConteudo = TotalPalavras;
            topico.PalavrasConteudo = palavrasPrincipais;
        }
        #endregion

        #region TratarDescricao
        private void TratarDescricao(string regexRemover, Item item, ref TopicoDTO topico)
        {
            TratarTexto(regexRemover, item.Description, out int TotalPalavras, out List<PalavraDTO> palavrasPrincipais);

            topico.TotalPalavrasDescricao = TotalPalavras;
            topico.PalavrasDescricao = palavrasPrincipais;
        }
        #endregion

        #region TratarTexto
        private void TratarTexto(string regexRemover, string texto, out int totalPalavras, out List<PalavraDTO> palavrasPrincipais)
        {
            // Remove palavras irrelevantes
            var descricaoTratada = Regex.Replace(texto, regexRemover, " ", RegexOptions.IgnoreCase);

            totalPalavras = ContarPalavras(descricaoTratada);

            palavrasPrincipais = RecuperarPalavrasPrincipais(descricaoTratada);
        }
        #endregion

        #region RecuperarPalavrasPrincipais
        private List<PalavraDTO> RecuperarPalavrasPrincipais(string descricaoTratada)
        {
            // Cria uma lista de palavras removendo as repetições
            List<string> listaPalavrasUnicas = RecuperarPalavrasUnicas(descricaoTratada);

            List<PalavraDTO> palavras = new List<PalavraDTO>();
            foreach (var palavraUnica in listaPalavrasUnicas)
            {
                // Recupera todas as repetições
                MatchCollection matches = Regex.Matches(descricaoTratada, palavraUnica);

                palavras.Add(new PalavraDTO()
                {
                    Palavra = palavraUnica,
                    Quantidade = matches.Count
                });
            }

            // Retorna as 10 palavras com maior ocorrência
            return palavras.OrderByDescending(_ => _.Quantidade).Take(10).ToList();
        }
        #endregion

        #region RecuperarPalavrasUnicas
        private List<string> RecuperarPalavrasUnicas(string descricaoTratada)
        {
            // Quebra o texto em uma lista removendo as palavras repetidas
            var listaPalavrasUnicas = descricaoTratada.Split(' ').Distinct().ToList();

            // Remove epaço vazio
            listaPalavrasUnicas.Remove(string.Empty);

            return listaPalavrasUnicas;
        }
        #endregion

        #region ContarPalavras
        private int ContarPalavras(string descricaoTratada)
        {
            IEnumerable<char> matchEspacos = from espaco in descricaoTratada
                                             where espaco == ' '
                                             select espaco;

            return matchEspacos.Count() + 1;
        }
        #endregion

        #region CriarRegexRemovePalavrasIrrelevantes
        private string CriarRegexRemovePalavrasIrrelevantes()
        {
            // Lista de preposições que serão removidas do texto
            List<string> preposicoes = new List<string>()
            {
                "a", "de", "em", "por", "per"
            };

            // Lista de artigos que serão removidos do texto
            List<string> artigos = new List<string>()
            {
                "o", "os", "ao", "aos", "do", "dos", "no", "nos", "pelo", "pelos",
                "a", "as", "à", "às", "da", "das", "na", "nas", "pela", "pelas",
                "um", "uns", "dum", "duns", "num", "nuns",
                "uma", "umas", "duma", "dumas", "numa", "numas"
            };

            string regexRemover = string.Empty;

            // Concatena expressão para remover palavras irrelevantes
            ConcatenarPalavrasIrrelevantes(preposicoes, ref regexRemover);
            ConcatenarPalavrasIrrelevantes(artigos, ref regexRemover);

            // Remove o último '|'
            return regexRemover.Remove(regexRemover.Length - 1);
        }
        #endregion

        #region ConcatenarPalavrasIrrelevantes
        private void ConcatenarPalavrasIrrelevantes(List<string> preposicoes, ref string regexRemover)
        {
            foreach (var preposicao in preposicoes)
            {
                regexRemover += @$"\s{preposicao}[\s,\,,\.]|";
            }
        }
        #endregion
    }
}