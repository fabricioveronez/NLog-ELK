using Compartilhado;
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
            this.logger.Info("Efetuando o cadastro do produto.");

            Produto produto = new Produto() { Nome = "Teste", Descricao = "Teste" };

            try
            {
                MongoDBContext db = new MongoDBContext();
                db.Produtos.InsertOne(produto);
                MessageBox.Show("Cadastro efetuado");
            }
            catch (System.Exception ex)
            {
                this.logger.Error(ex, "Erro ao cadastrar o produto.");
                MessageBox.Show("Erro ao efetuar o cadastro.");
            }
        }
    }
}
