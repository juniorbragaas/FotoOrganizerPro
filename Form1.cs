using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Directory = System.IO.Directory;

namespace FotoOrganizerPro
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource _cts;

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnProcessar_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtPasta.Text))
            {
                MessageBox.Show("Selecione uma pasta válida!");
                return;
            }

            _cts = new CancellationTokenSource();

            var arquivos = Directory.GetFiles(txtPasta.Text)
                .Where(f => new[] { ".jpg", ".jpeg", ".png", ".heic" }
                .Contains(Path.GetExtension(f).ToLower()))
                .ToList();

            progressBar.Maximum = arquivos.Count;
            progressBar.Value = 0;
            lstLog.Items.Clear();

            await Task.Run(() => ProcessarArquivos(arquivos, _cts.Token));
            lblStatus.Text = "Finalizado!";
        }

        private void ProcessarArquivos(System.Collections.Generic.List<string> arquivos, CancellationToken token)
        {
            int count = 0;

            foreach (var arquivo in arquivos)
            {
                if (token.IsCancellationRequested)
                    break;

                count++;

                try
                {
                    var metadata = ImageMetadataReader.ReadMetadata(arquivo);
                    var subIfd = metadata.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                    DateTime? data = subIfd?.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);

                    if (data != null)
                    {
                        File.SetCreationTime(arquivo, data.Value);
                        File.SetLastWriteTime(arquivo, data.Value);

                        string novoNomeBase = data.Value.ToString("yyyy-MM-dd_HH-mm-ss");
                        string ext = Path.GetExtension(arquivo);
                        string pasta = Path.GetDirectoryName(arquivo);

                        string novoCaminho = Path.Combine(pasta, novoNomeBase + ext);
                        int i = 1;

                        while (File.Exists(novoCaminho))
                        {
                            novoCaminho = Path.Combine(pasta, $"{novoNomeBase}_{i}{ext}");
                            i++;
                        }

                        File.Move(arquivo, novoCaminho);

                        AddLog($"OK: {Path.GetFileName(arquivo)}");
                    }
                    else
                    {
                        AddLog($"Sem EXIF: {Path.GetFileName(arquivo)}");
                    }
                }
                catch (Exception ex)
                {
                    AddLog($"Erro: {Path.GetFileName(arquivo)} - {ex.Message}");
                }

                UpdateProgress(count);
            }
        }

        private void AddLog(string texto)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AddLog), texto);
                return;
            }
            lstLog.Items.Add(texto);
            // 🔥 FAZ O SCROLL AUTOMÁTICO
            lstLog.TopIndex = lstLog.Items.Count - 1;
        }

        private void UpdateProgress(int valor)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(UpdateProgress), valor);
                return;
            }
            progressBar.Value = valor;
            lblStatus.Text = $"{valor}/{progressBar.Maximum}";
        }
    }
}
