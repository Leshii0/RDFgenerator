using RDFCompiler.QueryProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RDFCompiler.View
{
    public partial class DeleteQuery : Form
    {
        string textInHeader = "Текущее хранилище: ";
        QueryManager _mngr;
        public DeleteQuery(QueryManager mngr)
        {
            InitializeComponent();
            lblStore.Text = textInHeader + mngr.DB_NAME;
            _mngr = mngr;
        }

        private void bConfirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbQuery.Text))
            {
                try
                {
                    _mngr.DeleteData(txbQuery.Text);
                    MessageBox.Show("Запрос успешно выполнен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Во время запроса произошла ошибка: " + Environment.NewLine + ex.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }

        }
    }
}
