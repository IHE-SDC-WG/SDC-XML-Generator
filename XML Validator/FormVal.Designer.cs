namespace XML_Validator
{
    partial class FormVal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSchema = new System.Windows.Forms.Label();
            this.btnValidate = new System.Windows.Forms.Button();
            this.txtValMsg = new System.Windows.Forms.TextBox();
            this.txtSchema = new System.Windows.Forms.TextBox();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSchema
            // 
            this.lblSchema.AutoSize = true;
            this.lblSchema.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSchema.Location = new System.Drawing.Point(11, 17);
            this.lblSchema.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSchema.Name = "lblSchema";
            this.lblSchema.Size = new System.Drawing.Size(52, 13);
            this.lblSchema.TabIndex = 1;
            this.lblSchema.Text = "Schema";
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(14, 87);
            this.btnValidate.Margin = new System.Windows.Forms.Padding(2);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(97, 28);
            this.btnValidate.TabIndex = 2;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // txtValMsg
            // 
            this.txtValMsg.Location = new System.Drawing.Point(14, 120);
            this.txtValMsg.Multiline = true;
            this.txtValMsg.Name = "txtValMsg";
            this.txtValMsg.Size = new System.Drawing.Size(653, 331);
            this.txtValMsg.TabIndex = 6;
            this.txtValMsg.Text = "Validation Message";
            // 
            // txtSchema
            // 
            this.txtSchema.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSchema.Location = new System.Drawing.Point(70, 13);
            this.txtSchema.Name = "txtSchema";
            this.txtSchema.Size = new System.Drawing.Size(597, 22);
            this.txtSchema.TabIndex = 7;
            // 
            // txtFile
            // 
            this.txtFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFile.Location = new System.Drawing.Point(70, 43);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(597, 22);
            this.txtFile.TabIndex = 8;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFile.Location = new System.Drawing.Point(11, 47);
            this.lblFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(54, 13);
            this.lblFile.TabIndex = 9;
            this.lblFile.Text = "XML FIle";
            // 
            // FormVal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 458);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.txtSchema);
            this.Controls.Add(this.txtValMsg);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.lblSchema);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormVal";
            this.Text = "XML Validator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblSchema;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.TextBox txtValMsg;
        private System.Windows.Forms.TextBox txtSchema;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label lblFile;
    }
}

