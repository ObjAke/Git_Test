namespace Git_Test
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtChangeContent = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRipoPath = new System.Windows.Forms.TextBox();
            this.lstHistory = new System.Windows.Forms.ListBox();
            this.btnHistoryRead = new System.Windows.Forms.Button();
            this.btnBlobGet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(29, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "リポジトリ作成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(37, 94);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 45);
            this.button2.TabIndex = 1;
            this.button2.Text = "コミット";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtChangeContent
            // 
            this.txtChangeContent.Location = new System.Drawing.Point(184, 107);
            this.txtChangeContent.Name = "txtChangeContent";
            this.txtChangeContent.Size = new System.Drawing.Size(303, 19);
            this.txtChangeContent.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "変更内容について";
            // 
            // txtRipoPath
            // 
            this.txtRipoPath.Location = new System.Drawing.Point(176, 33);
            this.txtRipoPath.Name = "txtRipoPath";
            this.txtRipoPath.Size = new System.Drawing.Size(256, 19);
            this.txtRipoPath.TabIndex = 4;
            // 
            // lstHistory
            // 
            this.lstHistory.FormattingEnabled = true;
            this.lstHistory.ItemHeight = 12;
            this.lstHistory.Location = new System.Drawing.Point(25, 159);
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(750, 268);
            this.lstHistory.TabIndex = 5;
            // 
            // btnHistoryRead
            // 
            this.btnHistoryRead.Location = new System.Drawing.Point(667, 118);
            this.btnHistoryRead.Name = "btnHistoryRead";
            this.btnHistoryRead.Size = new System.Drawing.Size(108, 35);
            this.btnHistoryRead.TabIndex = 6;
            this.btnHistoryRead.Text = "履歴読出し";
            this.btnHistoryRead.UseVisualStyleBackColor = true;
            this.btnHistoryRead.Click += new System.EventHandler(this.btnHistoryRead_Click);
            // 
            // btnBlobGet
            // 
            this.btnBlobGet.Location = new System.Drawing.Point(669, 69);
            this.btnBlobGet.Name = "btnBlobGet";
            this.btnBlobGet.Size = new System.Drawing.Size(106, 35);
            this.btnBlobGet.TabIndex = 7;
            this.btnBlobGet.Text = "テキスト全取得";
            this.btnBlobGet.UseVisualStyleBackColor = true;
            this.btnBlobGet.Click += new System.EventHandler(this.btnBlobGet_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBlobGet);
            this.Controls.Add(this.btnHistoryRead);
            this.Controls.Add(this.lstHistory);
            this.Controls.Add(this.txtRipoPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtChangeContent);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtChangeContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRipoPath;
        private System.Windows.Forms.ListBox lstHistory;
        private System.Windows.Forms.Button btnHistoryRead;
        private System.Windows.Forms.Button btnBlobGet;
    }
}

