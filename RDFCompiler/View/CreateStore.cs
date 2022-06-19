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
    public enum eOperType
    {
        create,
        set
    }
    public partial class CreateStore : Form
    {
        QueryManager _mngr;
        eOperType _type = eOperType.create;
        public CreateStore(ref QueryManager mngr,eOperType type)
        {
            InitializeComponent();
            _mngr = mngr;
            _type = type;
            if (type == eOperType.create)
            {
                Text = "Создание хранилища";
                bConfirm.Text = "Создать";
            }
            else
            {
                Text = "Открытие хранилища";
                bConfirm.Text = "Открыть";
            }
        }

        private void bConfirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbStoreName.Text))
            {
                if (_type == eOperType.create)
                {
                    try
                    {
                        _mngr.CreateStore(txbStoreName.Text);
                        MessageBox.Show("Хранилище успешно создано!");
                        Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Во время запроса произошла ошибка: " + Environment.NewLine + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        _mngr.SetStore(txbStoreName.Text);
                        MessageBox.Show("Хранилище успешно установлено!");
                        Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Во время запроса произошла ошибка: " + Environment.NewLine + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }
    }
}
