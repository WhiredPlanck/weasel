namespace Weasel.Setup
{
    partial class SetupOptionDialog
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.inputLangGroup = new System.Windows.Forms.GroupBox();
            this.chtRadio = new System.Windows.Forms.RadioButton();
            this.chsRadio = new System.Windows.Forms.RadioButton();
            this.userFolderGroup = new System.Windows.Forms.GroupBox();
            this.customPathBox = new System.Windows.Forms.TextBox();
            this.customFolderRadio = new System.Windows.Forms.RadioButton();
            this.defaultFolderRadio = new System.Windows.Forms.RadioButton();
            this.confirmButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.inputLangGroup.SuspendLayout();
            this.userFolderGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputLangGroup
            // 
            this.inputLangGroup.Controls.Add(this.chtRadio);
            this.inputLangGroup.Controls.Add(this.chsRadio);
            this.inputLangGroup.Location = new System.Drawing.Point(9, 10);
            this.inputLangGroup.Name = "inputLangGroup";
            this.inputLangGroup.Size = new System.Drawing.Size(580, 180);
            this.inputLangGroup.TabIndex = 0;
            this.inputLangGroup.TabStop = false;
            this.inputLangGroup.Text = "Input language";
            // 
            // chtRadio
            // 
            this.chtRadio.AutoSize = true;
            this.chtRadio.Location = new System.Drawing.Point(40, 110);
            this.chtRadio.Name = "chtRadio";
            this.chtRadio.Size = new System.Drawing.Size(196, 19);
            this.chtRadio.TabIndex = 1;
            this.chtRadio.TabStop = true;
            this.chtRadio.Text = "Chinese (Traditional)";
            this.chtRadio.UseVisualStyleBackColor = true;
            // 
            // chsRadio
            // 
            this.chsRadio.AutoSize = true;
            this.chsRadio.Location = new System.Drawing.Point(40, 56);
            this.chsRadio.Name = "chsRadio";
            this.chsRadio.Size = new System.Drawing.Size(188, 19);
            this.chsRadio.TabIndex = 0;
            this.chsRadio.TabStop = true;
            this.chsRadio.Text = "Chinese (Simplified)";
            this.chsRadio.UseVisualStyleBackColor = true;
            // 
            // userFolderGroup
            // 
            this.userFolderGroup.Controls.Add(this.customPathBox);
            this.userFolderGroup.Controls.Add(this.customFolderRadio);
            this.userFolderGroup.Controls.Add(this.defaultFolderRadio);
            this.userFolderGroup.Location = new System.Drawing.Point(9, 196);
            this.userFolderGroup.Name = "userFolderGroup";
            this.userFolderGroup.Size = new System.Drawing.Size(580, 180);
            this.userFolderGroup.TabIndex = 1;
            this.userFolderGroup.TabStop = false;
            this.userFolderGroup.Text = "User folder";
            // 
            // customPathBox
            // 
            this.customPathBox.Enabled = false;
            this.customPathBox.Location = new System.Drawing.Point(40, 123);
            this.customPathBox.Name = "customPathBox";
            this.customPathBox.Size = new System.Drawing.Size(500, 25);
            this.customPathBox.TabIndex = 2;
            // 
            // customFolderRadio
            // 
            this.customFolderRadio.AutoSize = true;
            this.customFolderRadio.Location = new System.Drawing.Point(40, 82);
            this.customFolderRadio.Name = "customFolderRadio";
            this.customFolderRadio.Size = new System.Drawing.Size(148, 19);
            this.customFolderRadio.TabIndex = 1;
            this.customFolderRadio.TabStop = true;
            this.customFolderRadio.Text = "Custom location";
            this.customFolderRadio.UseVisualStyleBackColor = true;
            this.customFolderRadio.CheckedChanged += new System.EventHandler(this.CustomFolderRadio_CheckedChanged);
            // 
            // defaultFolderRadio
            // 
            this.defaultFolderRadio.AutoSize = true;
            this.defaultFolderRadio.Location = new System.Drawing.Point(40, 41);
            this.defaultFolderRadio.Name = "defaultFolderRadio";
            this.defaultFolderRadio.Size = new System.Drawing.Size(156, 19);
            this.defaultFolderRadio.TabIndex = 0;
            this.defaultFolderRadio.TabStop = true;
            this.defaultFolderRadio.Text = "Default location";
            this.defaultFolderRadio.UseVisualStyleBackColor = true;
            this.defaultFolderRadio.CheckedChanged += new System.EventHandler(this.DefaultFolderRadio_CheckedChanged);
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(49, 390);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(100, 40);
            this.confirmButton.TabIndex = 2;
            this.confirmButton.Text = "Install";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(449, 390);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(100, 40);
            this.removeButton.TabIndex = 3;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // SetupOptionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.userFolderGroup);
            this.Controls.Add(this.inputLangGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupOptionDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Weasel Setup Options";
            this.Load += new System.EventHandler(this.InstallOptionDialog_Load);
            this.inputLangGroup.ResumeLayout(false);
            this.inputLangGroup.PerformLayout();
            this.userFolderGroup.ResumeLayout(false);
            this.userFolderGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox inputLangGroup;
        private System.Windows.Forms.GroupBox userFolderGroup;
        private System.Windows.Forms.RadioButton chtRadio;
        private System.Windows.Forms.RadioButton chsRadio;
        private System.Windows.Forms.RadioButton customFolderRadio;
        private System.Windows.Forms.RadioButton defaultFolderRadio;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.TextBox customPathBox;
    }
}

