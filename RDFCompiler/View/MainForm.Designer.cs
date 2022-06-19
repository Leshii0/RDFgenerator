namespace RDFCompiler
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.bSetStore = new System.Windows.Forms.Button();
            this.bCreateStore = new System.Windows.Forms.Button();
            this.bLoadFromServer = new System.Windows.Forms.Button();
            this.bInsertData = new System.Windows.Forms.Button();
            this.bDeleteAll = new System.Windows.Forms.Button();
            this.bDeleteQuery = new System.Windows.Forms.Button();
            this.bProcess = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txbCsharpCode = new System.Windows.Forms.TextBox();
            this.txbRDF = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblDbName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.bSetStore);
            this.panel1.Controls.Add(this.bCreateStore);
            this.panel1.Controls.Add(this.bLoadFromServer);
            this.panel1.Controls.Add(this.bInsertData);
            this.panel1.Controls.Add(this.bDeleteAll);
            this.panel1.Controls.Add(this.bDeleteQuery);
            this.panel1.Controls.Add(this.bProcess);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 571);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1016, 57);
            this.panel1.TabIndex = 0;
            // 
            // bSetStore
            // 
            this.bSetStore.Dock = System.Windows.Forms.DockStyle.Right;
            this.bSetStore.Location = new System.Drawing.Point(506, 23);
            this.bSetStore.Name = "bSetStore";
            this.bSetStore.Size = new System.Drawing.Size(143, 32);
            this.bSetStore.TabIndex = 6;
            this.bSetStore.Text = "Изменить хранилище";
            this.bSetStore.UseVisualStyleBackColor = true;
            this.bSetStore.Click += new System.EventHandler(this.bSetStore_Click);
            // 
            // bCreateStore
            // 
            this.bCreateStore.Dock = System.Windows.Forms.DockStyle.Right;
            this.bCreateStore.Location = new System.Drawing.Point(649, 23);
            this.bCreateStore.Name = "bCreateStore";
            this.bCreateStore.Size = new System.Drawing.Size(160, 32);
            this.bCreateStore.TabIndex = 5;
            this.bCreateStore.Text = "Создать хранилище";
            this.bCreateStore.UseVisualStyleBackColor = true;
            this.bCreateStore.Click += new System.EventHandler(this.bCreateStore_Click);
            // 
            // bLoadFromServer
            // 
            this.bLoadFromServer.Dock = System.Windows.Forms.DockStyle.Right;
            this.bLoadFromServer.Location = new System.Drawing.Point(809, 23);
            this.bLoadFromServer.Name = "bLoadFromServer";
            this.bLoadFromServer.Size = new System.Drawing.Size(205, 32);
            this.bLoadFromServer.TabIndex = 4;
            this.bLoadFromServer.Text = "Загрузка онтологии с сервера";
            this.bLoadFromServer.UseVisualStyleBackColor = true;
            this.bLoadFromServer.Click += new System.EventHandler(this.bLoadFromServer_Click);
            // 
            // bInsertData
            // 
            this.bInsertData.Dock = System.Windows.Forms.DockStyle.Left;
            this.bInsertData.Location = new System.Drawing.Point(307, 23);
            this.bInsertData.Name = "bInsertData";
            this.bInsertData.Size = new System.Drawing.Size(199, 32);
            this.bInsertData.TabIndex = 3;
            this.bInsertData.Text = "Вставка данных";
            this.bInsertData.UseVisualStyleBackColor = true;
            this.bInsertData.Click += new System.EventHandler(this.bInsertData_Click);
            // 
            // bDeleteAll
            // 
            this.bDeleteAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.bDeleteAll.Location = new System.Drawing.Point(168, 23);
            this.bDeleteAll.Name = "bDeleteAll";
            this.bDeleteAll.Size = new System.Drawing.Size(139, 32);
            this.bDeleteAll.TabIndex = 2;
            this.bDeleteAll.Text = "Удалить всё";
            this.bDeleteAll.UseVisualStyleBackColor = true;
            this.bDeleteAll.Click += new System.EventHandler(this.bDeleteAll_Click);
            // 
            // bDeleteQuery
            // 
            this.bDeleteQuery.Dock = System.Windows.Forms.DockStyle.Left;
            this.bDeleteQuery.Location = new System.Drawing.Point(0, 23);
            this.bDeleteQuery.Name = "bDeleteQuery";
            this.bDeleteQuery.Size = new System.Drawing.Size(168, 32);
            this.bDeleteQuery.TabIndex = 1;
            this.bDeleteQuery.Text = "Запрос на удаление";
            this.bDeleteQuery.UseVisualStyleBackColor = true;
            this.bDeleteQuery.Click += new System.EventHandler(this.bDeleteQuery_Click);
            // 
            // bProcess
            // 
            this.bProcess.Dock = System.Windows.Forms.DockStyle.Top;
            this.bProcess.Location = new System.Drawing.Point(0, 0);
            this.bProcess.Name = "bProcess";
            this.bProcess.Size = new System.Drawing.Size(1014, 23);
            this.bProcess.TabIndex = 0;
            this.bProcess.Text = "Обработка";
            this.bProcess.UseVisualStyleBackColor = true;
            this.bProcess.Click += new System.EventHandler(this.bProcess_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Location = new System.Drawing.Point(0, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1016, 532);
            this.panel2.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txbCsharpCode);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txbRDF);
            this.splitContainer1.Size = new System.Drawing.Size(1016, 532);
            this.splitContainer1.SplitterDistance = 504;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // txbCsharpCode
            // 
            this.txbCsharpCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbCsharpCode.Location = new System.Drawing.Point(0, 0);
            this.txbCsharpCode.Multiline = true;
            this.txbCsharpCode.Name = "txbCsharpCode";
            this.txbCsharpCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbCsharpCode.Size = new System.Drawing.Size(504, 532);
            this.txbCsharpCode.TabIndex = 0;
            // 
            // txbRDF
            // 
            this.txbRDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbRDF.Location = new System.Drawing.Point(0, 0);
            this.txbRDF.Multiline = true;
            this.txbRDF.Name = "txbRDF";
            this.txbRDF.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbRDF.Size = new System.Drawing.Size(506, 532);
            this.txbRDF.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblDbName);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1016, 33);
            this.panel3.TabIndex = 2;
            // 
            // lblDbName
            // 
            this.lblDbName.AutoSize = true;
            this.lblDbName.Location = new System.Drawing.Point(10, 10);
            this.lblDbName.Name = "lblDbName";
            this.lblDbName.Size = new System.Drawing.Size(127, 15);
            this.lblDbName.TabIndex = 0;
            this.lblDbName.Text = "Текущее хранилище: ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 628);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Компилятор RDF";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txbCsharpCode;
        private System.Windows.Forms.TextBox txbRDF;
        private System.Windows.Forms.Button bProcess;
        private System.Windows.Forms.Button bDeleteQuery;
        private System.Windows.Forms.Button bSetStore;
        private System.Windows.Forms.Button bCreateStore;
        private System.Windows.Forms.Button bLoadFromServer;
        private System.Windows.Forms.Button bInsertData;
        private System.Windows.Forms.Button bDeleteAll;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblDbName;
    }
}
