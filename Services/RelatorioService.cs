using SondagemNectaAPI.Data.Repositories;
using SondagemNectaAPI.Interfaces;
using SondagemNectaAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Pictures;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace SondagemNectaAPI.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly RelatorioRepository _relatorioRepository;
        private readonly ICadastro _cadastroRepository;
        private readonly string pathRelatorio = @"D:\Relatorios\";
        private readonly string pathDocRelatorio = @"E:\Programas_Desenvolvidos\Bruno\Api_sondagem_necta\SondagemNectaAPI\Documento\PRODUTO.docx";

        public RelatorioService(ICadastro cadastroRepository)
        {
            _cadastroRepository = cadastroRepository;
        }

        public async Task<List<RelatorioGerado>> GerarRelatorios(List<int> idsList)
        {
            var cadastros = _cadastroRepository.GetById(idsList);
            var documentosGerados = new List<RelatorioGerado>();

            foreach (var cadastro in cadastros)
            {
                List<string> nomeFotos = new List<string>();
                string nomeSubdiretorio = cadastro.CodigoPonto + "_" + cadastro.Rodovia;

                nomeFotos.Add(cadastro.NomeFotoExecucao.Split(';')[0]);
                nomeFotos.Add(cadastro.NomeFotoExecucao.Split(';')[1]);
                nomeFotos.Add(cadastro.NomeFotoFuroFechado);
                nomeFotos.Add(cadastro.NomeFotoColeta.Split(';')[0]);
                nomeFotos.Add(cadastro.NomeFotoColeta.Split(';')[1]);

                string nomeArquivoGerado = GerarRelatorioWord(
                    cadastro.NomePonto,
                    cadastro.LatitudeUTM,
                    cadastro.LongitudeUTM,
                    cadastro.ProfundidadeFinal,
                    nomeSubdiretorio,
                    nomeFotos);

                documentosGerados.Add(new RelatorioGerado
                {
                    NomeArquivo = nomeArquivoGerado,
                    ConteudoArquivo = File.ReadAllBytes(nomeArquivoGerado)
                });
            }

            return documentosGerados;
        }

        public string GerarRelatorioWord(
            string nomePonto,
            string latitudeUTM,
            string longitudeUTM,
            string profundidadeFinal,
            string nomeSubdiretorio,
            List<string> nomeFotos)
        {
            string localFolder = System.IO.Path.Combine(pathRelatorio, "cache");
            string templatePath = System.IO.Path.Combine(pathDocRelatorio);
            string outputPath = System.IO.Path.Combine(pathRelatorio, $"{nomePonto}.docx");

            nomePonto = nomePonto.Split('_')[0];

            if (!Directory.Exists(localFolder))
            {
                Directory.CreateDirectory(localFolder);
            }

            File.Copy(templatePath, outputPath, true);

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(outputPath, true))
            {
                var body = wordDoc.MainDocumentPart.Document.Body;

                ReplaceTextInDocument(wordDoc, "ponto", nomePonto);
                ReplaceTextInDocument(wordDoc, "latitude", latitudeUTM);
                ReplaceTextInDocument(wordDoc, "longitude", longitudeUTM);
                ReplaceTextInDocument(wordDoc, "proffinal", profundidadeFinal);

                int contador = 1;
                string fotosFolderBase = "D:\\xampp\\htdocs\\Repositorio\\Dados_SondagemSP_2024";
                string fotosFolderSubdir = System.IO.Path.Combine(fotosFolderBase, nomeSubdiretorio);

                foreach (var foto in nomeFotos)
                {
                    string photoLocalPath = System.IO.Path.Combine(fotosFolderSubdir, foto);
                    if (File.Exists(photoLocalPath))
                    {
                        string placeholder = $"Imagem{contador}";
                        ReplaceImageInDocument(wordDoc, body, photoLocalPath, placeholder);
                        contador++;
                    }
                }

                // Salvar o documento
                wordDoc.MainDocumentPart.Document.Save();
            }

            return outputPath;
        }

        private void ReplaceTextInDocument(WordprocessingDocument wordDoc, string placeholder, string newValue)
        {
            // Substituir no corpo principal do documento
            ReplaceTextInPart(wordDoc.MainDocumentPart.Document.Body, placeholder, newValue);

            // Substituir em tabelas (explícito)
            foreach (var table in wordDoc.MainDocumentPart.Document.Body.Descendants<DocumentFormat.OpenXml.Wordprocessing.Table>())
            {
                foreach (var row in table.Descendants<DocumentFormat.OpenXml.Wordprocessing.TableRow>())
                {
                    foreach (var cell in row.Descendants<DocumentFormat.OpenXml.Wordprocessing.TableCell>())
                    {
                        ReplaceTextInPart(cell, placeholder, newValue);
                    }
                }
            }

            foreach (var headerPart in wordDoc.MainDocumentPart.HeaderParts)
            {
                ReplaceTextInPart(headerPart.Header, placeholder, newValue);
            }

            foreach (var footerPart in wordDoc.MainDocumentPart.FooterParts)
            {
                ReplaceTextInPart(footerPart.Footer, placeholder, newValue);
            }
        }

        private void ReplaceTextInPart(OpenXmlElement part, string placeholder, string newValue)
        {
            foreach (var cell in part.Descendants<DocumentFormat.OpenXml.Wordprocessing.TableCell>())
            {
                var textElements = cell.Descendants<Text>().ToList();
                string fullText = string.Join("", textElements.Select(t => t.Text));

                if (fullText.Contains(placeholder))
                {
                    fullText = fullText.Replace(placeholder, newValue);

                    for (int i = 0; i < textElements.Count; i++)
                    {
                        if (i == 0)
                        {
                            textElements[i].Text = fullText;
                        }
                        else
                        {
                            textElements[i].Text = string.Empty;
                        }
                    }
                }
            }
        }


        // Função para substituir imagens no documento
        private void ReplaceImageInDocument(WordprocessingDocument wordDoc, Body body, string imagePath, string placeholder)
        {
            // Localiza o parágrafo que contém o marcador da imagem
            var paragraph = body.Descendants<DocumentFormat.OpenXml.Wordprocessing.Paragraph>()
                                .FirstOrDefault(p => p.InnerText.Contains(placeholder));
            if (paragraph != null)
            {
                // Carregar a imagem no documento
                ImagePart imagePart = wordDoc.MainDocumentPart.AddImagePart(ImagePartType.Jpeg);
                using (FileStream stream = new FileStream(imagePath, FileMode.Open))
                {
                    imagePart.FeedData(stream);
                }

                // Inserir a imagem no parágrafo
                AddImageToParagraph(wordDoc, paragraph, wordDoc.MainDocumentPart.GetIdOfPart(imagePart));

                // Remover o marcador de posição
                var textElement = paragraph.Descendants<Text>().FirstOrDefault(t => t.Text.Contains(placeholder));
                if (textElement != null)
                {
                    textElement.Text = string.Empty;
                }
            }
        }

        private void AddImageToParagraph(WordprocessingDocument wordDoc, DocumentFormat.OpenXml.Wordprocessing.Paragraph paragraph, string relationshipId)
        {
            // Definir o tamanho da imagem em emus (English Metric Units)
            long widthEmu = 300 * 914400 / 96;  // 300 pixels de largura
            long heightEmu = 320 * 914400 / 96; // 320 pixels de altura

            // Criação do run para inserir a imagem
            var run = paragraph.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Run());

            var drawing = new Drawing(
                new Inline(
                    new Extent { Cx = widthEmu, Cy = heightEmu }, // Dimensões da imagem
                    new EffectExtent { LeftEdge = 0L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L },
                    new DocProperties { Id = (UInt32Value)1U, Name = "Imagem" },
                    new DocumentFormat.OpenXml.Drawing.Wordprocessing.NonVisualGraphicFrameDrawingProperties(new GraphicFrameLocks { NoChangeAspect = true }),
                    new Graphic(
                        new GraphicData(
                            new DocumentFormat.OpenXml.Drawing.Pictures.Picture(
                                new DocumentFormat.OpenXml.Drawing.Pictures.NonVisualPictureProperties(
                                    new DocumentFormat.OpenXml.Drawing.Pictures.NonVisualDrawingProperties { Id = (UInt32Value)0U, Name = "imagem" },
                                    new DocumentFormat.OpenXml.Drawing.Pictures.NonVisualPictureDrawingProperties()),
                                new DocumentFormat.OpenXml.Drawing.Pictures.BlipFill(
                                    new DocumentFormat.OpenXml.Drawing.Blip { Embed = relationshipId },
                                    new DocumentFormat.OpenXml.Drawing.Stretch(new DocumentFormat.OpenXml.Drawing.FillRectangle())),
                                new DocumentFormat.OpenXml.Drawing.Pictures.ShapeProperties(
                                    new DocumentFormat.OpenXml.Drawing.Transform2D(
                                        new DocumentFormat.OpenXml.Drawing.Offset { X = 0L, Y = 0L },
                                        new DocumentFormat.OpenXml.Drawing.Extents { Cx = widthEmu, Cy = heightEmu }),  // Tamanho especificado aqui
                                    new DocumentFormat.OpenXml.Drawing.PresetGeometry(new DocumentFormat.OpenXml.Drawing.AdjustValueList())
                                    { Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle })))
                        { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" }))
                { DistanceFromTop = 0U, DistanceFromBottom = 0U, DistanceFromLeft = 0U, DistanceFromRight = 0U });

            run.Append(drawing);
        }


    }
}
