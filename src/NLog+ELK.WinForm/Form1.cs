using Compartilhado;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NLog_ELK.WinForm
{
    public partial class Form1 : Form
    {

        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto() { Nome = txtNome.Text, Descricao = txtDescricao.Text };

            txtNome.Text = string.Empty;
            txtDescricao.Text = string.Empty;

            try
            {
                MongoDBContext db = new MongoDBContext();
                db.Produtos.InsertOne(produto);

                LogEventInfo logEvent = new LogEventInfo(LogLevel.Info, "",
                    string.Format("Produto {0}, {1}, {2} cadastrado com sucesso.",
                    produto.Id, produto.Nome, produto.Descricao));

                logEvent.Properties["nomeProduto"] = produto.Nome;
                logEvent.Properties["descricaoProduto"] = produto.Descricao;
                logEvent.Properties["idProduto"] = produto.Id;
                this.logger.Log(this.GetType(), logEvent);

                MessageBox.Show("Cadastro efetuado");
            }
            catch (System.Exception ex)
            {
                LogEventInfo logEvent = new LogEventInfo(LogLevel.Error, "", null,
                    string.Format("Erro ao cadastrar o produto {0} e {1}.",
                    produto.Nome, produto.Descricao), null, ex);

                logEvent.Properties["nomeProduto"] = produto.Nome;
                logEvent.Properties["descricaoProduto"] = produto.Descricao;
                this.logger.Log(this.GetType(), logEvent);

                MessageBox.Show("Erro ao efetuar o cadastro.");
            }
        }
    }
}
