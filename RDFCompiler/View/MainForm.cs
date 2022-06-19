using Antlr4.Runtime;
using RDFCompiler.ParserObj;
using RDFCompiler.QueryProcessor;
using RDFCompiler.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDFCompiler
{
    public partial class MainForm : Form
    {
        string fileName = "test.ss";
        string textInLabel = "Текущее хранилище: ";
        string currentDB = "CodeDomDB";
        QueryManager _mngr;
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            var width = splitContainer1.Width;
            splitContainer1.SplitterDistance = (int)width / 2;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            _mngr = new QueryManager(currentDB);
            lblDbName.Text = textInLabel + _mngr.DB_NAME;

            var file_contents = File.ReadAllText(fileName);
            txbCsharpCode.Text = file_contents;
            AntlrInputStream antlrInputStream = new AntlrInputStream(file_contents);
            var lexer = new GrammLexer(antlrInputStream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            var parser = new GrammParser(tokens);
            var progContext = parser.program();
            SimpleVisitor simpleVisitor = new SimpleVisitor();
            simpleVisitor.Visit(progContext);

            txbRDF.Text = simpleVisitor.GetAllData();
        }

        private void bProcess_Click(object sender, EventArgs e)
        {
            AntlrInputStream antlrInputStream = new AntlrInputStream(txbCsharpCode.Text);
            var lexer = new GrammLexer(antlrInputStream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            var parser = new GrammParser(tokens);
            var progContext = parser.program();
            SimpleVisitor simpleVisitor = new SimpleVisitor();
            simpleVisitor.Visit(progContext);
            txbRDF.Text = simpleVisitor.GetAllData();
        }

        private void bDeleteQuery_Click(object sender, EventArgs e)
        {
            if (_mngr != null)
            {
                DeleteQuery form = new DeleteQuery(_mngr);
                form.Show();
            }
        }

        private void bDeleteAll_Click(object sender, EventArgs e)
        {
            if (_mngr != null)
            {
                if (DialogResult.Yes == MessageBox.Show("Действительно удалить все данные из хранилища "+_mngr.DB_NAME+" ?", "", MessageBoxButtons.YesNo))
                {
                    try
                    {
                        _mngr.DeleteAll();
                        MessageBox.Show("Хранилище очищено.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Во время запроса произошла ошибка: " + Environment.NewLine + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void bInsertData_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show("Действительно выгрузить текущие триплеты в хранилище?", "", MessageBoxButtons.YesNo))
                return;

            if (_mngr != null)
            {
                if (!string.IsNullOrEmpty(txbRDF.Text))
                {
                    List<string> ontology = txbRDF.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    try
                    {
                        _mngr.InsertData(ontology);
                        MessageBox.Show("Успешная выгрузка данных!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Во время запроса произошла ошибка: " + Environment.NewLine + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void bLoadFromServer_Click(object sender, EventArgs e)
        {
            if (_mngr != null)
            {
                var result = _mngr.GetAll();
                string text = "";
                foreach(var r in result)
                {
                    text += r + Environment.NewLine;
                }
                txbRDF.Text = text;
                txbCsharpCode.Clear();
            }
        }

        private void bCreateStore_Click(object sender, EventArgs e)
        {
            if (_mngr != null)
            {
                CreateStore form = new CreateStore(ref _mngr,eOperType.create);
                form.ShowDialog();
                lblDbName.Text = textInLabel + _mngr.DB_NAME;
            }
        }

        private void bSetStore_Click(object sender, EventArgs e)
        {
            if (_mngr != null)
            {
                CreateStore form = new CreateStore(ref _mngr, eOperType.set);
                form.ShowDialog();
                lblDbName.Text = textInLabel + _mngr.DB_NAME;
            }
        }
    }
}
